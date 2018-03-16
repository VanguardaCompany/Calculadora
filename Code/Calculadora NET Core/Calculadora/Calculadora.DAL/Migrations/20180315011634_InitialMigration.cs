using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Calculadora.DAL.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    EnderecoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Bairro = table.Column<string>(nullable: true),
                    Cep = table.Column<string>(nullable: true),
                    Cidade = table.Column<string>(nullable: true),
                    Complemento = table.Column<string>(nullable: true),
                    Contato = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    Local = table.Column<int>(nullable: false),
                    Logradouro = table.Column<int>(nullable: false),
                    NomeLogradouro = table.Column<string>(nullable: true),
                    Numero = table.Column<string>(nullable: true),
                    Pais = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.EnderecoID);
                });

            migrationBuilder.CreateTable(
                name: "ExpectativasVida",
                columns: table => new
                {
                    ExpectativaVidaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<DateTime>(nullable: false),
                    Valor = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpectativasVida", x => x.ExpectativaVidaID);
                });

            migrationBuilder.CreateTable(
                name: "IndicesCorrecao",
                columns: table => new
                {
                    IndiceCorrecaoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Atualizado = table.Column<bool>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: false),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    Descricao = table.Column<string>(maxLength: 500, nullable: true),
                    Extinto = table.Column<bool>(nullable: false),
                    Informacoes = table.Column<string>(maxLength: 500, nullable: true),
                    Nome = table.Column<string>(maxLength: 150, nullable: true),
                    Periodicidade = table.Column<int>(nullable: false),
                    Tipo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicesCorrecao", x => x.IndiceCorrecaoID);
                });

            migrationBuilder.CreateTable(
                name: "Limites",
                columns: table => new
                {
                    LimiteID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<DateTime>(nullable: false),
                    MaiorValorTeto = table.Column<double>(nullable: false),
                    MenorValorTeto = table.Column<double>(nullable: false),
                    TetoContribuicao = table.Column<double>(nullable: false),
                    TetoRMI = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Limites", x => x.LimiteID);
                });

            migrationBuilder.CreateTable(
                name: "ReajusteRMIs",
                columns: table => new
                {
                    ReajusteRMIID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<DateTime>(nullable: false),
                    Fator1 = table.Column<double>(nullable: false),
                    Fator10 = table.Column<double>(nullable: false),
                    Fator11 = table.Column<double>(nullable: false),
                    Fator12 = table.Column<double>(nullable: false),
                    Fator2 = table.Column<double>(nullable: false),
                    Fator3 = table.Column<double>(nullable: false),
                    Fator4 = table.Column<double>(nullable: false),
                    Fator5 = table.Column<double>(nullable: false),
                    Fator6 = table.Column<double>(nullable: false),
                    Fator7 = table.Column<double>(nullable: false),
                    Fator8 = table.Column<double>(nullable: false),
                    Fator9 = table.Column<double>(nullable: false),
                    ReajusteTotal = table.Column<double>(nullable: false),
                    linkPrevidencia = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReajusteRMIs", x => x.ReajusteRMIID);
                });

            migrationBuilder.CreateTable(
                name: "ValoresIndiceCorrecao",
                columns: table => new
                {
                    Data = table.Column<DateTime>(nullable: false),
                    IndiceCorrecaoID = table.Column<int>(nullable: false),
                    Valor = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValoresIndiceCorrecao", x => new { x.Data, x.IndiceCorrecaoID });
                    table.ForeignKey(
                        name: "FK_ValoresIndiceCorrecao_IndicesCorrecao_IndiceCorrecaoID",
                        column: x => x.IndiceCorrecaoID,
                        principalTable: "IndicesCorrecao",
                        principalColumn: "IndiceCorrecaoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Escritorios",
                columns: table => new
                {
                    EscritorioID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EnderecoID = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    ProprietarioID = table.Column<int>(nullable: false),
                    Registro = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escritorios", x => x.EscritorioID);
                    table.ForeignKey(
                        name: "FK_Escritorios_Endereco_EnderecoID",
                        column: x => x.EnderecoID,
                        principalTable: "Endereco",
                        principalColumn: "EnderecoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    EscritorioID = table.Column<int>(nullable: true),
                    InscricaoINSS = table.Column<string>(nullable: true),
                    Nit = table.Column<string>(nullable: true),
                    PessoaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cpf = table.Column<string>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    DocumentoIdentificacao = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    EnderecoID = table.Column<int>(nullable: false),
                    EstadoCivil = table.Column<int>(nullable: false),
                    HomePage = table.Column<string>(nullable: true),
                    Nascimento = table.Column<DateTime>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    NumeroDocumentoIdentificacao = table.Column<string>(nullable: true),
                    Profissao = table.Column<string>(nullable: true),
                    Sexo = table.Column<int>(nullable: false),
                    Telefone1 = table.Column<string>(nullable: false),
                    Telefone2 = table.Column<string>(nullable: true),
                    Telefone3 = table.Column<string>(nullable: true),
                    TipoTelefone1 = table.Column<int>(nullable: false),
                    TipoTelefone2 = table.Column<int>(nullable: false),
                    TipoTelefone3 = table.Column<int>(nullable: false),
                    Tratamento = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.PessoaID);
                    table.ForeignKey(
                        name: "FK_Pessoas_Escritorios_EscritorioID",
                        column: x => x.EscritorioID,
                        principalTable: "Escritorios",
                        principalColumn: "EscritorioID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pessoas_Endereco_EnderecoID",
                        column: x => x.EnderecoID,
                        principalTable: "Endereco",
                        principalColumn: "EnderecoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Simulacoes",
                columns: table => new
                {
                    SimulacaoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientePessoaID = table.Column<int>(nullable: true),
                    Data = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Simulacoes", x => x.SimulacaoID);
                    table.ForeignKey(
                        name: "FK_Simulacoes_Pessoas_ClientePessoaID",
                        column: x => x.ClientePessoaID,
                        principalTable: "Pessoas",
                        principalColumn: "PessoaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TempoContribuicoes",
                columns: table => new
                {
                    TempoContribuicaoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AtividadeIPP = table.Column<bool>(nullable: false),
                    DataAdmissao = table.Column<DateTime>(nullable: false),
                    DataDemissao = table.Column<DateTime>(nullable: false),
                    Empregador = table.Column<string>(nullable: true),
                    Profissao = table.Column<string>(nullable: true),
                    SimulacaoID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempoContribuicoes", x => x.TempoContribuicaoID);
                    table.ForeignKey(
                        name: "FK_TempoContribuicoes_Simulacoes_SimulacaoID",
                        column: x => x.SimulacaoID,
                        principalTable: "Simulacoes",
                        principalColumn: "SimulacaoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Escritorios_EnderecoID",
                table: "Escritorios",
                column: "EnderecoID");

            migrationBuilder.CreateIndex(
                name: "IX_Escritorios_ProprietarioID",
                table: "Escritorios",
                column: "ProprietarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_EscritorioID",
                table: "Pessoas",
                column: "EscritorioID");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_EnderecoID",
                table: "Pessoas",
                column: "EnderecoID");

            migrationBuilder.CreateIndex(
                name: "IX_Simulacoes_ClientePessoaID",
                table: "Simulacoes",
                column: "ClientePessoaID");

            migrationBuilder.CreateIndex(
                name: "IX_TempoContribuicoes_SimulacaoID",
                table: "TempoContribuicoes",
                column: "SimulacaoID");

            migrationBuilder.CreateIndex(
                name: "IX_ValoresIndiceCorrecao_IndiceCorrecaoID",
                table: "ValoresIndiceCorrecao",
                column: "IndiceCorrecaoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Escritorios_Pessoas_ProprietarioID",
                table: "Escritorios",
                column: "ProprietarioID",
                principalTable: "Pessoas",
                principalColumn: "PessoaID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Escritorios_Endereco_EnderecoID",
                table: "Escritorios");

            migrationBuilder.DropForeignKey(
                name: "FK_Pessoas_Endereco_EnderecoID",
                table: "Pessoas");

            migrationBuilder.DropForeignKey(
                name: "FK_Escritorios_Pessoas_ProprietarioID",
                table: "Escritorios");

            migrationBuilder.DropTable(
                name: "ExpectativasVida");

            migrationBuilder.DropTable(
                name: "Limites");

            migrationBuilder.DropTable(
                name: "ReajusteRMIs");

            migrationBuilder.DropTable(
                name: "TempoContribuicoes");

            migrationBuilder.DropTable(
                name: "ValoresIndiceCorrecao");

            migrationBuilder.DropTable(
                name: "Simulacoes");

            migrationBuilder.DropTable(
                name: "IndicesCorrecao");

            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropTable(
                name: "Pessoas");

            migrationBuilder.DropTable(
                name: "Escritorios");
        }
    }
}
