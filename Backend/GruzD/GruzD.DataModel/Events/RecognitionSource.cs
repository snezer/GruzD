using System;
using System.Collections.Generic;
using System.Text;
using GruzD.DataModel.BL;

namespace GruzD.DataModel.Events
{
    public class RecognitionSource
    {
        public long Id { get; set; }
        public string CameraName { get; set; }
        public string Description { get; set; }
        public UnloadingZone SupplyZone { get; set; }
        public ProcessEventType ProvidingType { get; set; }
        public long UnloadingZoneId { get; set; }
    }
}
