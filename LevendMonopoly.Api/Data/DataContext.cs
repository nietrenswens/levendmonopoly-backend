using LevendMonopoly.Api.Models;
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
        
    }
}