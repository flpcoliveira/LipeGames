using LipeGames.Dominio.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LipeGames.Dominio.Interfaces.Servicos
{
    public interface IEmprestimoServico
    {
        public Task<EmprestimoDto> Criar(EmprestimoDto emprestimo);

        public Task<EmprestimoDto> Alterar(int id, EmprestimoDto emprestimo);

        public Task<IEnumerable<EmprestimoDto>> Listar();

        public Task<EmprestimoDto> Detalhar(int id);

        public Task Excluir(int id);
    }
}
