using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LipeGames.Dominio.Interfaces.UnidadeTrabalho
{
    public interface IUnidadeTrabalho
    {
        Task<bool> Commit();

        Task Rollback();
    }
}
