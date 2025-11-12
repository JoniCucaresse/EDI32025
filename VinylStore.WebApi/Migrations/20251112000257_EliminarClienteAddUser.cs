using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VinylStore.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class EliminarClienteAddUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Clientes_IdCliente",
                table: "Pedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Reseñas_Clientes_ClienteId",
                table: "Reseñas");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Reseñas_ClienteId",
                table: "Reseñas");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_IdCliente",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Reseñas");

            migrationBuilder.DropColumn(
                name: "IdPedido",
                table: "Reseñas");

            migrationBuilder.DropColumn(
                name: "IdCliente",
                table: "Pedidos");

            migrationBuilder.AddColumn<decimal>(
                name: "Precio",
                table: "Vinilos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Vinilos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DireccionEnvio",
                table: "User",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "IdUser",
                table: "Reseñas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdUser",
                table: "Pedidos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Reseñas_IdUser",
                table: "Reseñas",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_IdUser",
                table: "Pedidos",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_User_IdUser",
                table: "Pedidos",
                column: "IdUser",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Reseñas_User_IdUser",
                table: "Reseñas",
                column: "IdUser",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_User_IdUser",
                table: "Pedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Reseñas_User_IdUser",
                table: "Reseñas");

            migrationBuilder.DropIndex(
                name: "IX_Reseñas_IdUser",
                table: "Reseñas");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_IdUser",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Precio",
                table: "Vinilos");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Vinilos");

            migrationBuilder.DropColumn(
                name: "DireccionEnvio",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "Reseñas");

            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "Pedidos");

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Reseñas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdPedido",
                table: "Reseñas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdCliente",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DireccionEnvio = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaDeRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reseñas_ClienteId",
                table: "Reseñas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_IdCliente",
                table: "Pedidos",
                column: "IdCliente");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Clientes_IdCliente",
                table: "Pedidos",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reseñas_Clientes_ClienteId",
                table: "Reseñas",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
