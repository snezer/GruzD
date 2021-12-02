using System;
using System.Collections.Generic;
using System.Text;

namespace GruzD.DataModel.BL
{
    public class UnloadingZone
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal CurrentWeight { get; set; }
    }
}
