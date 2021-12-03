using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GruzD.DAL.PgSql;
using GruzD.DataModel.BL;
using GruzD.Web.Contracts.Provider;
using GruzD.Web.Contracts.Supply;
using Microsoft.AspNetCore.Mvc;

namespace GruzD.Web.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SuppliesController : ControllerBase
    {
        private LogicDataContext _context;
        private IMapper _mapper;

        public SuppliesController(LogicDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SupplyModel), 200)]
        public async Task<IActionResult> Create(SupplyModel model)
        {
            var data = _mapper.Map<Supply>(model);
            var entry = await _context.Supplies.AddAsync(data);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<SupplyModel>(entry.Entity);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(SupplyModel[]), 200)]
        public async Task<IActionResult> GetAll()
        {
            var results =  _context.Supplies.Select(e=>_mapper.Map<SupplyModel>(e)).ToArray();
            return Ok(results);
        }

        [HttpPut]
        [ProducesResponseType(typeof(SupplyModel), 200)]
        public async Task<IActionResult> Update(SupplyModel model)
        {
            var data = _mapper.Map<Supply>(model);
            var entry = _context.Supplies.Update(data);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<SupplyModel>(entry.Entity);
            return Ok(result);
        }


    }
}
