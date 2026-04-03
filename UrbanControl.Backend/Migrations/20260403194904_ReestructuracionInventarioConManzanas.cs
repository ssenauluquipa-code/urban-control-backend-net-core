using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrbanControl.Backend.Migrations
{
    /// <inheritdoc />
    public partial class ReestructuracionInventarioConManzanas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lotes_Proyectos_ProyectoId",
                table: "Lotes");

            migrationBuilder.DropColumn(
                name: "Manzana",
                table: "Lotes");

            migrationBuilder.RenameColumn(
                name: "ProyectoId",
                table: "Lotes",
                newName: "ManzanaId");

            migrationBuilder.RenameIndex(
                name: "IX_Lotes_ProyectoId",
                table: "Lotes",
                newName: "IX_Lotes_ManzanaId");

            migrationBuilder.AlterColumn<decimal>(
                name: "PrecioBaseM2",
                table: "Proyectos",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<string>(
                name: "ConfigMapa",
                table: "Proyectos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlImagenPlano",
                table: "Proyectos",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "SuperficieM2",
                table: "Lotes",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Lotes",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Geometria",
                table: "Lotes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MapCode",
                table: "Lotes",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Manzanas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CodigoManzana = table.Column<string>(type: "text", nullable: false),
                    Geometria = table.Column<string>(type: "text", nullable: true),
                    ProyectoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manzanas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Manzanas_Proyectos_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lotes_MapCode",
                table: "Lotes",
                column: "MapCode",
                unique: true,
                filter: "\"MapCode\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Manzanas_ProyectoId",
                table: "Manzanas",
                column: "ProyectoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lotes_Manzanas_ManzanaId",
                table: "Lotes",
                column: "ManzanaId",
                principalTable: "Manzanas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lotes_Manzanas_ManzanaId",
                table: "Lotes");

            migrationBuilder.DropTable(
                name: "Manzanas");

            migrationBuilder.DropIndex(
                name: "IX_Lotes_MapCode",
                table: "Lotes");

            migrationBuilder.DropColumn(
                name: "ConfigMapa",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "UrlImagenPlano",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "Geometria",
                table: "Lotes");

            migrationBuilder.DropColumn(
                name: "MapCode",
                table: "Lotes");

            migrationBuilder.RenameColumn(
                name: "ManzanaId",
                table: "Lotes",
                newName: "ProyectoId");

            migrationBuilder.RenameIndex(
                name: "IX_Lotes_ManzanaId",
                table: "Lotes",
                newName: "IX_Lotes_ProyectoId");

            migrationBuilder.AlterColumn<decimal>(
                name: "PrecioBaseM2",
                table: "Proyectos",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "SuperficieM2",
                table: "Lotes",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Lotes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "Manzana",
                table: "Lotes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Lotes_Proyectos_ProyectoId",
                table: "Lotes",
                column: "ProyectoId",
                principalTable: "Proyectos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
