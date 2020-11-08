using AutoMapper;
using LipeGames.Dominio.Dto;
using LipeGames.Dominio.Entidades;

namespace LipeGames.Api.AutoMapper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Amigo, AmigoDto>().ReverseMap();

            CreateMap<Jogo, JogoDto>().ReverseMap();
        }
    }
}
