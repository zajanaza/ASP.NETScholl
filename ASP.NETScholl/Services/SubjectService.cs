using ASP.NETScholl.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETScholl.Services {
  public class SubjectService {
    private ApplicationDbContext dbContext;

    public SubjectService(ApplicationDbContext dbContext) {
      this.dbContext = dbContext;
    }
    public async Task<IEnumerable<Subject>> GetAllAsync() {
      return await dbContext.Subjects.ToListAsync();
    }
    public async Task CreateSubjectAsync(Subject subject) {
      await dbContext.Subjects.AddAsync(subject);
      await dbContext.SaveChangesAsync();
    }
    internal Task<Subject> GetById(int id) {
      return dbContext.Subjects.FirstOrDefaultAsync(st => st.Id == id);
    }
    internal async Task UpdateAsync(Subject subject) {
      dbContext.Subjects.Update(subject);
      await dbContext.SaveChangesAsync();
    }

    internal async Task DeleteAsync(Subject subjectToDelete) {
      dbContext.Subjects.Remove(subjectToDelete);
      await dbContext.SaveChangesAsync();
    }
  }
}
