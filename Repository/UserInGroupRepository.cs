using FamAppAPI.Data;
using FamAppAPI.Interfaces;
using FamAppAPI.Models;
using System.Collections;

namespace FamAppAPI.Repository
{
    public class UserInGroupRepository : IUserInGroupRepository
    {
        private readonly DataContext _context;

        public UserInGroupRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<UserInGroup> GetAllUsersInGroups() => _context.UsersInGroups.OrderBy(ug => ug.UserId).ToList();
        public Groups GetGroupsOfUser(int groupId)
        {
            throw new NotImplementedException();
        }

        public User GetUsersInGroup(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
