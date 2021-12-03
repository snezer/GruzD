using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GruzD.Web.Contracts.Provider
{
    public class ProviderModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string LegalAddress { get; set; }
        public string FactAddress { get; set; }
        public string INN { get; set; }
        public string KPP { get; set; }
        public string BIK { get; set; }
        public string Account { get; set; }
        public string CorrAccount { get; set; }
        public string CEO { get; set; }
        public string Email { get; set; }
        public string Site { get; set; }
    }
}
