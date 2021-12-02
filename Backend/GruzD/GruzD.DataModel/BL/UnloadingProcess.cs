using System;
using System.Collections.Generic;
using System.Text;

namespace GruzD.DataModel.BL
{
    public class UnloadingProcess
    {
        public long  Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public virtual SupplierTransport IncomingTransport { get; set; }
        public virtual CompanyTransport OutgoingTransport { get; set; }
    }
}
