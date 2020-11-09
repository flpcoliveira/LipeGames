using AutoMapper;
using FluentValidation;
using LipeGames.Dominio.Dto;
using LipeGames.Dominio.Entidades;
using LipeGames.Dominio.Excecoes;
using LipeGames.Dominio.Interfaces.Repositorios;
using LipeGames.Dominio.Interfaces.Servicos;
using LipeGames.Dominio.Interfaces.UnidadeTrabalho;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LipeGames.Dominio.Servicos
{
    public class EmprestimoServico : IEmprestimoServico
    {
        private readonly IUnidadeTrabalho _unidadeTrabalho;
        private readonly IEmprestimoRepositorio _repositorio;
        private readonly IMapper _mapper;
        private readonly IValidator<Emprestimo> _validator;

        public EmprestimoServico(
            IUnidadeTrabalho unidadeTrabalho,
            IEmprestimoRepositorio repositorio,
            IMapper mapper, IValidator<Emprestimo> validator
            )
        {
            _unidadeTrabalho = unidadeTrabalho;
            _repositorio = repositorio;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<EmprestimoDto> Alterar(int id, EmprestimoDto emprestimo)
        {
            var entidade = await RecuperarEntidadePorId(id);

            _mapper.Map(emprestimo, entidade);

            entidade = _repositorio.Alterar(entidade);
            await _unidadeTrabalho.Commit();

            emprestimo = _mapper.Map(entidade, emprestimo);
            return emprestimo;
        }

        public async Task<EmprestimoDto> Criar(EmprestimoDto emprestimo)
        {
            var entidade = _mapper.Map<Emprestimo>(emprestimo);

            entidade = await _repositorio.Criar(entidade);
            await _unidadeTrabalho.Commit();

            return _mapper.Map(entidade, emprestimo);
        }

        public async Task<EmprestimoDto> Detalhar(int id)
        {
            var entidade = await RecuperarEntidadePorId(id);
            return _mapper.Map<EmprestimoDto>(entidade);
        }

        public async Task Excluir(int id)
        {
            var jogo = await RecuperarEntidadePorId(id);
            _repositorio.Excluir(jogo);
            await _unidadeTrabalho.Commit();
        }

        public async Task<IEnumerable<EmprestimoDto>> Listar()
        {
            var listaJogos = await _repositorio.Listar();
            return _mapper.Map<IEnumerable<Emprestimo>, IEnumerable<EmprestimoDto>>(listaJogos);
        }

        private async Task<Emprestimo> RecuperarEntidadePorId(int id)
        {
            var entidade = await _repositorio.Detalhar(id);
            if (entidade == null) throw new EntidadeNaoEncotradaException($"Emprestimo com código {id} não encontrado");
            return entidade;
        }
    }
}
