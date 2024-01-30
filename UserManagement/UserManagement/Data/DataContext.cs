
using Microsoft.EntityFrameworkCore;
using UserManagement.Entities;

namespace UserManagement.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define many-to-many relationship between User and Group
            modelBuilder.Entity<User>()
                .HasMany(u => u.Groups)
                .WithMany(g => g.Users)
                .UsingEntity(j => j.ToTable("UserGroups"));

            // Define many-to-many relationship between Group and Permission
            modelBuilder.Entity<Group>()
                .HasMany(g => g.Permissions)
                .WithMany(p => p.Groups)
                .UsingEntity(j => j.ToTable("GroupPermissions"));

            // Seed data
            modelBuilder.Entity<Permission>().HasData(
                new Permission { PermissionId = 1, PermissionName = "Read Access" },
                new Permission { PermissionId = 2, PermissionName = "Write Access" },
                new Permission { PermissionId = 3, PermissionName = "Admin Access" }
            );

            modelBuilder.Entity<Group>().HasData(
                new Group { GroupId = 1, GroupName = "Developers" },
                new Group { GroupId = 2, GroupName = "Managers" },
                new Group { GroupId = 3, GroupName = "Admins" }
            );
        }
    }
}