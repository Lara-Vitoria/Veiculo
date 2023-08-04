using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Veiculos.API.Models.Identity;
using Veiculos.API.Service;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Veiculos.API.Extensions;

namespace Veiculos.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UserController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpGet("getUser")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var username = User.GetUserName();
                var user = await _userService.GetUserByUserNameAsync(username);

                return Ok(user);
            }
            catch (Exception erro)
            {

                return this.StatusCode(500, $"Erro ao retornar os usuarios: {erro}");
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLogin user)
        {
            try
            {
                var userLogin = await _userService.GetUserByUserNameAsync(user.UserName);
                if(userLogin == null) return Unauthorized("Usuario ou senha incorreto");

                var resultado = await _userService.CheckUserPasswordAsync(userLogin, user.Senha);
                if (!resultado.Succeeded) return Unauthorized("Usuario ou senha incorreto");

                
                return Ok(new
                {
                    userName = userLogin.UserName,
                    PrimeiroNome = userLogin.PrimeiroNome,
                    token = _tokenService.CreateToken(userLogin).Result
                });
            }
            catch (Exception erro)
            {

                return this.StatusCode(500, $"Erro ao fazer login: {erro}");
            }
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                if (await _userService.UserExists(user.UserName)) return BadRequest("Usuario já existe");

                var userCriado = await _userService.AddUser(user);  
                   
                if(userCriado != null) return Ok(userCriado);

                return BadRequest("Erro ao criar usuario, tente novamente");
            }
            catch (Exception erro)
            {

                return this.StatusCode(500, $"Erro ao criar usuario: {erro}");
            }
        }

        [HttpPut("updateUser")]
        public async Task<IActionResult> UpdateUser(User user)
        {
            try
            {
                var userLogin = await _userService.GetUserByUserNameAsync(User.GetUserName());
                if (userLogin == null) return Unauthorized("Usuario invalido");

                var resultado = await _userService.UpdateUser(user);
                if (resultado == null) return NoContent();

                 return Ok(resultado);

            }
            catch (Exception erro)
            {

                return this.StatusCode(500, $"Erro ao atualizar usuario: {erro}");
            }
        }
    }
}
