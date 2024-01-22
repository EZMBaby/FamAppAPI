using FamAppAPI.Models;

namespace FamAppAPI.Interfaces
{
    // Schnittstelle für den Benutzer-Repository
    public interface IUserRepository
    {
        // Alle Benutzer abrufen
        ICollection<User> GetUsers();

        // Einen Benutzer anhand der ID abrufen
        User GetUserById(int id);

        // Einen Benutzer anhand der E-Mail-Adresse abrufen
        User GetUserByMail(string email);

        // Die Anzahl der Gruppen eines Benutzers abrufen
        int GetUserGroupCount(int userId);

        // Überprüfen, ob ein Benutzer anhand der ID existiert
        bool UserExistsById(int id);

        // Überprüfen, ob ein Benutzer anhand der E-Mail-Adresse existiert
        bool UserExistsByMail(string email);

        // Ein neuer Benutzer erstellen
        bool CreateUser(User user);

        bool UpdateUser(User user);

        bool Save();
    }
}