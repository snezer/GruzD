using System.ComponentModel.DataAnnotations;

namespace GruzD.Web.Contracts
{
    public class RoleContract
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string[] Claims { get; set; }
    }
}
