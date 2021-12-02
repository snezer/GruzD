using Microsoft.AspNetCore.Identity;

namespace GruzD.Web.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsDeleted { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
    }
}
