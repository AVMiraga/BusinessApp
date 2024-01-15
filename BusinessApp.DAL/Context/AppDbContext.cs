using BusinessApp.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BusinessApp.DAL.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<Setting>().HasData(
        //        new Setting
        //        {
        //            Key = "Logo",
        //            Value = ""
        //        },
        //        new Setting
        //        {
        //            Key = "Description",
        //            Value = ""
        //        }
        //    );
        //}

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Setting> Settings { get; set; }
    }
}
