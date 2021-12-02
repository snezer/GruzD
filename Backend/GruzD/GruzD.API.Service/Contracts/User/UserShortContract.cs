namespace GruzD.Web.Contracts.User
{
    public class UserShortContract
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Account { get; internal set; }
    }
}
