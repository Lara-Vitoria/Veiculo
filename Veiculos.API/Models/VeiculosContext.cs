using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Veiculos.API.Models.Identity;

namespace Veiculos.API.Models
{
    public class VeiculosContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, 
            UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, 
            IdentityUserToken<int>>
    {
        public VeiculosContext(DbContextOptions<VeiculosContext> opt) 
                : base(opt) {   }

        public DbSet<Veiculo> Veiculos { get; set; } = null!;
        //public DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>(ur =>
            {
                ur.HasKey(userRole => new { userRole.UserId, userRole.RoleId });

                ur.HasOne(userRole => userRole.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(userRole => userRole.RoleId)
                    .IsRequired();

                ur.HasOne(userRole => userRole.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(userRole => userRole.UserId)
                    .IsRequired();
            });
        }
    }
}
