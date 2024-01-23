using ASP.NETScholl.Models;

namespace ASP.NETScholl.ViewModels {
  public class GradeViewModel {
    public int Id { get; set; }
    public int SubjectId { get; set; }
    public int Mark { get; set; }
    public string Topic { get; set; }
    public DateTime Date { get; set; }
    public int StudentId { get; set; }
  }
}
