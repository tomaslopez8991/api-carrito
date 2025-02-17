using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingCart.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ItemsCarrito : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Dni = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    EsVip = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Dni);
                });

            migrationBuilder.CreateTable(
                name: "Carritos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioDNI = table.Column<string>(type: "nvarchar(8)", nullable: false),
                    TipoCarrito = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carritos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carritos_Usuarios_UsuarioDNI",
                        column: x => x.UsuarioDNI,
                        principalTable: "Usuarios",
                        principalColumn: "Dni",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Compras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioDNI = table.Column<string>(type: "nvarchar(8)", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    FechaCompra = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Compras_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Compras_Usuarios_UsuarioDNI",
                        column: x => x.UsuarioDNI,
                        principalTable: "Usuarios",
                        principalColumn: "Dni",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemsCarrito",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarritoId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsCarrito", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemsCarrito_Carritos_CarritoId",
                        column: x => x.CarritoId,
                        principalTable: "Carritos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemsCarrito_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carritos_UsuarioDNI",
                table: "Carritos",
                column: "UsuarioDNI");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_ProductoId",
                table: "Compras",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_UsuarioDNI",
                table: "Compras",
                column: "UsuarioDNI");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsCarrito_CarritoId",
                table: "ItemsCarrito",
                column: "CarritoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsCarrito_ProductoId",
                table: "ItemsCarrito",
                column: "ProductoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Compras");

            migrationBuilder.DropTable(
                name: "ItemsCarrito");

            migrationBuilder.DropTable(
                name: "Carritos");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
