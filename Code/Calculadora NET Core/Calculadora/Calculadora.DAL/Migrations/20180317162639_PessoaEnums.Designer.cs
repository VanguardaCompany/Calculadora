﻿// <auto-generated />
using Calculadora.DAL;
using Calculadora.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace Calculadora.DAL.Migrations
{
    [DbContext(typeof(VanguardaContext))]
    [Migration("20180317162639_PessoaEnums")]
    partial class PessoaEnums
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Calculadora.DAL.Models.Endereco", b =>
                {
                    b.Property<int>("EnderecoID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bairro");

                    b.Property<string>("Cep");

                    b.Property<string>("Cidade");

                    b.Property<string>("Complemento");

                    b.Property<string>("Contato");

                    b.Property<string>("Estado");

                    b.Property<int>("Local");

                    b.Property<int>("Logradouro");

                    b.Property<string>("NomeLogradouro");

                    b.Property<string>("Numero");

                    b.Property<string>("Pais");

                    b.Property<string>("Telefone");

                    b.HasKey("EnderecoID");

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("Calculadora.DAL.Models.Escritorio", b =>
                {
                    b.Property<int>("EscritorioID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EnderecoID");

                    b.Property<string>("Nome");

                    b.Property<int>("ProprietarioID");

                    b.Property<string>("Registro");

                    b.Property<string>("Telefone");

                    b.HasKey("EscritorioID");

                    b.HasIndex("EnderecoID");

                    b.HasIndex("ProprietarioID");

                    b.ToTable("Escritorios");
                });

            modelBuilder.Entity("Calculadora.DAL.Models.ExpectativaVida", b =>
                {
                    b.Property<int>("ExpectativaVidaID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Data");

                    b.Property<int>("Valor");

                    b.HasKey("ExpectativaVidaID");

                    b.ToTable("ExpectativasVida");
                });

            modelBuilder.Entity("Calculadora.DAL.Models.IndiceCorrecao", b =>
                {
                    b.Property<int>("IndiceCorrecaoID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Atualizado");

                    b.Property<DateTime>("DataFim");

                    b.Property<DateTime>("DataInicio");

                    b.Property<string>("Descricao")
                        .HasMaxLength(500);

                    b.Property<bool>("Extinto");

                    b.Property<string>("Informacoes")
                        .HasMaxLength(500);

                    b.Property<string>("Nome")
                        .HasMaxLength(150);

                    b.Property<int>("Periodicidade");

                    b.Property<int>("Tipo");

                    b.HasKey("IndiceCorrecaoID");

                    b.ToTable("IndicesCorrecao");
                });

            modelBuilder.Entity("Calculadora.DAL.Models.Limite", b =>
                {
                    b.Property<int>("LimiteID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Data");

                    b.Property<double>("MaiorValorTeto");

                    b.Property<double>("MenorValorTeto");

                    b.Property<double>("TetoContribuicao");

                    b.Property<double>("TetoRMI");

                    b.HasKey("LimiteID");

                    b.ToTable("Limites");
                });

            modelBuilder.Entity("Calculadora.DAL.Models.Pessoa", b =>
                {
                    b.Property<int>("PessoaID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Cpf")
                        .IsRequired();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int>("DocumentoIdentificacao");

                    b.Property<string>("Email");

                    b.Property<int>("EnderecoID");

                    b.Property<int>("EstadoCivil");

                    b.Property<string>("HomePage");

                    b.Property<DateTime>("Nascimento");

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.Property<string>("NumeroDocumentoIdentificacao");

                    b.Property<string>("Profissao");

                    b.Property<int>("Sexo");

                    b.Property<string>("Telefone1")
                        .IsRequired();

                    b.Property<string>("Telefone2");

                    b.Property<string>("Telefone3");

                    b.Property<int>("TipoTelefone1");

                    b.Property<int?>("TipoTelefone2");

                    b.Property<int?>("TipoTelefone3");

                    b.Property<int>("Tratamento");

                    b.HasKey("PessoaID");

                    b.HasIndex("EnderecoID");

                    b.ToTable("Pessoas");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Pessoa");
                });

            modelBuilder.Entity("Calculadora.DAL.Models.ReajusteRMI", b =>
                {
                    b.Property<int>("ReajusteRMIID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Data");

                    b.Property<double>("Fator1");

                    b.Property<double>("Fator10");

                    b.Property<double>("Fator11");

                    b.Property<double>("Fator12");

                    b.Property<double>("Fator2");

                    b.Property<double>("Fator3");

                    b.Property<double>("Fator4");

                    b.Property<double>("Fator5");

                    b.Property<double>("Fator6");

                    b.Property<double>("Fator7");

                    b.Property<double>("Fator8");

                    b.Property<double>("Fator9");

                    b.Property<double>("ReajusteTotal");

                    b.Property<string>("linkPrevidencia");

                    b.HasKey("ReajusteRMIID");

                    b.ToTable("ReajusteRMIs");
                });

            modelBuilder.Entity("Calculadora.DAL.Models.Simulacao", b =>
                {
                    b.Property<int>("SimulacaoID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ClientePessoaID");

                    b.Property<DateTime>("Data");

                    b.HasKey("SimulacaoID");

                    b.HasIndex("ClientePessoaID");

                    b.ToTable("Simulacoes");
                });

            modelBuilder.Entity("Calculadora.DAL.Models.TempoContribuicao", b =>
                {
                    b.Property<int>("TempoContribuicaoID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AtividadeIPP");

                    b.Property<DateTime>("DataAdmissao");

                    b.Property<DateTime>("DataDemissao");

                    b.Property<string>("Empregador");

                    b.Property<string>("Profissao");

                    b.Property<int>("SimulacaoID");

                    b.HasKey("TempoContribuicaoID");

                    b.HasIndex("SimulacaoID");

                    b.ToTable("TempoContribuicoes");
                });

            modelBuilder.Entity("Calculadora.DAL.Models.ValorIndiceCorrecao", b =>
                {
                    b.Property<DateTime>("Data");

                    b.Property<int>("IndiceCorrecaoID");

                    b.Property<double>("Valor");

                    b.HasKey("Data", "IndiceCorrecaoID");

                    b.HasIndex("IndiceCorrecaoID");

                    b.ToTable("ValoresIndiceCorrecao");
                });

            modelBuilder.Entity("Calculadora.DAL.Models.Cliente", b =>
                {
                    b.HasBaseType("Calculadora.DAL.Models.Pessoa");

                    b.Property<int>("EscritorioID");

                    b.Property<string>("InscricaoINSS");

                    b.Property<string>("Nit");

                    b.HasIndex("EscritorioID");

                    b.ToTable("Cliente");

                    b.HasDiscriminator().HasValue("Cliente");
                });

            modelBuilder.Entity("Calculadora.DAL.Models.Escritorio", b =>
                {
                    b.HasOne("Calculadora.DAL.Models.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Calculadora.DAL.Models.Pessoa", "Proprietario")
                        .WithMany()
                        .HasForeignKey("ProprietarioID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Calculadora.DAL.Models.Pessoa", b =>
                {
                    b.HasOne("Calculadora.DAL.Models.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Calculadora.DAL.Models.Simulacao", b =>
                {
                    b.HasOne("Calculadora.DAL.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClientePessoaID");
                });

            modelBuilder.Entity("Calculadora.DAL.Models.TempoContribuicao", b =>
                {
                    b.HasOne("Calculadora.DAL.Models.Simulacao", "Simulacao")
                        .WithMany("TempoContribuicoes")
                        .HasForeignKey("SimulacaoID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Calculadora.DAL.Models.ValorIndiceCorrecao", b =>
                {
                    b.HasOne("Calculadora.DAL.Models.IndiceCorrecao", "IndiceCorrecao")
                        .WithMany("Valores")
                        .HasForeignKey("IndiceCorrecaoID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Calculadora.DAL.Models.Cliente", b =>
                {
                    b.HasOne("Calculadora.DAL.Models.Escritorio", "Escritorio")
                        .WithMany("Clientes")
                        .HasForeignKey("EscritorioID")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
