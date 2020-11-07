using LipeGames.Dominio.Interfaces;
using LipeGames.Dominio.Interfaces.UnidadeTrabalho;
using LipeGames.Infraestrutura.Dados.EFCore.Contexto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LipeGames.Infraestrutura.Dados.EFCore
{
    public class UnidadeTrabalho : IUnidadeTrabalho
    {
        private readonly EmprestimoContexto _contexto;

        public UnidadeTrabalho(EmprestimoContexto contexto) => _contexto = contexto;

        public async Task<bool> Commit()
        {
            return await _contexto.SaveChangesAsync() > 0;
        }

        public Task Rollback()
        {
            return Task.CompletedTask;
        }

        public void Dispose() =>
            _contexto.Dispose();
    }
}
