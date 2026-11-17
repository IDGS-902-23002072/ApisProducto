using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApisCrud.Migrations;

/// <inheritdoc />
public partial class _20261117051311_CrearSistemaProductos : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Productos",
            columns: table => new
            {
                IdProducto = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PrecioUnitario = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: false),
                Estatus = table.Column<bool>(type: "bit", nullable: false),
                Existencias = table.Column<int>(type: "int", nullable: false),
                Tipo = table.Column<int>(type: "int", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Productos", x => x.IdProducto);
            });

        migrationBuilder.CreateTable(
            name: "Usuarios",
            columns: table => new
            {
                IdUsuario = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Correo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Contrasena = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Estatus = table.Column<bool>(type: "bit", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
            });

        migrationBuilder.CreateTable(
            name: "Resenas",
            columns: table => new
            {
                IdResena = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                IdProducto = table.Column<int>(type: "int", nullable: false),
                IdUsuario = table.Column<int>(type: "int", nullable: false),
                Calificacion = table.Column<int>(type: "int", nullable: false),
                Comentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Resenas", x => x.IdResena);
                table.ForeignKey(
                    name: "FK_Resenas_Productos_IdProducto",
                    column: x => x.IdProducto,
                    principalTable: "Productos",
                    principalColumn: "IdProducto",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_Resenas_Usuarios_IdUsuario",
                    column: x => x.IdUsuario,
                    principalTable: "Usuarios",
                    principalColumn: "IdUsuario",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Resenas_IdProducto",
            table: "Resenas",
            column: "IdProducto");

        migrationBuilder.CreateIndex(
            name: "IX_Resenas_IdUsuario",
            table: "Resenas",
            column: "IdUsuario");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Resenas");

        migrationBuilder.DropTable(
            name: "Productos");

        migrationBuilder.DropTable(
            name: "Usuarios");
    }
}
