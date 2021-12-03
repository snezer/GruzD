using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GruzD.DAL.PgSql;
using Microsoft.AspNetCore.Mvc;

namespace GruzD.Web.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FileController : ControllerBase
    {
        private LogicDataContext _context;

        public FileController(LogicDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetFile(long fileId)
        {
            var blob = _context.Blobs.SingleOrDefault(b => b.Id == fileId);
            if (blob == null)
            {
                return NotFound();
            }

            var ms = new MemoryStream(blob.Content);
            return File(ms, blob.ContentType ?? "application/octet-stream");
        }
    }
}
