using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using GruzD.DataModel.Events;

namespace GruzD.DAL.PgSql.Repositories
{
    public class EventRegistrationRepository : IProcessEventRegistrationRepository
    {
        private LogicDataContext _context;

        public EventRegistrationRepository(LogicDataContext context)
        {
            _context = context;
        }

        public async Task<ProcessEvent> CreateEventAsync(ProcessEvent prEvent)
        {
            var entry = await _context.AddAsync(prEvent);
            return entry.Entity;
        }
    }
}
