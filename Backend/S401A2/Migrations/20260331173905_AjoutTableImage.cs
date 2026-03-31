using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace S401A2.Migrations
{
    /// <inheritdoc />
    public partial class AjoutTableImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_t_e_article_art_art_id",
                schema: "public",
                table: "t_e_article_art");

            migrationBuilder.AlterColumn<string>(
                name: "lienvue360",
                schema: "public",
                table: "t_e_velo_vel",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "clr_id",
                schema: "public",
                table: "t_e_lignepanier_lig",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "tli_id",
                schema: "public",
                table: "t_e_lignepanier_lig",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "t_e_accessoire_acc",
                schema: "public",
                columns: table => new
                {
                    art_id = table.Column<int>(type: "integer", nullable: false),
                    acc_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    acc_taille_unique = table.Column<bool>(type: "boolean", nullable: false),
                    acc_materiaux = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    acc_dimensions = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    acc_caracteristiques = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_accessoire_acc", x => x.art_id);
                    table.ForeignKey(
                        name: "FK_t_e_accessoire_acc_t_e_article_art_art_id",
                        column: x => x.art_id,
                        principalSchema: "public",
                        principalTable: "t_e_article_art",
                        principalColumn: "art_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_image_img",
                schema: "public",
                columns: table => new
                {
                    img_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    img_chemin = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    art_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_image_img", x => x.img_id);
                    table.ForeignKey(
                        name: "FK_t_e_image_img_t_e_article_art_art_id",
                        column: x => x.art_id,
                        principalSchema: "public",
                        principalTable: "t_e_article_art",
                        principalColumn: "art_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_accessoire_taille_ata",
                schema: "public",
                columns: table => new
                {
                    AccessoiresArticleId = table.Column<int>(type: "integer", nullable: false),
                    TaillesIdTaille = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_j_accessoire_taille_ata", x => new { x.AccessoiresArticleId, x.TaillesIdTaille });
                    table.ForeignKey(
                        name: "FK_t_j_accessoire_taille_ata_t_e_accessoire_acc_AccessoiresArt~",
                        column: x => x.AccessoiresArticleId,
                        principalSchema: "public",
                        principalTable: "t_e_accessoire_acc",
                        principalColumn: "art_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_j_accessoire_taille_ata_t_s_taille_tli_TaillesIdTaille",
                        column: x => x.TaillesIdTaille,
                        principalSchema: "public",
                        principalTable: "t_s_taille_tli",
                        principalColumn: "tli_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_e_lignepanier_lig_clr_id",
                schema: "public",
                table: "t_e_lignepanier_lig",
                column: "clr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_lignepanier_lig_tli_id",
                schema: "public",
                table: "t_e_lignepanier_lig",
                column: "tli_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_article_art_art_id",
                schema: "public",
                table: "t_e_article_art",
                column: "art_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_accessoire_acc_art_id",
                schema: "public",
                table: "t_e_accessoire_acc",
                column: "art_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_image_img_art_id",
                schema: "public",
                table: "t_e_image_img",
                column: "art_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_accessoire_taille_ata_TaillesIdTaille",
                schema: "public",
                table: "t_j_accessoire_taille_ata",
                column: "TaillesIdTaille");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_lignepanier_lig_t_s_couleur_clr_clr_id",
                schema: "public",
                table: "t_e_lignepanier_lig",
                column: "clr_id",
                principalSchema: "public",
                principalTable: "t_s_couleur_clr",
                principalColumn: "clr_id");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_lignepanier_lig_t_s_taille_tli_tli_id",
                schema: "public",
                table: "t_e_lignepanier_lig",
                column: "tli_id",
                principalSchema: "public",
                principalTable: "t_s_taille_tli",
                principalColumn: "tli_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_e_lignepanier_lig_t_s_couleur_clr_clr_id",
                schema: "public",
                table: "t_e_lignepanier_lig");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_lignepanier_lig_t_s_taille_tli_tli_id",
                schema: "public",
                table: "t_e_lignepanier_lig");

            migrationBuilder.DropTable(
                name: "t_e_image_img",
                schema: "public");

            migrationBuilder.DropTable(
                name: "t_j_accessoire_taille_ata",
                schema: "public");

            migrationBuilder.DropTable(
                name: "t_e_accessoire_acc",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "IX_t_e_lignepanier_lig_clr_id",
                schema: "public",
                table: "t_e_lignepanier_lig");

            migrationBuilder.DropIndex(
                name: "IX_t_e_lignepanier_lig_tli_id",
                schema: "public",
                table: "t_e_lignepanier_lig");

            migrationBuilder.DropIndex(
                name: "IX_t_e_article_art_art_id",
                schema: "public",
                table: "t_e_article_art");

            migrationBuilder.DropColumn(
                name: "clr_id",
                schema: "public",
                table: "t_e_lignepanier_lig");

            migrationBuilder.DropColumn(
                name: "tli_id",
                schema: "public",
                table: "t_e_lignepanier_lig");

            migrationBuilder.AlterColumn<string>(
                name: "lienvue360",
                schema: "public",
                table: "t_e_velo_vel",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_article_art_art_id",
                schema: "public",
                table: "t_e_article_art",
                column: "art_id");
        }
    }
}
