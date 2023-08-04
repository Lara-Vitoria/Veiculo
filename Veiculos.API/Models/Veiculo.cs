using System.ComponentModel.DataAnnotations;
using Veiculos.API.Models.Identity;

namespace Veiculos.API.Models
{
    public class Veiculo
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public required string Nome { get; set; }

        [MaxLength(50)]
        [Required]
        public required string Marca { get; set; }

        [MaxLength(50)]
        [Required]
        public required string Modelo { get; set; }

        [MaxLength(200)]
        [Required]
        public required string ImagemURL { get; set; }

        [MaxLength(50)]
        public string? Valor { get; set; }

        [MaxLength(50)]
        public  string? Quilometragem { get; set; }

        [MaxLength(50)]
        public  string? InformacoesAdicionais { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
