using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LipeGames.Dominio.Interfaces
{
    public interface IUnidadeDeTrabalho
    {
        Task<bool> Commit();

        Task Rollback();
    }
}
