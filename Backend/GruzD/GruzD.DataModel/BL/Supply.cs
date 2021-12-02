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
        public virtual Provider Provider { get; set; }
        public virtual  RawMaterialType RawMaterialType { get; set; }
    }
}
