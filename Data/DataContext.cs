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

        /// <summary>
        /// Ruft die Gruppen in der API ab oder legt sie fest.
        /// </summary>
        public DbSet<Groups> Groups { get; set; }

        /// <summary>
        /// Ruft die Benutzer in der API ab oder legt sie fest.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Ruft die Benutzer-in-Gruppen-Beziehungen in der API ab oder legt sie fest.
        /// </summary>
        public DbSet<UserInGroup> UsersInGroups { get; set; }

        /// <summary>
        /// Konfiguriert das Modell für den Datenkontext.
        /// </summary>
        /// <param name="modelBuilder">Der Modell-Builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
        }
    }
}