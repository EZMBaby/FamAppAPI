namespace FamAppAPI.Models
{
    public class UserInGroup
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public User Users { get; set; }
        public Groups Groups { get; set; }
    }
}
