using LipeGames.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LipeGames.Dominio.Interfaces.Repositorios
{
    public interface IEmprestimoRepositorio
    {
        public Task<Emprestimo> Criar(Emprestimo emprestimo);

        public Emprestimo Alterar(Emprestimo emprestimo);

        public Task<IEnumerable<Emprestimo>> Listar();

        public Task<Emprestimo> Detalhar(int id);

        public void Excluir(Emprestimo emprestimo);
    }
}
