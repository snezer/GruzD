using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GruzD.DAL.PgSql;
using GruzD.DataModel.BL;
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
        private IMapper _mapper;

        public ZoneController(LogicDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        [HttpGet]
        [ProducesResponseType(typeof(ZoneModel[]), 200)]
        public IActionResult GetAllZones()
        {
            var results = _context.RecognitionSources.Select(s => _mapper.Map<ZoneModel>(s)).ToArray();
            return Ok(results);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ZoneModel), 200)]
        public async Task<IActionResult> CreateZone(ZoneModel model)
        {
            var newOne = _mapper.Map<UnloadingZone>(model);
            var entry = await _context.AddAsync(newOne);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<ZoneModel>(entry.Entity);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ZoneModel), 200)]
        public async Task<IActionResult> UpdateZone(ZoneModel model)
        {
            var newOne = _mapper.Map<UnloadingZone>(model);
            var entry = _context.Update(newOne);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<ZoneModel>(entry.Entity);
            return Ok(result);
        }
    }
}
