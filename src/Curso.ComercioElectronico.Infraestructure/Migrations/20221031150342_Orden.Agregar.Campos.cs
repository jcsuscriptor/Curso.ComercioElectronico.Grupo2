using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Curso.ComercioElectronico.Infraestructure.Migrations
{
    public partial class OrdenAgregarCampos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Cantidad",
                table: "OrdenItem",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Observaciones",
                table: "OrdenItem",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Precio",
                table: "OrdenItem",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "Ordenes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha",
                table: "Ordenes",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaAnulacion",
                table: "Ordenes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observaciones",
                table: "Ordenes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Ordenes",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "OrdenItem");

            migrationBuilder.DropColumn(
                name: "Observaciones",
                table: "OrdenItem");

            migrationBuilder.DropColumn(
                name: "Precio",
                table: "OrdenItem");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Ordenes");

            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "Ordenes");

            migrationBuilder.DropColumn(
                name: "FechaAnulacion",
                table: "Ordenes");

            migrationBuilder.DropColumn(
                name: "Observaciones",
                table: "Ordenes");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Ordenes");
        }
    }
}
