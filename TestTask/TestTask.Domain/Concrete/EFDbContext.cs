using TestTask.Domain.Entities;
using System.Data.Entity;

namespace TestTask.Domain.Concrete {
    public class EFDbContext : DbContext {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}