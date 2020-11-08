using AutoMapper;
using LipeGames.Api.AutoMapper;
using LipeGames.Dominio.Interfaces.Repositorios;
using LipeGames.Dominio.Interfaces.Servicos;
using LipeGames.Dominio.Interfaces.UnidadeTrabalho;
using LipeGames.Dominio.Servicos;
using LipeGames.Infraestrutura.Dados.EFCore;
using LipeGames.Infraestrutura.Dados.EFCore.Contexto;
using LipeGames.Infraestrutura.Dados.EFCore.Repositorio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LipeGames.Api.Configuration
{
    public static class ApiConfiguration
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<EmprestimoContexto>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("EmprestimoContexto"));
            });

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSwaggerGen();

            services.AddScoped<IUnidadeTrabalho, UnidadeTrabalho>();

            services.AddScoped<IAmigoRepositorio, AmigoRepositorio>();

            services.AddScoped<IAmigoServico, AmigoServico>();            

            return services;
        }
    }
}
