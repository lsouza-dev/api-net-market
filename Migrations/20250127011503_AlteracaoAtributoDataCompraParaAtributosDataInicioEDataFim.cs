using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Migrations
{
    /// <inheritdoc />
    public partial class AlteracaoAtributoDataCompraParaAtributosDataInicioEDataFim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataCompra",
                table: "Compras",
                newName: "DataInicio");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFim",
                table: "Compras",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataFim",
                table: "Compras");

            migrationBuilder.RenameColumn(
                name: "DataInicio",
                table: "Compras",
                newName: "DataCompra");
        }
    }
}
