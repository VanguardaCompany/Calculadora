using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Calculadora.DAL.Migrations
{
    public partial class ForeignKeySimulacaoCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoas_Escritorios_EscritorioID",
                table: "Pessoas");

            migrationBuilder.DropForeignKey(
                name: "FK_Simulacoes_Pessoas_ClientePessoaID",
                table: "Simulacoes");

            migrationBuilder.DropIndex(
                name: "IX_Simulacoes_ClientePessoaID",
                table: "Simulacoes");

            migrationBuilder.DropColumn(
                name: "ClientePessoaID",
                table: "Simulacoes");

            migrationBuilder.AddColumn<int>(
                name: "ClienteID",
                table: "Simulacoes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Simulacoes_ClienteID",
                table: "Simulacoes",
                column: "ClienteID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoas_Escritorios_EscritorioID",
                table: "Pessoas",
                column: "EscritorioID",
                principalTable: "Escritorios",
                principalColumn: "EscritorioID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Simulacoes_Pessoas_ClienteID",
                table: "Simulacoes",
                column: "ClienteID",
                principalTable: "Pessoas",
                principalColumn: "PessoaID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoas_Escritorios_EscritorioID",
                table: "Pessoas");

            migrationBuilder.DropForeignKey(
                name: "FK_Simulacoes_Pessoas_ClienteID",
                table: "Simulacoes");

            migrationBuilder.DropIndex(
                name: "IX_Simulacoes_ClienteID",
                table: "Simulacoes");

            migrationBuilder.DropColumn(
                name: "ClienteID",
                table: "Simulacoes");

            migrationBuilder.AddColumn<int>(
                name: "ClientePessoaID",
                table: "Simulacoes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Simulacoes_ClientePessoaID",
                table: "Simulacoes",
                column: "ClientePessoaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoas_Escritorios_EscritorioID",
                table: "Pessoas",
                column: "EscritorioID",
                principalTable: "Escritorios",
                principalColumn: "EscritorioID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Simulacoes_Pessoas_ClientePessoaID",
                table: "Simulacoes",
                column: "ClientePessoaID",
                principalTable: "Pessoas",
                principalColumn: "PessoaID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
