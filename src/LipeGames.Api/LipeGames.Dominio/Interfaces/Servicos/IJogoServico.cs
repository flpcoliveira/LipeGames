using LipeGames.Dominio.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LipeGames.Dominio.Interfaces.Servicos
{
    public interface IJogoServico
    {
        public Task<JogoDto> Criar(JogoDto jogo);

        public Task<JogoDto> Alterar(int id, JogoDto amigo);

        public Task<IEnumerable<JogoDto>> Listar();

        public Task<JogoDto> Detalhar(int id);

        public Task Excluir(int id);
    }
}
