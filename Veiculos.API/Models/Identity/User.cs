using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Veiculos.API.Models.Identity
{
    public class User : IdentityUser<int>
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public required string PrimeiroNome { get; set; }
        [MaxLength(50)]
        public required string UltimoNome { get; set; }
        [MaxLength(20)]
        public required override string UserName { get; set; }
        [MaxLength(20)]
        public required string Senha { get; set; }
        [MaxLength(100)]
        public string? Email { get; set; }
        [MaxLength(25)]
        public string? Telefone { get; set; }
        public string? Token { get; set; }
        public IEnumerable<UserRole>? UserRoles { get; set; }
    }
}
