using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GruzD.Web.Contracts.Zone
{
    public class ZoneModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal CurrentWeight { get; set; }
    }
}
