using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using UniversityProject.Models;

namespace UniversityProject.DAL
{
    public class AppDbContext:IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<RelatedProducts> RelatedProducts { get; set; }
        public DbSet<ShopProduct> shopProducts { get; set; }

    }
}
