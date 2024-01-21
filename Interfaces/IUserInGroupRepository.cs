using FamAppAPI.Models;

namespace FamAppAPI.Interfaces
{
    public interface IUserInGroupRepository
    {
        ICollection<UserInGroup> GetAllUsersInGroups();
        User GetUsersInGroup(int userId);
        Groups GetGroupsOfUser(int groupId);
    }
}
