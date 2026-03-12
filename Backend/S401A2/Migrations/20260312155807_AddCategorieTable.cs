using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace S401A2.Migrations
{
    /// <inheritdoc />
    public partial class AddCategorieTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "cat_id",
                schema: "public",
                table: "t_e_article_art",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "t_e_categorie_cat",
                schema: "public",
                columns: table => new
                {
                    cat_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cat_nom = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_categorie_cat", x => x.cat_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_e_article_art_cat_id",
                schema: "public",
                table: "t_e_article_art",
                column: "cat_id");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_article_art_t_e_categorie_cat_cat_id",
                schema: "public",
                table: "t_e_article_art",
                column: "cat_id",
                principalSchema: "public",
                principalTable: "t_e_categorie_cat",
                principalColumn: "cat_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_e_article_art_t_e_categorie_cat_cat_id",
                schema: "public",
                table: "t_e_article_art");

            migrationBuilder.DropTable(
                name: "t_e_categorie_cat",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "IX_t_e_article_art_cat_id",
                schema: "public",
                table: "t_e_article_art");

            migrationBuilder.DropColumn(
                name: "cat_id",
                schema: "public",
                table: "t_e_article_art");
        }
    }
}
