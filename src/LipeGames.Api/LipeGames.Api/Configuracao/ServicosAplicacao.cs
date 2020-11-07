using LipeGames.Infraestrutura.Dados.EFCore.Contexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace LipeGames.Api.Configuracao
{
    public static class ServicosAplicacao
    {
        public static void Configurar(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EmprestimoContexto>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("EmprestimoContexto"));
            });
        }
    }
}
