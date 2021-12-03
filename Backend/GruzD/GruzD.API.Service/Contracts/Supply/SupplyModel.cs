using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GruzD.Web.Contracts.Supply
{
    public class SupplyModel
    {
        public long Id { get; set; }
        public DateTime PlannedDate { get; set; }
        public DateTime FactDate { get; set; }
        public long ProviderId { get; set; }
        public string ProviderName { get; set; }
        public long RawMaterialId { get; set; }
        public string RawMaterialName { get; set; }
    }
}
