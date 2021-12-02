using System.ComponentModel.DataAnnotations;

namespace GruzD.Web.Contracts.User
{
    public class UserContract
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        [Required]
        public string RoleId { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
