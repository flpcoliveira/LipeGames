using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using LipeGames.Dominio.Entidades;

namespace LipeGames.Infraestrutura.Dados.EFCore.Contexto
{
    public class EmprestimoContexto: DbContext
    {

        public EmprestimoContexto(DbContextOptions<EmprestimoContexto> options): base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Amigo> Amigos { get; set; }

        public DbSet<Jogo> Jogos { get; set; }

        public DbSet<Emprestimo> Emprestimos { get; set; }
    }
}
