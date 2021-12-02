using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GruzD.DataModel.Events;

namespace GruzD.DAL.PgSql.Repositories
{
    public class RecognitionSourceRepository : IRecognitionSourceRepository
    {
        private LogicDataContext _context;

        public RecognitionSourceRepository(LogicDataContext context)
        {
            _context = context;
        }

        public async Task<RecognitionSource> CreateAsync(RecognitionSource src)
        {
            var entityEntry =  await _context.AddAsync(src);
            return entityEntry.Entity;
        }

        public async Task<RecognitionSource> UpdateAsync(RecognitionSource src)
        {
            var entityEntry = _context.Update(src);
            return entityEntry.Entity;
        }

        
    }
}
