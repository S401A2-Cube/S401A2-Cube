using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace S401A2.Migrations
{
    /// <inheritdoc />
    public partial class ModifLignePaniers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "com_id",
                schema: "public",
                table: "t_e_lignepanier_lig",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "com_id",
                schema: "public",
                table: "t_e_lignepanier_lig",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
