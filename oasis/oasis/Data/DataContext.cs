using oasis.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Runtime;

namespace oasis.Data
{
    public class DataContext:IdentityDbContext<AppUser,AppRole,int,
        IdentityUserClaim<int>,AppUserRole,IdentityUserLogin<int>, IdentityRoleClaim<int>,IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions options):base(options)
        {

        }
        public virtual DbSet<ToDoUsers> ToDoUsers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder bulider) {
            base.OnModelCreating(bulider);


            bulider.Entity<ToDoUsers>(entity =>
            {
                entity.Property(e => e.Title).IsRequired(true);
                entity.Property(e => e.Description).IsRequired(true);
            });

            bulider.Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            bulider.Entity<AppUser>()
            .HasMany(ur => ur.ToDoUsers)
            .WithOne(u => u.User)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();

            bulider.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

        }
    }
}
