using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Migrations
{
    /// <inheritdoc />
    public partial class AtributoSaldoTabelaClientes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Clientes_ClienteId",
                table: "Compras");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Compras",
                newName: "Status");

            migrationBuilder.AlterColumn<int>(
                name: "ClienteId",
                table: "Compras",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Saldo",
                table: "Clientes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Clientes_ClienteId",
                table: "Compras",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Clientes_ClienteId",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "Saldo",
                table: "Clientes");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Compras",
                newName: "status");

            migrationBuilder.AlterColumn<int>(
                name: "ClienteId",
                table: "Compras",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Clientes_ClienteId",
                table: "Compras",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id");
        }
    }
}
