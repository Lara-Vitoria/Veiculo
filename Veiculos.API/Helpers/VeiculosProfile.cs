using AutoMapper;
using Veiculos.API.Models.Identity;

namespace Veiculos.API.Helpers
{
    public class VeiculosProfile: Profile
    {
        public VeiculosProfile()
        {
            CreateMap<User, UserLogin>().ReverseMap();
        }
    }
}
