using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Calculadora.DAL.Migrations
{
    public partial class ModificacaoEndereco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Escritorios_Endereco_EnderecoID",
                table: "Escritorios");

            migrationBuilder.DropForeignKey(
                name: "FK_Pessoas_Endereco_EnderecoID",
                table: "Pessoas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Endereco",
                table: "Endereco");

            migrationBuilder.RenameTable(
                name: "Endereco",
                newName: "Enderecos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enderecos",
                table: "Enderecos",
                column: "EnderecoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Escritorios_Enderecos_EnderecoID",
                table: "Escritorios",
                column: "EnderecoID",
                principalTable: "Enderecos",
                principalColumn: "EnderecoID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoas_Enderecos_EnderecoID",
                table: "Pessoas",
                column: "EnderecoID",
                principalTable: "Enderecos",
                principalColumn: "EnderecoID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Escritorios_Enderecos_EnderecoID",
                table: "Escritorios");

            migrationBuilder.DropForeignKey(
                name: "FK_Pessoas_Enderecos_EnderecoID",
                table: "Pessoas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enderecos",
                table: "Enderecos");

            migrationBuilder.RenameTable(
                name: "Enderecos",
                newName: "Endereco");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Endereco",
                table: "Endereco",
                column: "EnderecoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Escritorios_Endereco_EnderecoID",
                table: "Escritorios",
                column: "EnderecoID",
                principalTable: "Endereco",
                principalColumn: "EnderecoID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoas_Endereco_EnderecoID",
                table: "Pessoas",
                column: "EnderecoID",
                principalTable: "Endereco",
                principalColumn: "EnderecoID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
