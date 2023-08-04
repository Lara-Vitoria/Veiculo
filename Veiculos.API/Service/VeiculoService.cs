using Microsoft.EntityFrameworkCore;
using Veiculos.API.Models;

namespace Veiculos.API.Service
{
    public class VeiculoService : IVeiculoService
    {
        private readonly VeiculosContext _context;

        public VeiculoService(VeiculosContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Veiculo> AddVeiculo(Veiculo model)
        {
            try
            {
                _context.Add(model);
                await _context.SaveChangesAsync();

                return model;
            }
            catch (Exception erro)
            {
                throw new Exception($"Erro ao adicionar novo veiculo: {erro}");
            }
        }

        public async Task<Veiculo> UpdateVeiculo(int id, Veiculo model)
        {
            try
            {
                var veiculo = await _context.Veiculos.FindAsync(id);
                if (veiculo == null) return null;

                model.Id = veiculo.Id;
                _context.Update(model);

                await _context.SaveChangesAsync();

                return veiculo;
            }
            catch (Exception erro)
            {
                throw new Exception($"Erro ao atualizar o veiculo: {erro}");
            }

        }

        public async Task<bool> DeleteVeiculo(int id)
        {
            try
            {
                var veiculo = await _context.Veiculos.FindAsync(id);
                if (veiculo == null) throw new Exception("Veiculo não encontrado");

                _context.Veiculos.Remove(veiculo);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception erro)
            {
                throw new Exception($"Erro ao excluir o veiculo: {erro}");
            }
        }

        public async Task<Veiculo[]> GetAllVeiculosAsync()
        {
            try
            {
                var veiculos = await _context.Veiculos.ToArrayAsync();
                if (veiculos == null) return null;


                return veiculos;
            }
            catch (Exception erro)
            {
                throw new Exception($"Erro ao recuperar os veiculos: {erro}");
            }
        }

        public async Task<Veiculo> GetVeiculoByIdAsync(int id)
        {
            try
            {
                var veiculo = await _context.Veiculos.FindAsync(id);
                if (veiculo == null) return null;

                return veiculo;
            }
            catch (Exception erro)
            {
                throw new Exception($"Erro ao recuperar o veiculo: {erro}");
            }
        }

        
    }
}
