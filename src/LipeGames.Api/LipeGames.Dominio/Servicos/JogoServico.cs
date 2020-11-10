using AutoMapper;
using FluentValidation;
using LipeGames.Dominio.Dto;
using LipeGames.Dominio.Entidades;
using LipeGames.Dominio.Excecoes;
using LipeGames.Dominio.Interfaces.Repositorios;
using LipeGames.Dominio.Interfaces.Servicos;
using LipeGames.Dominio.Interfaces.UnidadeTrabalho;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LipeGames.Dominio.Servicos
{
    public class JogoServico : IJogoServico
    {

        const string MSG_ERRO_VALIDACAO = "Ocorreu um problema de validação das informações do jogo";
        const string MSG_AMIGO_NAO_ENCONTRADO = "Jogo com código {0} não encontrado";

        private readonly IUnidadeTrabalho _unidadeTrabalho;
        private readonly IJogoRepositorio _repositorio;
        private readonly IMapper _mapper;
        private readonly IValidator<Jogo> _validator;
        public JogoServico(IUnidadeTrabalho unidadeTrabalho, IJogoRepositorio repositorio, IMapper mapper, IValidator<Jogo> validator)
        {
            _unidadeTrabalho = unidadeTrabalho;
            _repositorio = repositorio;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<JogoDto> Alterar(int id, JogoDto jogo)
        {
            var entidade = await RecuperarEntidadePorId(id);

            _mapper.Map(jogo, entidade);

            var resultadoValidacao = await _validator.ValidateAsync(entidade);
            if (!resultadoValidacao.IsValid)
            {
                var errosValidacao = resultadoValidacao.Errors.ToDictionary(mensagem => mensagem.PropertyName, mensagem => mensagem.ErrorMessage);
                throw new RegraNegocioExcecao(MSG_ERRO_VALIDACAO, errosValidacao);
            }

            entidade = _repositorio.Alterar(entidade);
            await _unidadeTrabalho.Commit();

            jogo = _mapper.Map(entidade, jogo);
            return jogo;
        }

        public async Task<JogoDto> Criar(JogoDto jogo)
        {
            var entidade = _mapper.Map<Jogo>(jogo);

            var resultadoValidacao = await _validator.ValidateAsync(entidade);
            if (!resultadoValidacao.IsValid)
            {
                var errosValidacao = resultadoValidacao.Errors.ToDictionary(mensagem => mensagem.PropertyName, mensagem => mensagem.ErrorMessage);
                throw new RegraNegocioExcecao(MSG_ERRO_VALIDACAO, errosValidacao);
            }

            entidade = await _repositorio.Criar(entidade);
            await _unidadeTrabalho.Commit();

            return _mapper.Map(entidade, jogo);
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
            return _mapper.Map<IEnumerable<Jogo>, IEnumerable<JogoDto>>(listaJogos);
        }

        private async Task<Jogo> RecuperarEntidadePorId(int id)
        {
            var entidade = await _repositorio.Detalhar(id);
            if (entidade == null) throw new EntidadeNaoEncotradaException(MSG_AMIGO_NAO_ENCONTRADO.Replace("{0}", id.ToString()));
            return entidade;
        }
    }
}
