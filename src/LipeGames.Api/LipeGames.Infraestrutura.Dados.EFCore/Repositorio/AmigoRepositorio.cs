using LipeGames.Dominio.Entidades;
using LipeGames.Dominio.Interfaces.Repositorios;
using LipeGames.Infraestrutura.Dados.EFCore.Contexto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LipeGames.Infraestrutura.Dados.EFCore.Repositorio
{
    public class AmigoRepositorio : IAmigoRepositorio
    {
        private readonly EmprestimoContexto _contexto;

        public AmigoRepositorio(EmprestimoContexto contexto)
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

        public async Task<Amigo> Detalhar(int id)
        {
            return await _contexto.Amigos.FindAsync(id);
        }

        public void Excluir(Amigo amigo)
        {
            _contexto.Remove(amigo);
        }

        public async Task<IEnumerable<Amigo>> Listar()
        {
            return await _contexto.Amigos.AsNoTracking().ToListAsync();
        }
    }
}
