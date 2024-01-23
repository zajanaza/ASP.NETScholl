
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETScholl.Models {
  public class ApplicationDbContext : IdentityDbContext<AppUser> {

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Student> Students { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Grade> Grades { get; set; }
  }
}
