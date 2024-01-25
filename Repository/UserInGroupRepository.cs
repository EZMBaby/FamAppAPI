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

        // Gesammte Tabelle abrufen
        public ICollection<UserInGroup> GetAllUsersInGroups() 
            => dataContext.UsersInGroups.OrderBy(ug => ug.UserId).ToList();

        // Alle Gruppen eines Users abrufen
        public ICollection<Groups> GetGroupsOfUser(int userId)
            => dataContext.Groups.Where(g
                => (dataContext.UsersInGroups.Where(ug => ug.UserId == userId).Select(ug => ug.GroupId).ToList())
            .Contains(g.id)).ToList();

        // Alle Users einer Gruppe abrufen
        public ICollection<User> GetUsersInGroup(int groupId)
            => dataContext.Users.Where(u
                => (dataContext.UsersInGroups.Where(ug => ug.GroupId == groupId).Select(ug => ug.UserId).ToList())
                .Contains(u.id)).ToList();

        public UserInGroup GetUserInGroup(int userId, int groupId)
        {
            var userInGroup = dataContext.UsersInGroups.SingleOrDefault(ug => ug.UserId == userId && ug.GroupId == groupId);
            return userInGroup != null ? userInGroup : new UserInGroup();
        }

        // Prüfen, ob der User in der Gruppe ist
        public bool CheckIfUserIsInGroup(int userId, int groupId)
            => dataContext.UsersInGroups.Any( ug => ug.UserId == userId && ug.GroupId == groupId);


        // Einen Benutzer in eine Gruppe eintragen
        public bool CreateUserInGroup(UserInGroup userInGroup)
        {
            dataContext.Add(userInGroup);
            return Save();
        }

        // Einen Benutzer aus einer Gruppe löschen
        public bool DeleteUserInGroup(UserInGroup userInGroup)
        {
            dataContext.Remove(userInGroup);
            return Save();
        }

        public bool Save()
            => dataContext.SaveChanges() > 0
            ? true
            : false;
    }
}
