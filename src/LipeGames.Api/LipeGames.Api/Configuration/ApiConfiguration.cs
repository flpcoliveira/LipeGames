using AutoMapper;
using FluentValidation;
using LipeGames.Api.AutoMapper;
using LipeGames.Dominio.Entidades;
using LipeGames.Dominio.Entidades.Validadores;
using LipeGames.Dominio.Interfaces.Repositorios;
using LipeGames.Dominio.Interfaces.Servicos;
using LipeGames.Dominio.Interfaces.UnidadeTrabalho;
using LipeGames.Dominio.Servicos;
using LipeGames.Infraestrutura.Dados.EFCore;
using LipeGames.Infraestrutura.Dados.EFCore.Contexto;
using LipeGames.Infraestrutura.Dados.EFCore.Repositorio;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

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

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<EmprestimoContexto>()
                .AddDefaultTokenProviders();


            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "API",
                    Description = "API",
                    Contact = new OpenApiContact() { Name = "Felipe Cristo", Email = "felipe.cristo@gmail.com" },
                    License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Insert the JWT token in that way: Bearer {your token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            }
                );

            services.AddScoped<IUnidadeTrabalho, UnidadeTrabalho>();

            services.AddScoped<IAmigoRepositorio, AmigoRepositorio>();

            services.AddScoped<IAmigoServico, AmigoServico>();

            services.AddScoped<IJogoRepositorio, JogoRepositorio>();

            services.AddScoped<IJogoServico, JogoServico>();

            services.AddScoped<IAutenticacaoServico, AutenticacaoServico>();

            services.AddSingleton<IValidator<Amigo>, AmigoValidator>();

            return services;
        }
    }
}
