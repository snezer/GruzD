using System;
using System.Collections.Generic;
using System.Text;
using GruzD.DataModel.Events;

namespace GruzD.DataModel.BL
{
    public class UnloadingZone
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal CurrentWeight { get; set; }
        public List<RecognitionSource> Sources { get; set; }

        public long? SupplierTransportId { get; set; }
        public long? CompanyTransportId { get; set; }

        public virtual SupplierTransport SupplierTransport { get; set; }

        public virtual CompanyTransport CompanyTransport { get; set; }
    }
}
