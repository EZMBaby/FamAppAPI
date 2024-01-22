using FamAppAPI.Models;
using System.Text.RegularExpressions;

namespace FamAppAPI.Interfaces
{
    public interface IUserInGroupRepository
    {
        ICollection<UserInGroup> GetAllUsersInGroups();
        ICollection<User> GetUsersInGroup(int groupId);
        ICollection<Groups> GetGroupsOfUser(int userId);

        bool CreateUserInGroup(UserInGroup userInGroup);

        bool Save();
    }
}
