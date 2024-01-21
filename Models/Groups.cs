namespace FamAppAPI.Models
{
    public class Groups
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool premium { get; set; }
        public ICollection<UserInGroup> UsersInGroups { get; set; }
    }
}
