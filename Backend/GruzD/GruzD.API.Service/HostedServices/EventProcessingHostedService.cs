using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using GruzD.DAL.PgSql;
using GruzD.DataModel.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Prometheus;

namespace GruzD.Service.HostedServices
{
    public class EventProcessingHostedService : IHostedService, IDisposable
    {
        private ILogger<EventProcessingHostedService> _logger;
        private Task _runningTask;
        private readonly CancellationTokenSource _stoppingSrc = new CancellationTokenSource();
        static TimeSpan DelayForShutdown = TimeSpan.FromSeconds(3);
        private IServiceProvider _provider;


        public EventProcessingHostedService(ILogger<EventProcessingHostedService> logger, IServiceProvider provider)
        {
            _logger = logger;
            _provider = provider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogWarning($"Запуск сервиса обработки состояний запросов {nameof(EventProcessingHostedService)}");
            _runningTask = Task.Run(async () => await ProcessingRoutine(_stoppingSrc.Token));
            return Task.CompletedTask;
        }

        public static SemaphoreSlim CanGo = new SemaphoreSlim(0,1);

        private async Task ProcessingRoutine(CancellationToken token)
        {
            await CanGo.WaitAsync();
            _logger.LogTrace($"Запуск основного цикла сервиса {nameof(EventProcessingHostedService)}");
            int[] delayMultiplier = {1, 1, 2, 3, 5, 8, 13};
            int pointer = 0;
            using var _context = _provider.GetService<IServiceScopeFactory>().CreateScope();
            var _logicContext = _context.ServiceProvider.GetRequiredService<LogicDataContext>();

            var readDbTask = Task.Run(async () =>
            {
                _logger.LogTrace("Начало задачи отбора запросов для исполнения (запуск машин состояний)");
                try
                {
                    while (!token.IsCancellationRequested)
                    {
                        var evt = _logicContext.ProcessEvents
                            .Where(e => !e.Processed)
                            .OrderBy(e => e.Id)
                            .FirstOrDefault();

                        if (evt == null)
                        {
                            int msToWait = 100 * delayMultiplier[pointer++];
                            _logger.LogTrace("Отбор запросов не дал результата: пауза отбора {msToWait} мс",
                                msToWait);
                            await Task.Delay(msToWait, token);
                            if (pointer >= delayMultiplier.Length)
                            {
                                pointer = delayMultiplier.Length - 1;
                            }

                            continue;
                        }
                        else
                        {
                            _logger.LogTrace($"Сброс паузы отбора запросов - получены результаты");
                            pointer = 0;
                        }

                        await ProcessEvent(_logicContext, evt);
                    }

                    _logger.LogTrace("Завершение задачи отбора событий");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                        $"Ошибка основного цикла сервиса {nameof(EventProcessingHostedService)}...");
                }
            }, token);

            await readDbTask;
        }

        private async Task ProcessEvent(LogicDataContext _logicContext, ProcessEvent evt)
        {
            var zone = _logicContext.UnloadingZones.Include(e=>e.SupplierTransport).Include(e=>e.CompanyTransport).Single(z=>z.Id == evt.UnloadingZoneId);
            switch (evt.ClassifiedType)
            {
                case ProcessEventType.IncomingArrived:
                    var transport = await _logicContext.SupplierTransports.SingleAsync(e => e.Number == evt.Number);
                    zone.SupplierTransportId = transport.Id;
                    break;
                case ProcessEventType.IncomingDispatched:
                    zone.SupplierTransportId = null;
                    zone.SupplierTransport = null;
                    break;
                case ProcessEventType.OutgoingArrived:
                    var cTr = await _logicContext.CompanyTransports.SingleAsync(e => e.Number == evt.Number);
                    zone.CompanyTransportId = cTr.Id;
                    zone.CompanyTransport = cTr;
                    break;

                case ProcessEventType.OutgoingDispatched:
                    zone.CompanyTransport = null;
                    zone.CompanyTransportId = null;
                    break;
                case ProcessEventType.IncomingGet:
                    if (zone.SupplierTransport != null)
                    {
                        zone.SupplierTransport.RemainingWeight -= 5;
                        if (zone.SupplierTransport.RemainingWeight < 0)
                            zone.SupplierTransport.RemainingWeight = 0;
                    }

                    break;
                case ProcessEventType.OutgoingGet:
                    if (zone.CompanyTransport != null)
                    {
                        zone.CompanyTransport.CurrentWeight -= 5;
                        if (zone.CompanyTransport.CurrentWeight < 0)
                            zone.CompanyTransport.CurrentWeight = 0;
                    }

                    break;
                case ProcessEventType.TransitiveGet:
                    zone.CurrentWeight -= 5;
                    if (zone.CurrentWeight < 0)
                        zone.CurrentWeight = 0;
                    break;

                case ProcessEventType.IncomingPut:
                    zone.SupplierTransport.RemainingWeight += 5;
                    break;
                case ProcessEventType.OutgoingPut:
                    zone.CompanyTransport.CurrentWeight += 5;
                    break;
                case ProcessEventType.TransitivePut:
                    zone.CurrentWeight += 5;
                    break;

                case ProcessEventType.OutgoingDefect:
                case ProcessEventType.IncomingDefect:
                case ProcessEventType.TransitiveDefect:
                default:
                    break;
            }

            evt.Processed = true;
            evt.ProcessTime = DateTime.Now;
            await _logicContext.SaveChangesAsync();
        }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogWarning($"Остановка сервиса обработки состояний запросов {nameof(EventProcessingHostedService)}");
            if (_runningTask == null)
            {
                return;
            }

            try
            {
                _stoppingSrc.Cancel();
            }
            finally
            {
                await Task.WhenAny(_runningTask, Task.Delay(DelayForShutdown, cancellationToken));
                _runningTask = null;
            }
        }

        public void Dispose()
        {
            _stoppingSrc.Cancel();
        }
    }
}
