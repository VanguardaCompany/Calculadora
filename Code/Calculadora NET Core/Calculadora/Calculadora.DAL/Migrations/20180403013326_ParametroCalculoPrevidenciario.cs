using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Calculadora.DAL.Migrations
{
    public partial class ParametroCalculoPrevidenciario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoas_Escritorios_EscritorioID",
                table: "Pessoas");

            migrationBuilder.AddColumn<int>(
                name: "ClienteID",
                table: "Simulacoes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ParametroCalculoPrevidenciario",
                columns: table => new
                {
                    FatoPrevidenciarioID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ano = table.Column<int>(nullable: false),
                    TipoParametro = table.Column<string>(nullable: true),
                    ValorHomem = table.Column<int>(nullable: false),
                    ValorMulher = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParametroCalculoPrevidenciario", x => x.FatoPrevidenciarioID);
                });

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

            migrationBuilder.DropTable(
                name: "ParametroCalculoPrevidenciario");

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
