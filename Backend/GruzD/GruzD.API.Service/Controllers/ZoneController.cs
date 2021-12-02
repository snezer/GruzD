using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GruzD.DAL.PgSql;
using GruzD.Web.Contracts;
using GruzD.Web.Contracts.Zone;
using Microsoft.AspNetCore.Mvc;

namespace GruzD.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZoneController : ControllerBase
    {
        private LogicDataContext _context;

        public ZoneController(LogicDataContext context)
        {
            _context = context;
        }

        [HttpGet("state")]
        [ProducesResponseType(typeof(ZoneStateModel), 200)]
        public IActionResult GetState(long zoneId)
        {
            var model = new ZoneStateModel()
            {
                CompanyTransportNumber = "45678912",
                SupplyTransportNumber = "45678913",

                SupplyTransportWeight = 70,
                CompanyTransportWeight = 0,
                ZoneWeight = 0,
                TransitWeight = 7,

                ZoneId = 1,
                ZoneName = "Зона погрузки А",
            };
            return Ok(model);
        }
    }
}
