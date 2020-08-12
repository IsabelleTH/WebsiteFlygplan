using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebsiteFlygplan.Models;

namespace WebsiteFlygplan.Service
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //Change database columns
        ////protected override void OnModelCreating(DbModelBuilder modelBuilder)
        ////{
        ////    //    base.OnModelCreating(modelBuilder);

        ////    //    modelBuilder.Entity<ApplicationUser>().ToTable("Users").Property(p => p.Id).HasColumnName("UserId");
        ////    //modelBuilder.Entity<ApplicationUser>()
        ////    //                                   .Ignore(c => c.LockoutEndDateUtc)
        ////    //                                   .Ignore(c => c.PhoneNumberConfirmed)
        ////    //                                   .Ignore(c => c.PhoneNumber)
        ////    //                                   .Ignore(c => c.LockoutEnabled)
        ////    //                                   .Ignore(c => c.TwoFactorEnabled);
        ////}

    }
}