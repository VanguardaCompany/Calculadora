using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Calculadora.DAL.Migrations
{
    public partial class AddSimulacaoPeriodo5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientePessoaID",
                table: "Simulacoes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Simulacoes_ClientePessoaID",
                table: "Simulacoes",
                column: "ClientePessoaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Simulacoes_Pessoas_ClientePessoaID",
                table: "Simulacoes",
                column: "ClientePessoaID",
                principalTable: "Pessoas",
                principalColumn: "PessoaID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Simulacoes_Pessoas_ClientePessoaID",
                table: "Simulacoes");

            migrationBuilder.DropIndex(
                name: "IX_Simulacoes_ClientePessoaID",
                table: "Simulacoes");

            migrationBuilder.DropColumn(
                name: "ClientePessoaID",
                table: "Simulacoes");
        }
    }
}
