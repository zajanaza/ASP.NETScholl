using ASP.NETScholl.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETScholl.Services {
  public class StudentService {
    private ApplicationDbContext dbContext;

    public StudentService(ApplicationDbContext dbContext) {
      this.dbContext = dbContext;
    }
    public async Task<IEnumerable<Student>> GetAllAsync() {
      return await dbContext.Students.ToListAsync();
    }
    public async Task CreateStudentAsync(Student student) {
      await dbContext.Students.AddAsync(student);
      await dbContext.SaveChangesAsync();
    }
    internal Task<Student> GetById(int id) {
      return dbContext.Students.FirstOrDefaultAsync(st => st.Id == id);
    }
    internal async Task UpdateAsync(Student student) {
      dbContext.Students.Update(student);
      await dbContext.SaveChangesAsync();
    }

    internal async Task DeleteAsync(Student studentToDelete) {
      dbContext.Students.Remove(studentToDelete);
      await dbContext.SaveChangesAsync();
    }
  }
}
