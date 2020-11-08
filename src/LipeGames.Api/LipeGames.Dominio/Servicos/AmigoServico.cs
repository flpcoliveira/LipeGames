using AutoMapper;
using LipeGames.Dominio.Dto;
using LipeGames.Dominio.Entidades;
using LipeGames.Dominio.Excecoes;
using LipeGames.Dominio.Interfaces.Repositorios;
using LipeGames.Dominio.Interfaces.Servicos;
using LipeGames.Dominio.Interfaces.UnidadeTrabalho;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LipeGames.Dominio.Servicos
{
    public class AmigoServico : IAmigoServico
    {
        private readonly IUnidadeTrabalho _unidadeTrabalho;
        private readonly IAmigoRepositorio _repositorio;
        private readonly IMapper _mapper;

        public AmigoServico(IUnidadeTrabalho unidadeTrabalho, IAmigoRepositorio repositorio, IMapper mapper)
        {
            _unidadeTrabalho = unidadeTrabalho;
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<AmigoDto> Alterar(int id, AmigoDto amigo)
        {
            var entidade = await _repositorio.Detalhar(id);
            if (entidade == null) throw new EntidadeNaoEncotradaException($"Amigo com código {id} não encontrado");

            _mapper.Map(amigo, entidade);

            entidade = _repositorio.Alterar(entidade);
            await _unidadeTrabalho.Commit();

            amigo = _mapper.Map(entidade, amigo);
            return amigo;
        }

        public async Task<AmigoDto> Criar(AmigoDto amigo)
        {
            var entidade = _mapper.Map<Amigo>(amigo);            

            entidade = await _repositorio.Criar(entidade);
            await _unidadeTrabalho.Commit();

            return _mapper.Map(entidade, amigo);
        }

        public async Task<AmigoDto> Detalhar(int id)
        {
            var entidade = await _repositorio.Detalhar(id);
            if (entidade == null) throw new EntidadeNaoEncotradaException($"Amigo com código {id} não encontrado");

            return _mapper.Map<AmigoDto>(entidade);
        }

        public async Task Excluir(int id)
        {
            var jogo = await RecuperarEntidadePorId(id);
             _repositorio.Excluir(jogo);
            await _unidadeTrabalho.Commit();
        }

        public async Task<IEnumerable<AmigoDto>> Listar()
        {
            var listaAmigos = await _repositorio.Listar();
            return _mapper.Map <IEnumerable<Amigo>, IEnumerable<AmigoDto>>(listaAmigos);
        }

        private async Task<Amigo> RecuperarEntidadePorId(int id)
        {
            var entidade = await _repositorio.Detalhar(id);
            if (entidade == null) throw new EntidadeNaoEncotradaException($"Amigo com código {id} não encontrado");
            return entidade;
        }
    }
}
