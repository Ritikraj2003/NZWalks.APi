
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;
using System.Reflection.Emit;

namespace NZWalks.API.Data
{
    public class NZWalkAuthDbContext : IdentityDbContext
    {
        public NZWalkAuthDbContext(DbContextOptions<NZWalkAuthDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var ReadRoleId= "2b11e738-ab09-455b-a3f1-9b6b5ea4193f";
            var WriteRoleId = "7g8h9i0j-k1l2m3n4-o5p6q7r8s9t0-u1v2w3x4y5z6";
            var roles= new List<IdentityRole>
            {
                new IdentityRole
                {   
                    Id = ReadRoleId,
                    ConcurrencyStamp = ReadRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = WriteRoleId,
                    ConcurrencyStamp = WriteRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToLower()
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
           
        }


    }
}
