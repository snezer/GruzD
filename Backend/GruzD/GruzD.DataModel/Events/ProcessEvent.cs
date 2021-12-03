using System;
using System.Collections.Generic;
using System.Text;
using GruzD.DataModel.BL;

namespace GruzD.DataModel.Events
{
    public class ProcessEvent
    {
        public long Id { get; set; }
        public BLOBData[] Sources { get; set; }
        public DateTime Occured { get; set; }
        public DateTime Registered { get; set; }
        public string AuxData { get; set; }
        public virtual ProcessEventType ClassifiedType { get; set; }
        public string Number { get; set; }
        public decimal? Weight { get; set; }
        public bool Processed { get; set; }
        public DateTime? ProcessTime { get; set; }
        public UnloadingZone UnloadingZone { get; set; }
        public long UnloadingZoneId { get; set; }
    }
}
