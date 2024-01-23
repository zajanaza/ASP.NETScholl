using ASP.NETScholl.Models;
using Microsoft.AspNetCore.Identity;

namespace ASP.NETScholl.ViewModels {
  public class RoleEditViewModel {
        public IdentityRole Role { get; set; }
        public IEnumerable<AppUser> Members { get; set; }
        public IEnumerable<AppUser> NonMembers { get; set; }
    }
}
