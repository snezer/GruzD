using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GruzD.DAL.PgSql;
using GruzD.DataModel.BL;
using GruzD.Web.Contracts.Provider;
using Microsoft.AspNetCore.Mvc;

namespace GruzD.Web.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProvidersController : ControllerBase
    {
        private LogicDataContext _context;
        private IMapper _mapper;

        public ProvidersController(LogicDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProviderModel), 200)]
        public async Task<IActionResult> Create(ProviderModel model)
        {
            var data = _mapper.Map<Provider>(model);
            var entry = await _context.Providers.AddAsync(data);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<ProviderModel>(entry.Entity);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ProviderModel[]), 200)]
        public async Task<IActionResult> GetAll()
        {
            var results =  _context.Providers.Select(e=>_mapper.Map<ProviderModel>(e)).ToArray();
            return Ok(results);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ProviderModel), 200)]
        public async Task<IActionResult> Update(ProviderModel model)
        {
            var data = _mapper.Map<Provider>(model);
            var entry = _context.Providers.Update(data);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<ProviderModel>(entry.Entity);
            return Ok(result);
        }


    }
}
