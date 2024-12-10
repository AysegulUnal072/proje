using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using proje.Models;

namespace proje.Utility
{
    public class AppDbContext : IdentityDbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<SurveyType> SurveyTypes { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        
    }
}
