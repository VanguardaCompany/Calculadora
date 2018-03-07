using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Calculadora.DAL.Migrations
{
    public partial class ModificaoPeriodoTempoContribuicao1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TempoContribuicoes_Simulacoes_SimulacaoID",
                table: "TempoContribuicoes");

            migrationBuilder.AlterColumn<int>(
                name: "SimulacaoID",
                table: "TempoContribuicoes",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TempoContribuicoes_Simulacoes_SimulacaoID",
                table: "TempoContribuicoes",
                column: "SimulacaoID",
                principalTable: "Simulacoes",
                principalColumn: "SimulacaoID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TempoContribuicoes_Simulacoes_SimulacaoID",
                table: "TempoContribuicoes");

            migrationBuilder.AlterColumn<int>(
                name: "SimulacaoID",
                table: "TempoContribuicoes",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_TempoContribuicoes_Simulacoes_SimulacaoID",
                table: "TempoContribuicoes",
                column: "SimulacaoID",
                principalTable: "Simulacoes",
                principalColumn: "SimulacaoID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
