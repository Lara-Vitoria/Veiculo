using Veiculos.API.Models.Identity;

namespace Veiculos.API.Service
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}
