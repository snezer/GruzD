using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GruzD.DAL.PgSql;
using GruzD.DataModel.Events;
using GruzD.Web.Contracts;
using GruzD.Web.Contracts.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace GruzD.Web.Controllers
{
    
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EventsController : ControllerBase
    {
        private LogicDataContext _context;
        private IMapper _mapper;

        public EventsController(LogicDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        /// <summary>
        /// Передача события процесса
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Published(ProcessEventModel model)
        {
            BLOBData[] blobs = GetBlobs(model.Base64Pics);
            var dEvent = _mapper.Map<ProcessEvent>(model);
            await _context.ProcessEvents.AddAsync(dEvent);
            if (blobs != null)
            {
                foreach (var blobData in blobs)
                {
                    blobData.ProcessEvent = dEvent;
                    _context.Blobs.Add(blobData);
                }
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("last")]
        [ProducesResponseType(typeof(ProcessEventShort[]), 200)]
        public async Task<IActionResult> LastEvents(long zoneId, int number)
        {
            var result = _context.ProcessEvents
                .Where(e=>e.UnloadingZoneId==zoneId)
                .OrderByDescending(e => e.Registered)
                .Take(number)
                .Select(e => _mapper.Map<ProcessEventShort>(e)).ToArray();
            return Ok(result);
        }

        private BLOBData[] GetBlobs(string[] modelBase64Pics)
        {
            var res = modelBase64Pics?.Select(s =>
            {
                var bytes = Convert.FromBase64String(s);
                BLOBData d = new BLOBData()
                {
                    Content = bytes,
                    ContentType = "application/jpeg",
                };
                return d;
            }).ToArray();

            return res;
        }
    }
}
