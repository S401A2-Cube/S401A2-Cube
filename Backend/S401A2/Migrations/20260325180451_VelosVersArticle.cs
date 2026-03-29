using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace S401A2.Migrations
{
    /// <inheritdoc />
    public partial class VelosVersArticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_t_e_velo_vel_idarticle",
                schema: "public",
                table: "t_e_velo_vel",
                column: "idarticle");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_velo_vel_t_e_article_art_idarticle",
                schema: "public",
                table: "t_e_velo_vel",
                column: "idarticle",
                principalSchema: "public",
                principalTable: "t_e_article_art",
                principalColumn: "art_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_e_velo_vel_t_e_article_art_idarticle",
                schema: "public",
                table: "t_e_velo_vel");

            migrationBuilder.DropIndex(
                name: "IX_t_e_velo_vel_idarticle",
                schema: "public",
                table: "t_e_velo_vel");
        }
    }
}
