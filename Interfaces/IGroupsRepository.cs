using FamAppAPI.Data;
using FamAppAPI.Models;

namespace FamAppAPI.Interfaces
{
    // Schnittstelle für das Gruppen-Repository
    public interface IGroupsRepository
    {
        // Alle Gruppen abrufen
        ICollection<Groups> GetGroups();

        // Gruppe anhand der ID abrufen
        Groups GetGroupById(int groupId);

        // Die Anzahl der Benutzer in einer Gruppe abrufen
        int GetGroupUserCount(int groupId);

        // Überprüfen, ob eine Gruppe anhand der ID existiert
        bool GroupExistsById(int groupId);

        // Gruppe erstellen
        bool CreateGroup(Groups group);

        // Gruppe bearbeiten
        bool UpdateGroup(Groups group);

        bool Save();
    }
}