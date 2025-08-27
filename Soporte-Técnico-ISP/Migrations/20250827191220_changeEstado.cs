using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Soporte_Técnico_ISP.Migrations
{
    /// <inheritdoc />
    public partial class changeEstado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "prioridad",
                table: "Casos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "prioridad",
                table: "Casos");
        }
    }
}
