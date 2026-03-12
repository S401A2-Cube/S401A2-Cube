using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace S401A2.Migrations
{
    /// <inheritdoc />
    public partial class InitialisationArticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "t_e_article_art",
                schema: "public",
                columns: table => new
                {
                    art_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    art_reference = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    art_nom = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    art_description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    art_prix = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    art_poids = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    art_qte_stock = table.Column<int>(type: "integer", nullable: false),
                    art_annee = table.Column<int>(type: "integer", nullable: false),
                    art_dispo_en_ligne = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_article_art", x => x.art_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_e_article_art",
                schema: "public");
        }
    }
}
