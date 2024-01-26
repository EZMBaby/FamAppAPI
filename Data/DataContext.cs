using FamAppAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FamAppAPI.Data
{
    /// <summary>
    /// Stellt den Datenkontext für die FamAppAPI dar.
    /// </summary>
    public class DataContext : DbContext
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="DataContext"/> Klasse.
        /// </summary>
        /// <param name="options">Die Optionen zur Konfiguration des Datenkontexts.</param>
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        /// Ruft die Gruppen in der API ab oder legt sie fest.
        public DbSet<Groups> Groups { get; set; }

        /// Ruft die Benutzer in der API ab oder legt sie fest.
        public DbSet<User> Users { get; set; }

        /// Ruft die Benutzer-in-Gruppen-Beziehungen in der API ab oder legt sie fest.
        public DbSet<UserInGroup> UsersInGroups { get; set; }

        /// Konfiguriert das Modell für den Datenkontext.
        /// <param name="modelBuilder">Der Modell-Builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region UserInGroup

            // Konfiguriert den Primärschlüssel für die UserInGroup-Entität
            modelBuilder.Entity<UserInGroup>()
                .HasKey(userInGroup => new { userInGroup.UserId, userInGroup.GroupId });

            // Konfiguriert die Beziehung zwischen UserInGroup und Users-Entitäten
            modelBuilder.Entity<UserInGroup>()
                .HasOne(userInGroup => userInGroup.Users)
                .WithMany(user => user.UsersInGroups)
                .HasForeignKey(userInGroup => userInGroup.UserId);

            // Konfiguriert die Beziehung zwischen UserInGroup und Groups-Entitäten
            modelBuilder.Entity<UserInGroup>()
                .HasOne(userInGroup => userInGroup.Groups)
                .WithMany(group => group.UsersInGroups)
                .HasForeignKey(userInGroup => userInGroup.GroupId);

            #endregion

            #region Groups

            modelBuilder.Entity<User>()
                .HasMany(user => user.Groups) // Konfiguriert die Beziehung zwischen Groups und Users-Entitäten, um eine Gruppe zu erstellen, die nur einem User zugewiesen werden kann
                .WithOne(group => group.User)
                .HasForeignKey(group => group.UserId)
                .IsRequired();

            #endregion
        }
    }
}