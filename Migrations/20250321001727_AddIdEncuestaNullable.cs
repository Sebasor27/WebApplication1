using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class AddIdEncuestaNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "fecha_creacion",
                table: "usuarios",
                type: "datetime",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(byte[]),
                oldType: "timestamp",
                oldDefaultValueSql: "current_timestamp()");

            migrationBuilder.AddColumn<int>(
                name: "id_encuesta",
                table: "resumen_ice",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "id_encuesta",
                table: "resultados_ice",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "id_encuesta",
                table: "encuestas_ice",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "id_encuesta",
                table: "encuestas_ice",
                column: "id_encuesta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "id_encuesta",
                table: "encuestas_ice");

            migrationBuilder.DropColumn(
                name: "id_encuesta",
                table: "resumen_ice");

            migrationBuilder.DropColumn(
                name: "id_encuesta",
                table: "resultados_ice");

            migrationBuilder.DropColumn(
                name: "id_encuesta",
                table: "encuestas_ice");

            migrationBuilder.AlterColumn<byte[]>(
                name: "fecha_creacion",
                table: "usuarios",
                type: "timestamp",
                nullable: false,
                defaultValueSql: "current_timestamp()",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "current_timestamp()");
        }
    }
}
