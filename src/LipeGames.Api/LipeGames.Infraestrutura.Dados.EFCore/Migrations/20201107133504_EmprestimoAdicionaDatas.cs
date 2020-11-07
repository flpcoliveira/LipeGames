using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LipeGames.Infraestrutura.Dados.EFCore.Migrations
{
    public partial class EmprestimoAdicionaDatas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataDevolucao",
                table: "Emprestimos",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataEmprestimo",
                table: "Emprestimos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataDevolucao",
                table: "Emprestimos");

            migrationBuilder.DropColumn(
                name: "DataEmprestimo",
                table: "Emprestimos");
        }
    }
}
