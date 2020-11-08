using LipeGames.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LipeGames.Dominio.Interfaces.Repositorios
{
    public interface IAmigoRepositorio
    {
        public Task<Amigo> Criar(Amigo amigo);

        public Amigo Alterar(Amigo amigo);

        public Task<IEnumerable<Amigo>> Listar();

        public Task<Amigo> Detalhar(int id);

        public Task<int> Excluir(int id);
    }
}
