using LevendMonopoly.Api.Models;
using LevendMonopoly.Api.Utils;
using Microsoft.EntityFrameworkCore;

namespace LevendMonopoly.Api.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Team> Teams { get; set; } = null!;
        public DbSet<Building> Buildings { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Log> Logs { get; set; } = null!;
        public DbSet<Session> Sessions { get; set;} = null!;
        public DbSet<TeamSession> TeamSessions { get; set; } = null!;

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Guid adminRoleGuid = Guid.NewGuid();
            modelBuilder.Entity<Role>().HasData(new Role()
            {
                Id = adminRoleGuid,
                Name = "Admin"
            });

            modelBuilder.Entity<Role>().HasData(new Role()
            {
                Id = Guid.NewGuid(),
                Name = "Moderator"
            });

            byte[] passwordSalt = Cryptography.GenerateSalt();
            string passwordHash = Cryptography.HashPassword("admin", passwordSalt);
            modelBuilder.Entity<Team>().HasData(Team.CreateNewTeam("RensTest", "wachtwoord"));
            modelBuilder.Entity<User>().HasData(User.CreateNewUser("Rens", "wachtwoord", adminRoleGuid));
        }

    }
}