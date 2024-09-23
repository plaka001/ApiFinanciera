using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Contrasena = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    IntentosLogin = table.Column<int>(type: "int", maxLength: 3, nullable: true),
                    FechaBloqueoLogin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TokenCorreoConfirmar = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    ConfirmadoCorreo = table.Column<bool>(type: "bit", nullable: false),
                    CorreoConfirmacionEstado = table.Column<int>(type: "int", nullable: false),
                    FechaExpiraToken = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_CorreoElectronico",
                table: "Usuarios",
                column: "CorreoElectronico",
                unique: true,
                filter: "[CorreoElectronico] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
