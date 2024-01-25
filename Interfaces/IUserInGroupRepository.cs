using FamAppAPI.Models;
using System.Text.RegularExpressions;

namespace FamAppAPI.Interfaces
{
    public interface IUserInGroupRepository
    {
        // Schnittstelle für die UserInGroup-Repository
        
        // Gesamte Tabelle abrufen
        ICollection<UserInGroup> GetAllUsersInGroups();

        // Alle Benutzer einer Gruppe abrufen
        ICollection<User> GetUsersInGroup(int groupId);

        // Alle Gruppen eines Benutzers abrufen
        ICollection<Groups> GetGroupsOfUser(int userId);

        // User in Gruppe finden
        UserInGroup GetUserInGroup(int userId, int groupId);

        // Checken ob User in einer Gruppe ist
        bool CheckIfUserIsInGroup(int userId, int groupId);

        // Benutzer einer Gruppe anlegen
        bool CreateUserInGroup(UserInGroup userInGroup);

        // Benutzer einer Gruppe entfernen
        bool DeleteUserInGroup(UserInGroup userInGroup);

        bool Save();
    }
}
