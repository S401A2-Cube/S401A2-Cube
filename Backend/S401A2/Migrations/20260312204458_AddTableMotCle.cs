using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace S401A2.Migrations
{
    /// <inheritdoc />
    public partial class AddTableMotCle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_e_motcle_mcl",
                schema: "public",
                columns: table => new
                {
                    mcl_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mcl_nom = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_motcle_mcl", x => x.mcl_id);
                });

            migrationBuilder.CreateTable(
                name: "t_j_article_motcle_amc",
                schema: "public",
                columns: table => new
                {
                    ArticlesArticleId = table.Column<int>(type: "integer", nullable: false),
                    MotsClesMotCleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_j_article_motcle_amc", x => new { x.ArticlesArticleId, x.MotsClesMotCleId });
                    table.ForeignKey(
                        name: "FK_t_j_article_motcle_amc_t_e_article_art_ArticlesArticleId",
                        column: x => x.ArticlesArticleId,
                        principalSchema: "public",
                        principalTable: "t_e_article_art",
                        principalColumn: "art_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_j_article_motcle_amc_t_e_motcle_mcl_MotsClesMotCleId",
                        column: x => x.MotsClesMotCleId,
                        principalSchema: "public",
                        principalTable: "t_e_motcle_mcl",
                        principalColumn: "mcl_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_j_article_motcle_amc_MotsClesMotCleId",
                schema: "public",
                table: "t_j_article_motcle_amc",
                column: "MotsClesMotCleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_j_article_motcle_amc",
                schema: "public");

            migrationBuilder.DropTable(
                name: "t_e_motcle_mcl",
                schema: "public");
        }
    }
}
