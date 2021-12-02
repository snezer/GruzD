using System.ComponentModel.DataAnnotations;

namespace GruzD.Web.Contracts.User
{
    public class UserCreateContract
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string RoleId { get; set; }
        public string FullName { get; set; }
        [Required]
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
    }
}
