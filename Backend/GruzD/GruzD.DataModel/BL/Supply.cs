using System;
using System.Collections.Generic;
using System.Text;

namespace GruzD.DataModel.BL
{
    public class Supply
    {
        public long Id { get; set; }
        public DateTime PlannedDate { get; set; }
        public DateTime FactDate { get; set; }
        public  Provider Provider { get; set; }
        public long ProviderId { get; set; }
        public  RawMaterialType RawMaterialType { get; set; }
        public long RawMaterialId { get; set; }
    }
}
