using LipeGames.Dominio.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LipeGames.Dominio.Interfaces.Servicos
{
    public interface IAmigoServico
    {
        public Task<AmigoDto> Criar(AmigoDto amigo);

        public Task<AmigoDto> Alterar(int id, AmigoDto amigo);

        public Task<IEnumerable<AmigoDto>> Listar();

        public Task<AmigoDto> Detalhar(int id);

        public Task Excluir(int id);
    }
}
