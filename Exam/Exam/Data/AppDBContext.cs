using Exam.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Exam.Data
{
    public class AppDBContext: IdentityDbContext<AppUser>
    {
        public AppDBContext(DbContextOptions options) :base(options) { }
        public DbSet<Collections>Collections { get; set; }
    }
}
