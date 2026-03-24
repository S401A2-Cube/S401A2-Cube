using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace S401A2.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "client_datenaissance",
                schema: "public",
                table: "t_e_client_cli",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<int>(
                name: "ArticleCommandeId",
                schema: "public",
                table: "t_e_article_art",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "t_s_millesime_mls",
                schema: "public",
                columns: table => new
                {
                    mls_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mls_annee = table.Column<int>(type: "integer", nullable: false),
                    mls_description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_s_millesime_mls", x => x.mls_id);
                });

            migrationBuilder.CreateTable(
                name: "MillesimeVelo",
                schema: "public",
                columns: table => new
                {
                    MillesimesIdMillesime = table.Column<int>(type: "integer", nullable: false),
                    VelosIdVelo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MillesimeVelo", x => new { x.MillesimesIdMillesime, x.VelosIdVelo });
                    table.ForeignKey(
                        name: "FK_MillesimeVelo_t_e_velo_vel_VelosIdVelo",
                        column: x => x.VelosIdVelo,
                        principalSchema: "public",
                        principalTable: "t_e_velo_vel",
                        principalColumn: "idVelo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MillesimeVelo_t_s_millesime_mls_MillesimesIdMillesime",
                        column: x => x.MillesimesIdMillesime,
                        principalSchema: "public",
                        principalTable: "t_s_millesime_mls",
                        principalColumn: "mls_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_e_article_art_ArticleCommandeId",
                schema: "public",
                table: "t_e_article_art",
                column: "ArticleCommandeId");

            migrationBuilder.CreateIndex(
                name: "IX_MillesimeVelo_VelosIdVelo",
                schema: "public",
                table: "MillesimeVelo",
                column: "VelosIdVelo");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_article_art_t_e_commande_com_ArticleCommandeId",
                schema: "public",
                table: "t_e_article_art",
                column: "ArticleCommandeId",
                principalSchema: "public",
                principalTable: "t_e_commande_com",
                principalColumn: "com_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_e_article_art_t_e_commande_com_ArticleCommandeId",
                schema: "public",
                table: "t_e_article_art");

            migrationBuilder.DropTable(
                name: "MillesimeVelo",
                schema: "public");

            migrationBuilder.DropTable(
                name: "t_s_millesime_mls",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "IX_t_e_article_art_ArticleCommandeId",
                schema: "public",
                table: "t_e_article_art");

            migrationBuilder.DropColumn(
                name: "ArticleCommandeId",
                schema: "public",
                table: "t_e_article_art");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "client_datenaissance",
                schema: "public",
                table: "t_e_client_cli",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }
    }
}
