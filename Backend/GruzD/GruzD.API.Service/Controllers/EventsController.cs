using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GruzD.DAL.PgSql;
using GruzD.Web.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GruzD.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private LogicDataContext _context;

        public EventsController(LogicDataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Published(ProcessEventModel model)
        {
            return Ok();
        }
    }
}
