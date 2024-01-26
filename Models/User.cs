namespace FamAppAPI.Models
{
    public class User
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public ICollection<Groups> Groups { get; set; }
        public ICollection<UserInGroup> UsersInGroups { get; set; }
    }
}
