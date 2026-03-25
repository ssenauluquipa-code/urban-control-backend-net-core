using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UrbanControl.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddSistemaPermisosSofisticado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Modulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Icono = table.Column<string>(type: "text", nullable: false),
                    Orden = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modulos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoPermisos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Slug = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPermisos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Submodulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ModuloId = table.Column<int>(type: "integer", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    RutaAngular = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submodulos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Submodulos_Modulos_ModuloId",
                        column: x => x.ModuloId,
                        principalTable: "Modulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CapacidadSubmodulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubmoduloId = table.Column<int>(type: "integer", nullable: false),
                    TipoPermisoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapacidadSubmodulos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CapacidadSubmodulos_Submodulos_SubmoduloId",
                        column: x => x.SubmoduloId,
                        principalTable: "Submodulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CapacidadSubmodulos_TipoPermisos_TipoPermisoId",
                        column: x => x.TipoPermisoId,
                        principalTable: "TipoPermisos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PermisosRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RolId = table.Column<int>(type: "integer", nullable: false),
                    CapacidadSubmoduloId = table.Column<int>(type: "integer", nullable: false),
                    Concedido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermisosRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermisosRoles_CapacidadSubmodulos_CapacidadSubmoduloId",
                        column: x => x.CapacidadSubmoduloId,
                        principalTable: "CapacidadSubmodulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermisosRoles_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Modulos",
                columns: new[] { "Id", "Icono", "Nombre", "Orden" },
                values: new object[,]
                {
                    { 1, "pi pi-box", "Inventario", 1 },
                    { 2, "pi pi-shopping-cart", "Ventas", 2 }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Descripcion", "Nombre" },
                values: new object[,]
                {
                    { 1, "Acceso total", "Administrador" },
                    { 2, "Solo ventas y clientes", "Vendedor" }
                });

            migrationBuilder.InsertData(
                table: "TipoPermisos",
                columns: new[] { "Id", "Nombre", "Slug" },
                values: new object[,]
                {
                    { 1, "Ver", "view" },
                    { 2, "Crear", "create" },
                    { 3, "Editar", "edit" },
                    { 4, "Eliminar", "delete" },
                    { 5, "Imprimir PDF", "pdf" }
                });

            migrationBuilder.InsertData(
                table: "Submodulos",
                columns: new[] { "Id", "ModuloId", "Nombre", "RutaAngular" },
                values: new object[,]
                {
                    { 1, 1, "Proyectos", "/proyectos" },
                    { 2, 1, "Lotes", "/lotes" },
                    { 3, 2, "Reservas", "/reservas" }
                });

            migrationBuilder.InsertData(
                table: "CapacidadSubmodulos",
                columns: new[] { "Id", "SubmoduloId", "TipoPermisoId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 1, 3 },
                    { 4, 2, 1 },
                    { 5, 2, 2 },
                    { 6, 2, 3 },
                    { 7, 2, 4 },
                    { 8, 3, 1 },
                    { 9, 3, 2 },
                    { 10, 3, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CapacidadSubmodulos_SubmoduloId_TipoPermisoId",
                table: "CapacidadSubmodulos",
                columns: new[] { "SubmoduloId", "TipoPermisoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CapacidadSubmodulos_TipoPermisoId",
                table: "CapacidadSubmodulos",
                column: "TipoPermisoId");

            migrationBuilder.CreateIndex(
                name: "IX_PermisosRoles_CapacidadSubmoduloId",
                table: "PermisosRoles",
                column: "CapacidadSubmoduloId");

            migrationBuilder.CreateIndex(
                name: "IX_PermisosRoles_RolId_CapacidadSubmoduloId",
                table: "PermisosRoles",
                columns: new[] { "RolId", "CapacidadSubmoduloId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Submodulos_ModuloId",
                table: "Submodulos",
                column: "ModuloId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermisosRoles");

            migrationBuilder.DropTable(
                name: "CapacidadSubmodulos");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Submodulos");

            migrationBuilder.DropTable(
                name: "TipoPermisos");

            migrationBuilder.DropTable(
                name: "Modulos");
        }
    }
}
