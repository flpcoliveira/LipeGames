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
    public class AmigoServico : IAmigoServico
    {

        const string MSG_ERRO_VALIDACAO = "Ocorreu um problema de validação das informações do amigo";
        const string MSG_AMIGO_NAO_ENCONTRADO = "Amigo com código {0} não encontrado";

        private readonly IUnidadeTrabalho _unidadeTrabalho;
        private readonly IAmigoRepositorio _repositorio;
        private readonly IMapper _mapper;
        private readonly IValidator<Amigo> _validator;

        public AmigoServico(IUnidadeTrabalho unidadeTrabalho, IAmigoRepositorio repositorio, IMapper mapper, IValidator<Amigo> validator)
        {
            _unidadeTrabalho = unidadeTrabalho;
            _repositorio = repositorio;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<AmigoDto> Alterar(int id, AmigoDto amigo)
        {
            var entidade = await RecuperarEntidadePorId(id);

            _mapper.Map(amigo, entidade);

            var resultadoValidacao = await _validator.ValidateAsync(entidade);
            if (!resultadoValidacao.IsValid)
            {
                var errosValidacao = resultadoValidacao.Errors.ToDictionary(mensagem => mensagem.PropertyName, mensagem => mensagem.ErrorMessage);
                throw new RegraNegocioExcecao(MSG_ERRO_VALIDACAO, errosValidacao);
            }

            entidade = _repositorio.Alterar(entidade);
            await _unidadeTrabalho.Commit();

            amigo = _mapper.Map(entidade, amigo);
            return amigo;
        }

        public async Task<AmigoDto> Criar(AmigoDto amigo)
        {
            var entidade = _mapper.Map<Amigo>(amigo);

            var resultadoValidacao = await _validator.ValidateAsync(entidade);
            if (!resultadoValidacao.IsValid)
            {
                var errosValidacao = resultadoValidacao.Errors.ToDictionary(mensagem => mensagem.PropertyName, mensagem => mensagem.ErrorMessage);
                throw new RegraNegocioExcecao(MSG_ERRO_VALIDACAO, errosValidacao);
            }

            entidade = await _repositorio.Criar(entidade);
            await _unidadeTrabalho.Commit();

            return _mapper.Map(entidade, amigo);
        }

        public async Task<AmigoDto> Detalhar(int id)
        {
            var entidade = await RecuperarEntidadePorId(id);
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
            return _mapper.Map<IEnumerable<Amigo>, IEnumerable<AmigoDto>>(listaAmigos);
        }

        private async Task<Amigo> RecuperarEntidadePorId(int id)
        {
            var entidade = await _repositorio.Detalhar(id);
            if (entidade == null) throw new EntidadeNaoEncotradaException(MSG_AMIGO_NAO_ENCONTRADO.Replace("{0}", id.ToString()));
            return entidade;
        }
    }
}
