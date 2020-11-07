using AutoMapper;
using LipeGames.Dominio.Dto;
using LipeGames.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LipeGames.Api.AutoMapper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Amigo, AmigoDto>().ReverseMap();
        }
    }
}
