using System;
using System.Collections.Generic;
using System.Text;

namespace LipeGames.Dominio.Interfaces
{
    public interface IUnitOfWork
    {
        void Commit();

        void Rollback();
    }
}
