using LipeGames.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LipeGames.Dominio.Interfaces.Repositorios
{
    public interface IJogoRepositorio
    {
        public Task<Jogo> Criar(Jogo jogo);

        public Jogo Alterar(Jogo jogo);

        public Task<IEnumerable<Jogo>> Listar();

        public Task<Jogo> Detalhar(int id);

        public void Excluir(Jogo jogo);
    }
}
