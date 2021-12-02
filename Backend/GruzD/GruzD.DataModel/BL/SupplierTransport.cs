using System;
using System.Collections.Generic;
using System.Text;

namespace GruzD.DataModel.BL
{

    public class SupplierTransport
    {
        public long Id { get; set; }

        public string Number { get; set; }

        public decimal InitialWeight { get; set; }

        public decimal RemainingWeight { get; set; }
    }
}
