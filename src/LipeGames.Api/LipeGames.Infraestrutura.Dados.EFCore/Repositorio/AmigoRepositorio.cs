using LipeGames.Dominio.Entidades;
using LipeGames.Dominio.Interfaces.Repositorios;
using LipeGames.Infraestrutura.Dados.EFCore.Contexto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LipeGames.Infraestrutura.Dados.EFCore.Repositorio
{
    public class AmigoRepositorio : IAmigoRepositorio
    {
        private readonly EmprestimoContexto _contexto;

        public AmigoRepositorio (EmprestimoContexto contexto)
        {
            _contexto = contexto;
        }

        public Amigo Alterar(Amigo amigo)
        {
            var registro = _contexto.Amigos.Update(amigo);
            return amigo;
        }

        public async Task<Amigo> Criar(Amigo amigo)
        {
            var registro = await _contexto.Amigos.AddAsync(amigo);
            return amigo;
        }

        public Task<Amigo> Detalhar(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Amigo>> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
