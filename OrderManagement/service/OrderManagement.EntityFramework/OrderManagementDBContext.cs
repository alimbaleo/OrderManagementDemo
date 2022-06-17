using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entitities;
using OrderManagement.EntityFramework.Identity;
using static OrderManagement.Domain.Constants;
namespace OrderManagement.EntityFramework
{
    public class OrderManagementDBContext : IdentityDbContext<AppUser>
    {
        public OrderManagementDBContext(DbContextOptions<OrderManagementDBContext> options) : base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Order> Orders { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AppUser>(b =>
            {
                b.ToTable("AppUsers");
            });

            builder.Entity<IdentityRole>(i =>
            {
                i.ToTable("Roles");
                i.HasData(new IdentityRole { Id = USER_ROLE, Name = USER_ROLE, NormalizedName = USER_ROLE, ConcurrencyStamp = Guid.NewGuid().ToString() }, new IdentityRole { Id = ADMIN_ROLE, Name = ADMIN_ROLE, NormalizedName = ADMIN_ROLE, ConcurrencyStamp = Guid.NewGuid().ToString() });
            });

        }

    }
}