using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Calculadora.DAL.Migrations
{
    public partial class AddSimulacaoPeriodo4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Simulacoes",
                columns: table => new
                {
                    SimulacaoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Simulacoes", x => x.SimulacaoID);
                });

            migrationBuilder.CreateTable(
                name: "Periodos",
                columns: table => new
                {
                    PeriodoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AtividadeIPP = table.Column<bool>(nullable: false),
                    DataAdmissao = table.Column<DateTime>(nullable: false),
                    DataDemissao = table.Column<DateTime>(nullable: false),
                    Empregador = table.Column<string>(nullable: true),
                    Profissao = table.Column<string>(nullable: true),
                    SimulacaoID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periodos", x => x.PeriodoID);
                    table.ForeignKey(
                        name: "FK_Periodos_Simulacoes_SimulacaoID",
                        column: x => x.SimulacaoID,
                        principalTable: "Simulacoes",
                        principalColumn: "SimulacaoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Periodos_SimulacaoID",
                table: "Periodos",
                column: "SimulacaoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Periodos");

            migrationBuilder.DropTable(
                name: "Simulacoes");
        }
    }
}
