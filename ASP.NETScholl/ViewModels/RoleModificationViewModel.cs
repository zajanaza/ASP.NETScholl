using System.Reflection.Metadata.Ecma335;

namespace ASP.NETScholl.ViewModels {
  public class RoleModificationViewModel {
    public string RoleName { get; set; }
    public string RoleId { get; set; }
    public string[]? IdsToAdd { get; set; }
    public string[]? IdsToDelete { get; set; }
  }
}
