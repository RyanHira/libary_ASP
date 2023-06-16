using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibaryASP_MVC.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        
            public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
            {
            }


            protected override void OnModelCreating(ModelBuilder builder)
            {
                base.OnModelCreating(builder);


                //seed data Roles User, Admin, Super admin
                var adminRoleId = "7f9759e4-f369-4b04-8b56-43d9caa6b31e";
                var superAdminRoleId = "aad468d3-56b6-4980-ac9e-4739f1e8282e";
                var userRoleId = "14472e7d-afee-4db3-a3a0-27434e128054";

                var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name= "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                }
            };

                builder.Entity<IdentityRole>().HasData(roles);

                //seed superAdmin
                var superAdminId = "1d4e2765-87c6-4442-b337-35b0c7dd5b06";
                var superAdminUser = new IdentityUser
                {
                    UserName = "superadmin@bloggie.com",
                    Email = "superadmin@bloggie.com",
                    NormalizedEmail = "superadmin@bloggie.com".ToUpper(),
                    NormalizedUserName = "superadmin@bloggie.com".ToUpper(),
                    Id = superAdminId
                };

                superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                    .HashPassword(superAdminUser, "Superadmin@123");


                builder.Entity<IdentityUser>().HasData(superAdminUser);


                // Add All roles to SuperAdminUser
                var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId
                }
            };

                builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);

            }
        }
    }

