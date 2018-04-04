using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Calculadora.DAL.Migrations
{
    public partial class ParametroCalculoPrevidenciario2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FatoPrevidenciarioID",
                table: "ParametroCalculoPrevidenciario",
                newName: "ParametroCalculoPrevidenciarioID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ParametroCalculoPrevidenciarioID",
                table: "ParametroCalculoPrevidenciario",
                newName: "FatoPrevidenciarioID");
        }
    }
}
