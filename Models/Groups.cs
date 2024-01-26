namespace FamAppAPI.Models
{
    public class Groups
    {
        public int id { get; set; }
        public string name { get; set; }
        public int UserId { get; set; }
        public bool premium { get; set; }
        public User User { get; set; }
        public ICollection<UserInGroup> UsersInGroups { get; set; }
    }
}
