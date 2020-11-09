using AutoMapper;
using LipeGames.Dominio.Dto;
using LipeGames.Dominio.Dto.Autenticacao;
using LipeGames.Dominio.Entidades;
using Microsoft.AspNetCore.Identity;

namespace LipeGames.Api.AutoMapper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Amigo, AmigoDto>().ReverseMap();

            CreateMap<Jogo, JogoDto>().ReverseMap();

            CreateMap<IdentityUser, UsuarioRegistroDto>()
                .ReverseMap()
                .ForMember(identity => identity.Email, opcoes => opcoes.MapFrom(dto => dto.Email))
                .ForMember(identity => identity.UserName, opcoes => opcoes.MapFrom(dto => dto.Email))
                .ForAllOtherMembers(opcoes => opcoes.Ignore());
        }
    }
}
