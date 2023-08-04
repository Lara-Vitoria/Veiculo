using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Veiculos.API.Models;
using Veiculos.API.Models.Identity;

namespace Veiculos.API.Service
{
    public class UsuarioService : IUserService
    {
        private readonly VeiculosContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public UsuarioService(VeiculosContext context, 
            UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }
        public async Task<User> AddUser(User model)
        {
            try
            {
                var user = await _userManager.CreateAsync(model, model.Senha);
                if(user.Succeeded)
                {
                    var userRetorno = await _userManager.FindByNameAsync(model.UserName);
                    return userRetorno;
                }

                return null;
            }
            catch (Exception erro)
            {
                throw new Exception($"Erro ao adicionar novo usuario: {erro}");
            }
        }

        public async Task<SignInResult> CheckUserPasswordAsync(User userUpdate, string senha)
        {
            try
            {
                var user = await _userManager.Users
                    .SingleOrDefaultAsync(user => user.UserName == userUpdate.UserName.ToLower());

                return await _signInManager.CheckPasswordSignInAsync(user, senha, false);
            }
            catch (Exception erro)
            {
                throw new Exception($"Erro ao tentar verificar a senha: {erro}");
            }
        }

        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null) throw new Exception("Usuario não encontrado");

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception erro)
            {
                throw new Exception($"Erro ao excluir o usuario: {erro}");
            }
        }

        public async Task<User[]> GetAllUsersAsync()
        {
            try
            {
                var user = await _context.Users.ToArrayAsync();
                if (user == null) return null;


                return user;
            }
            catch (Exception erro)
            {
                throw new Exception($"Erro ao recuperar os usuarios: {erro}");
            }
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null) return null;

                return user;
            }
            catch (Exception erro)
            {
                throw new Exception($"Erro ao recuperar o usuario: {erro}");
            }
        }

        public async Task<User> GetUserByUserNameAsync(string username)
        {
            try
            {
                var user = await _context.Users
                            .SingleOrDefaultAsync(user => user.UserName == username.ToLower());
                if (user == null) return null;

                return user;
            }
            catch (Exception erro)
            {
                throw new Exception($"Erro ao recuperar o usuario: {erro}");
            }
        }


        public async Task<User> UpdateUser(User model)
        {
            try
            {
                var user = await _context.Users
                            .SingleOrDefaultAsync(user => user.UserName == model.UserName.ToLower());
                if (user == null) return null;

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _userManager.ResetPasswordAsync(user, token, model.Senha);

                _context.Update<User>(user);
                if(await _context.SaveChangesAsync() > 0)
                {
                    var retorno = await _context.Users.FindAsync(user.Id);

                    return _mapper.Map<User>(retorno);
                }

                return null;
                
            }
            catch (Exception erro)
            {
                throw new Exception($"Erro ao atualizar o usuario: {erro}");
            }
        }

        public async Task<bool> UserExists(string username)
        {
            try
            {
                return await _userManager.Users
                    .AnyAsync(user => user.UserName == username.ToLower());
            }
            catch (Exception erro)
            {
                throw new Exception($"Erro ao tentar verificar existencia do usuario: {erro}");
            }
        }
    }
}
