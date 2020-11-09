using LipeGames.Dominio.Entidades;
using LipeGames.Dominio.Interfaces.Repositorios;
using LipeGames.Infraestrutura.Dados.EFCore.Contexto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LipeGames.Infraestrutura.Dados.EFCore.Repositorio
{
    public class EmprestimoRepositorio : IEmprestimoRepositorio
    {
        private readonly EmprestimoContexto _contexto;

        public EmprestimoRepositorio(EmprestimoContexto contexto)
        {
            _contexto = contexto;
        }

        public Emprestimo Alterar(Emprestimo emprestimo)
        {
            var registro = _contexto.Emprestimos.Update(emprestimo);
            return emprestimo;
        }

        public async Task<Emprestimo> Criar(Emprestimo emprestimo)
        {
            var registro = await _contexto.Emprestimos.AddAsync(emprestimo);
            return emprestimo;
        }

        public async Task<Emprestimo> Detalhar(int id)
        {
            return await _contexto.Emprestimos.FindAsync(id);
        }

        public void Excluir(Emprestimo emprestimo)
        {
            _contexto.Emprestimos.Remove(emprestimo);
        }

        public async Task<IEnumerable<Emprestimo>> Listar()
        {
            return await _contexto.Emprestimos.AsNoTracking().ToListAsync();
        }
    }
}
