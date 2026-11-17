using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApisCrud.Migrations;

/// <inheritdoc />
public partial class _20261117081606_CorregirEstatus : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<bool>(
            name: "Estatus", table: "Productos", type: "bit",
            nullable: false, defaultValue: true,
            oldClrType: typeof(bool), oldType: "bit");

        migrationBuilder.AlterColumn<bool>(
            name: "Estatus", table: "Usuarios", type: "bit",
            nullable: false, defaultValue: true,
            oldClrType: typeof(bool), oldType: "bit");

        migrationBuilder.AddColumn<bool>(
            name: "Estatus", table: "Resenas", type: "bit",
            nullable: false, defaultValue: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(name: "Estatus", table: "Resenas");
        // (opcional) revertir los defaults de Productos y Usuarios
    }
}
