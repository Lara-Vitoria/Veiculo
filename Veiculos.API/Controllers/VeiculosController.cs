using Microsoft.AspNetCore.Mvc;
using Veiculos.API.Models;
using Veiculos.API.Service;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Veiculos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculosController : ControllerBase
    {
        private readonly IVeiculoService _veiculoService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public VeiculosController(IVeiculoService veiculoService, IWebHostEnvironment hostEnvironment)
        {
            _veiculoService = veiculoService;
            _hostEnvironment = hostEnvironment;
        }

        // GET: api/Veiculos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Veiculo>>> GetVeiculos()
        {
            try
            {
                var veiculos = await _veiculoService.GetAllVeiculosAsync();
                if (veiculos == null) return NotFound("Nenhum evento encontrado");

                return Ok(veiculos);
            }
            catch (Exception erro)
            {
                return this.StatusCode(500, $"Erro ao retornar os veiculos: {erro}");
            }
        }

        // GET: api/Veiculos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Veiculo>> GetVeiculo(int id)
        {
            try
            {
                var veiculo = await _veiculoService.GetVeiculoByIdAsync(id);
                if (veiculo == null) return NotFound("Nenhum evento encontrado");

                return Ok(veiculo);
            }
            catch (Exception erro)
            {
                return this.StatusCode(500, $"Erro ao retornar os veiculos: {erro}");
            }
        }

        // PUT: api/Veiculos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVeiculo(int id, Veiculo model)
        {
            try
            {
                var veiculo = await _veiculoService.UpdateVeiculo(id, model);
                if (veiculo == null) return BadRequest("Erro ao tentar atualizar o veiculo");

                return Ok(veiculo);
            }
            catch (Exception erro)
            {
                return this.StatusCode(500, $"Erro ao tentar atualizar: {erro}");
            }
        }

        // POST: api/Veiculos
        [HttpPost]
        public async Task<ActionResult<Veiculo>> PostVeiculo(Veiculo model)
        {
            try
            {
                var veiculo = await _veiculoService.AddVeiculo(model);
                if (veiculo == null) return BadRequest("Erro ao tentar adicionar o veiculo");

                return Ok(veiculo);
            }
            catch (Exception erro)
            {
                return this.StatusCode(500, $"Erro: {erro}");
            }
        }

        // DELETE: api/Veiculos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVeiculo(int id)
        {
            try
            {
                var veiculo = await _veiculoService.GetVeiculoByIdAsync(id);
                if (veiculo == null) return NoContent();

                if(await _veiculoService.DeleteVeiculo(id))
                {
                    DeleteImg(veiculo.ImagemURL);
                    return Ok(new { message = "Deletado" });
                }

                return BadRequest("Erro ao tentar excluir o veiculo");

            }
            catch (Exception erro)
            {
                return this.StatusCode(500, $"Erro ao tentar excluir: {erro}");
            }
        }


        [NonAction]
        public void DeleteImg(string imagem)
        {
            var caminhoImg = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources", imagem);

            if(System.IO.File.Exists(caminhoImg)) System.IO.File.Delete(caminhoImg);
        }

        [HttpPost("upload-img")]
        public async Task<string> SaveImg(IFormFile imagem)
        {
            string nomeImagem = new string(Path.GetFileNameWithoutExtension(imagem.FileName)
                .Take(10)
                .ToArray()).Replace(' ', '-');

            nomeImagem = $"{nomeImagem}{DateTime.UtcNow.ToString("yymmssfff")}{Path.GetExtension(imagem.FileName)}";

            var caminhoImg = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources", nomeImagem);

            using(var fileStream = new FileStream(caminhoImg, FileMode.Create))
            {
                await imagem.CopyToAsync(fileStream);
            }

            return nomeImagem;
        }
    }
}
