using Veiculos.API.Models;

namespace Veiculos.API.Service
{
    public interface IVeiculoService 
    {
        Task<Veiculo> AddVeiculo(Veiculo model);
        Task<Veiculo[]> GetAllVeiculosAsync();
        Task<Veiculo> GetVeiculoByIdAsync(int id);
        Task<Veiculo> UpdateVeiculo(int id, Veiculo model);
        Task<bool> DeleteVeiculo(int id);
    }
}
