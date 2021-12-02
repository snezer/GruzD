using System;
using System.Collections.Generic;
using System.Text;

namespace GruzD.DataModel.Recognition
{
    public class ProcessEvent
    {
        public long Id { get; set; }
        public BLOBData[] Sources { get; set; }
        public string AuxData { get; set; }
        public string ClassifiedType { get; set; }
    }
}
