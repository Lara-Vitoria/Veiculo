using Microsoft.AspNetCore.Identity;

namespace Veiculos.API.Models.Identity
{
    public class UserRole : IdentityUserRole<int>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
