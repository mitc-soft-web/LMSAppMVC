using LMSAppMVC.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LMSAppMVC.LMSDbContext
{
    public class LMSContext(DbContextOptions<LMSContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            SeedRoleData(modelBuilder);
            SeedAdminData(modelBuilder);

            modelBuilder.Entity<Member>()
                .HasOne(m => m.User)
                .WithOne(u => u.Member)
                .HasForeignKey<Member>(m => m.UserId);

            modelBuilder.Entity<Member>()
               .HasIndex(m => m.MembershipNumber)
               .IsUnique();

            modelBuilder.Entity<Librarian>()
                .HasOne(l => l.User)
                .WithOne(u => u.Librarian)
                .HasForeignKey<Librarian>(l => l.UserId);

            modelBuilder.Entity<Librarian>()
               .HasIndex(l => l.LibrarianRegistrationCode)
               .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

        }

        private void SeedAdminData(ModelBuilder modelBuilder)
        {
            var adminUserId = new Guid("c8f2e5ab-9f34-4b97-8b7c-1a5e86c77e52");
            var adminRoleId = new Guid("6e3d4978-dcb0-42ea-8c48-7f6209d4a871");
            var librarianRegNumber = Guid.NewGuid().ToString().Substring(0, 5).Replace("-", "").ToUpper();

            var adminRole = new Role
            {
                Id = adminRoleId,
                Name = "Admin",
                Description = "Has full access",
                DateCreated = DateTime.SpecifyKind(new DateTime(2026, 03, 12), DateTimeKind.Utc),
            };

            //var hasher = new PasswordHasher<object>();
            //var passwordHash = hasher.HashPassword(null, "Admin@001");
            var adminUser = new User
            {
                Email = "greatmoh007@gmail.com",
                Id = adminUserId,
                HashPassword = "AQAAAAIAAYagAAAAEM32vE7BpeyLOD9b4Zhg6UUlfA10yv/AQe4zgdtti3inkkSehi+ZMjQbEBD+b19jIQ==",
                RoleId = adminRoleId,
                DateCreated = DateTime.SpecifyKind(new DateTime(2026, 03, 12), DateTimeKind.Utc),
            };

            var admin = new Librarian
            {
                FullName = "LMS Admin",
                Id = new Guid("a65c9e02-1f0b-4e57-b3d8-7b77b4a302be"),
                UserId = adminUserId,
                LibrarianRegistrationCode = librarianRegNumber
            };

            modelBuilder.Entity<Role>().HasData(adminRole);
            modelBuilder.Entity<User>().HasData(adminUser);
            modelBuilder.Entity<Librarian>().HasData(admin);
        }
        private void SeedRoleData(ModelBuilder modelBuilder)
        {
            var roles = new List<Role>
            {
                new Role
                {
                    Id = new Guid("a45c9e02-1f0b-4e57-b3d8-9b77b4a302be"),
                    Name = "Member",
                    Description = "Members of the Library",
                    DateCreated = DateTime.SpecifyKind(new DateTime(2026, 03, 12), DateTimeKind.Utc),
                },
                new Role
                {
                    Id = new Guid("6e3d4978-dcb0-42ea-9c48-7f6209d4a871"),
                    Name = "Librarian",
                    Description = "Manages Library",
                    DateCreated = DateTime.SpecifyKind(new DateTime(2026, 03, 12), DateTimeKind.Utc),
                },


            };

            modelBuilder.Entity<Role>().HasData(roles);
        }


        public DbSet<Book> Books => Set<Book>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Librarian> Librarians => Set<Librarian>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Loan> Loans => Set<Loan>();
        public DbSet<Member> Members => Set<Member>();
        public DbSet<FailedLoginAttempts> FailedLoginAttempts => Set<FailedLoginAttempts>();
        public DbSet<LibrarianRegistrationCodeGenerator> EmployeeNumberGenerators => Set<LibrarianRegistrationCodeGenerator>();
    }
}
