using AutoMapper;
using LipeGames.Dominio.Dto;
using LipeGames.Dominio.Dto.Autenticacao;
using LipeGames.Dominio.Entidades;
using Microsoft.AspNetCore.Identity;

namespace LipeGames.Api.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Amigo, AmigoDto>().ReverseMap();

            CreateMap<Jogo, JogoDto>().ReverseMap();

            CreateMap<Emprestimo, EmprestimoDto>()
                .ReverseMap()
                .ForMember(emprestimo => emprestimo.AmigoId, opcoes => opcoes.MapFrom(dto => dto.AmigoId))
                .ForMember(emprestimo => emprestimo.JogoId, opcoes => opcoes.MapFrom(dto => dto.JogoId))
                .ForAllOtherMembers(opcoes => opcoes.Ignore());

            CreateMap<Usuario, UsuarioLoginDto>()
                .ReverseMap()
                .ForMember(entidade => entidade.Email, opcoes => opcoes.MapFrom(dto => dto.Email))
                .ForMember(entidade => entidade.Senha, opcoes => opcoes.MapFrom(dto => dto.Senha))
                .ForAllOtherMembers(opcoes => opcoes.Ignore());


            CreateMap<Usuario, UsuarioRegistroDto>()
                    .ReverseMap()
                    .ForMember(usuario => usuario.Email, opcoes => opcoes.MapFrom(dto => dto.Email))
                    .ForMember(usuario => usuario.Senha, opcoes => opcoes.MapFrom(dto => dto.Senha))
                    .ForMember(usuario => usuario.ConfirmacaoSenha, opcoes => opcoes.MapFrom(dto => dto.ConfirmacaoSenha));

            CreateMap<Usuario, IdentityUser>()
                .ForMember(usuarioAutenticacao => usuarioAutenticacao.Email, opcoes => opcoes.MapFrom(entidade => entidade.Email))
                .ForMember(usuarioAutenticacao => usuarioAutenticacao.UserName, opcoes => opcoes.MapFrom(entidade => entidade.Email));
        }

    }
}
