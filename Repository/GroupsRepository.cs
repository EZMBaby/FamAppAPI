using FamAppAPI.Data;
using FamAppAPI.Interfaces;
using FamAppAPI.Models;

namespace FamAppAPI.Repository
{
    // Repository für den Zugriff auf Gruppendaten
    public class GroupsRepository : IGroupsRepository
    {
        private readonly DataContext dataContext;

        // Konstruktor zur Initialisierung des GroupsRepository
        public GroupsRepository(DataContext context)
        {
            dataContext = context;
        }

        // Alle Gruppen abrufen
        public ICollection<Groups> GetGroups() => dataContext.Groups.OrderBy(g => g.id).ToList();

        // Gruppe anhand der ID abrufen
        public Groups GetGroupById(int groupId) => dataContext.Groups.Where(g => g.id == groupId).FirstOrDefault();

        // Überprüfen, ob eine Gruppe anhand der ID existiert
        public bool GroupExistsById(int groupId) => dataContext.Groups.Any(g => g.id == groupId);

        // Die Anzahl der Benutzer in einer Gruppe abrufen
        public int GetGroupUserCount(int groupId)
        {
            var userCount = dataContext.Groups.Where(g => g.id == groupId);
            return userCount.Count() <= 0 ? 0 : userCount.Count();
        }

        // Eine neue Gruppe erstellen
        public bool CreateGroup(Groups group)
        {
            dataContext.Add(group);
            return Save();
        }

        // Eine bestehende Gruppe aktualisieren
        public bool UpdateGroup(Groups group)
        {
            dataContext.Update(group);
            return Save();
        }

        public bool Save()
            => dataContext.SaveChanges() > 0
            ? true
            : false;
    }
}