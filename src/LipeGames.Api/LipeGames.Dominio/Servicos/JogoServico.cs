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
    public class JogoServico : IJogoServico
    {
        private readonly IUnidadeTrabalho _unidadeTrabalho;
        private readonly IJogoRepositorio _repositorio;
        private readonly IMapper _mapper;

        public JogoServico(IUnidadeTrabalho unidadeTrabalho, IJogoRepositorio repositorio, IMapper mapper)
        {
            _unidadeTrabalho = unidadeTrabalho;
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<JogoDto> Alterar(int id, JogoDto amigo)
        {
            var entidade = await _repositorio.Detalhar(id);
            if (entidade == null) throw new EntidadeNaoEncotradaException($"Amigo com código {id} não encontrado");

            _mapper.Map(amigo, entidade);

            entidade = _repositorio.Alterar(entidade);
            await _unidadeTrabalho.Commit();

            amigo = _mapper.Map(entidade, amigo);
            return amigo;
        }

        public async Task<JogoDto> Criar(JogoDto amigo)
        {
            var entidade = _mapper.Map<Jogo>(amigo);            

            entidade = await _repositorio.Criar(entidade);
            await _unidadeTrabalho.Commit();

            return _mapper.Map(entidade, amigo);
        }

        public async Task<JogoDto> Detalhar(int id)
        {
            var entidade = await RecuperarEntidadePorId(id);
            return _mapper.Map<JogoDto>(entidade);
        }

        public async Task Excluir(int id)
        {
            var jogo = await RecuperarEntidadePorId(id);
             _repositorio.Excluir(jogo);
            await _unidadeTrabalho.Commit();
        }

        public async Task<IEnumerable<JogoDto>> Listar()
        {
            var listaJogos = await _repositorio.Listar();
            return _mapper.Map <IEnumerable<Jogo>, IEnumerable<JogoDto>>(listaJogos);
        }

        private async Task<Jogo> RecuperarEntidadePorId(int id)
        {
            var entidade = await _repositorio.Detalhar(id);
            if (entidade == null) throw new EntidadeNaoEncotradaException($"Jogo com código {id} não encontrado");
            return entidade;
        }
    }
}
