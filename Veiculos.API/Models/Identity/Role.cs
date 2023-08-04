using Microsoft.AspNetCore.Identity;

namespace Veiculos.API.Models.Identity
{
    public class Role : IdentityRole<int>
    {
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}
