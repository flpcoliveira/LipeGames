using LipeGames.Dominio.Entidades;
using LipeGames.Dominio.Interfaces.Repositorios;
using LipeGames.Infraestrutura.Dados.EFCore.Contexto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LipeGames.Infraestrutura.Dados.EFCore.Repositorio
{
    public class JogoRepositorio : IJogoRepositorio
    {
        private readonly EmprestimoContexto _contexto;

        public JogoRepositorio(EmprestimoContexto contexto)
        {
            _contexto = contexto;
        }

        public Jogo Alterar(Jogo jogo)
        {
            var registro = _contexto.Jogos.Update(jogo);
            return jogo;
        }

        public async Task<Jogo> Criar(Jogo jogo)
        {
            var registro = await _contexto.Jogos.AddAsync(jogo);
            return jogo;
        }

        public async Task<Jogo> Detalhar(int id)
        {
            return await _contexto.Jogos.FindAsync(id);
        }

        public void Excluir(Jogo jogo)
        {
            _contexto.Jogos.Remove(jogo);
        }

        public async Task<IEnumerable<Jogo>> Listar()
        {
            return await _contexto.Jogos.AsNoTracking().ToListAsync();
        }
    }
}
