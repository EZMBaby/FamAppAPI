using FamAppAPI.Data;
using FamAppAPI.Interfaces;
using FamAppAPI.Models;

namespace FamAppAPI.Repository
{
    public class UserInGroupRepository : IUserInGroupRepository
    {
        private readonly DataContext dataContext;

        public UserInGroupRepository(DataContext context)
        {
            dataContext = context;
        }

        public ICollection<UserInGroup> GetAllUsersInGroups() 
        {
            return dataContext
                .UsersInGroups
                .OrderBy(ug => ug.UserId)
                .ToList();
        }
        public ICollection<Groups> GetGroupsOfUser(int userId)
        {
            var groupIds = dataContext
                .UsersInGroups
                .Where(ug => ug.UserId == userId)
                .Select(ug => ug.GroupId)
                .ToList();
            
            return dataContext
                .Groups
                .Where(g => groupIds.Contains(g.id))
                .ToList();
        }

        public ICollection<User> GetUsersInGroup(int groupId)
        {
            var userIds = dataContext
                .UsersInGroups
                .Where(ug => ug.GroupId == groupId)
                .Select(ug => ug.UserId)
                .ToList();

            return dataContext
                .Users
                .Where(u => userIds.Contains(u.id))
                .ToList();
        }

        public bool CreateUserInGroup(UserInGroup userInGroup)
        {
            dataContext.Add(userInGroup);
            return Save();
        }

        public bool Save()
            => dataContext.SaveChanges() > 0
            ? true
            : false;
    }
}
