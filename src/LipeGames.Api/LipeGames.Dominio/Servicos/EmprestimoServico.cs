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
    public class EmprestimoServico : IEmprestimoServico
    {
        private readonly IUnidadeTrabalho _unidadeTrabalho;
        private readonly IEmprestimoRepositorio _repositorio;
        IAmigoRepositorio _amigoRepositorio;
        IJogoRepositorio _jogoRepositorio;
        private readonly IMapper _mapper;
        private readonly IValidator<Emprestimo> _validator;

        public EmprestimoServico(
            IUnidadeTrabalho unidadeTrabalho,
            IEmprestimoRepositorio repositorio,
            IAmigoRepositorio amigoRepositorio,
            IJogoRepositorio jogoRepositorio,
            IMapper mapper, 
            IValidator<Emprestimo> validator
            )
        {
            _unidadeTrabalho = unidadeTrabalho;
            _repositorio = repositorio;
            _amigoRepositorio = amigoRepositorio;
            _jogoRepositorio = jogoRepositorio;
            _mapper = mapper;
            _validator = validator;
            
        }

        public async Task<EmprestimoDto> Alterar(int id, EmprestimoDto emprestimo)
        {
            var entidade = await RecuperarEntidadeEmprestimoPorId(id);

            _mapper.Map(emprestimo, entidade);

            var resultadoValidacao = await _validator.ValidateAsync(entidade);
            if (!resultadoValidacao.IsValid)
            {
                var errosValidacao = resultadoValidacao.Errors.ToDictionary(mensagem => mensagem.PropertyName, mensagem => mensagem.ErrorMessage);
                throw new RegraNegocioExcecao("Foram encontrados erros de validacao no empréstimo", errosValidacao);
            }

            entidade.Amigo = await RecuperarEntidadeAmigoPorId(emprestimo.AmigoId);

            entidade.Jogo = await RecuperarEntidadeJogoPorId(emprestimo.AmigoId);

            entidade = _repositorio.Alterar(entidade);
            await _unidadeTrabalho.Commit();

            emprestimo = _mapper.Map(entidade, emprestimo);
            return emprestimo;
        }

        public async Task<EmprestimoDto> Criar(EmprestimoDto emprestimo)
        {
            var entidade = _mapper.Map<Emprestimo>(emprestimo);

            var resultadoValidacao = await _validator.ValidateAsync(entidade);
            if (!resultadoValidacao.IsValid)
            {
                var errosValidacao = resultadoValidacao.Errors.ToDictionary(mensagem => mensagem.PropertyName, mensagem => mensagem.ErrorMessage);
                throw new RegraNegocioExcecao("Foram encontrados erros de validacao no empréstimo", errosValidacao);
            }

            entidade.Amigo = await RecuperarEntidadeAmigoPorId(emprestimo.AmigoId);

            entidade.Jogo = await RecuperarEntidadeJogoPorId(emprestimo.AmigoId);

            entidade = await _repositorio.Criar(entidade);
            await _unidadeTrabalho.Commit();

            return _mapper.Map(entidade, emprestimo);
        }

        public async Task<EmprestimoDto> Detalhar(int id)
        {
            var entidade = await RecuperarEntidadeEmprestimoPorId(id);
            return _mapper.Map<EmprestimoDto>(entidade);
        }

        public async Task Excluir(int id)
        {
            var jogo = await RecuperarEntidadeEmprestimoPorId(id);
            _repositorio.Excluir(jogo);
            await _unidadeTrabalho.Commit();
        }

        public async Task<IEnumerable<EmprestimoDto>> Listar()
        {
            var listaJogos = await _repositorio.Listar();
            return _mapper.Map<IEnumerable<Emprestimo>, IEnumerable<EmprestimoDto>>(listaJogos);
        }

        private async Task<Emprestimo> RecuperarEntidadeEmprestimoPorId(int id)
        {
            var entidade = await _repositorio.Detalhar(id);
            if (entidade == null) throw new EntidadeNaoEncotradaException($"Emprestimo com código {id} não encontrado");
            return entidade;
        }

        private async Task<Amigo> RecuperarEntidadeAmigoPorId(int id)
        {
            var entidade = await _amigoRepositorio.Detalhar(id);
            if (entidade == null) throw new EntidadeNaoEncotradaException($"Amigo com código {id} não encontrado");
            return entidade;
        }

        private async Task<Jogo> RecuperarEntidadeJogoPorId(int id)
        {
            var entidade = await _jogoRepositorio.Detalhar(id);
            if (entidade == null) throw new EntidadeNaoEncotradaException($"Jogo com código {id} não encontrado");
            return entidade;
        }
    }
}
