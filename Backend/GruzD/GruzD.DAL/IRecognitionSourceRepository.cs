using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GruzD.DataModel.Events;

namespace GruzD.DAL
{
    public interface IRecognitionSourceRepository
    {
        public Task<RecognitionSource> CreateAsync(RecognitionSource src);
        public Task<RecognitionSource> UpdateAsync(RecognitionSource src);
    }
}
