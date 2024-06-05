using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Odev_Dagitim_Portali.Models
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<HomeworkSubmission> HomeworkSubmissions{ get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Class> Classes{ get; set; }
        public DbSet<UserClass> UserClasses { get; set; }

    }
}
