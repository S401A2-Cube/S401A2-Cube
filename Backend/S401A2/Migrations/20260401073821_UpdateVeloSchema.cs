using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace S401A2.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVeloSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_e_velo_vel_t_e_article_art_idarticle",
                schema: "public",
                table: "t_e_velo_vel");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_velo_vel_t_e_modele_mod_idmodele",
                schema: "public",
                table: "t_e_velo_vel");

            migrationBuilder.RenameColumn(
                name: "lienvue360",
                schema: "public",
                table: "t_e_velo_vel",
                newName: "vel_lienvue360");

            migrationBuilder.RenameColumn(
                name: "idmodele",
                schema: "public",
                table: "t_e_velo_vel",
                newName: "mod_idmodele");

            migrationBuilder.RenameColumn(
                name: "idarticle",
                schema: "public",
                table: "t_e_velo_vel",
                newName: "art_idarticle");

            migrationBuilder.RenameColumn(
                name: "idVelo",
                schema: "public",
                table: "t_e_velo_vel",
                newName: "vel_idVelo");

            migrationBuilder.RenameIndex(
                name: "IX_t_e_velo_vel_idmodele",
                schema: "public",
                table: "t_e_velo_vel",
                newName: "IX_t_e_velo_vel_mod_idmodele");

            migrationBuilder.RenameIndex(
                name: "IX_t_e_velo_vel_idarticle",
                schema: "public",
                table: "t_e_velo_vel",
                newName: "IX_t_e_velo_vel_art_idarticle");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_velo_vel_t_e_article_art_art_idarticle",
                schema: "public",
                table: "t_e_velo_vel",
                column: "art_idarticle",
                principalSchema: "public",
                principalTable: "t_e_article_art",
                principalColumn: "art_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_velo_vel_t_e_modele_mod_mod_idmodele",
                schema: "public",
                table: "t_e_velo_vel",
                column: "mod_idmodele",
                principalSchema: "public",
                principalTable: "t_e_modele_mod",
                principalColumn: "mod_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_e_velo_vel_t_e_article_art_art_idarticle",
                schema: "public",
                table: "t_e_velo_vel");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_velo_vel_t_e_modele_mod_mod_idmodele",
                schema: "public",
                table: "t_e_velo_vel");

            migrationBuilder.RenameColumn(
                name: "vel_lienvue360",
                schema: "public",
                table: "t_e_velo_vel",
                newName: "lienvue360");

            migrationBuilder.RenameColumn(
                name: "mod_idmodele",
                schema: "public",
                table: "t_e_velo_vel",
                newName: "idmodele");

            migrationBuilder.RenameColumn(
                name: "art_idarticle",
                schema: "public",
                table: "t_e_velo_vel",
                newName: "idarticle");

            migrationBuilder.RenameColumn(
                name: "vel_idVelo",
                schema: "public",
                table: "t_e_velo_vel",
                newName: "idVelo");

            migrationBuilder.RenameIndex(
                name: "IX_t_e_velo_vel_mod_idmodele",
                schema: "public",
                table: "t_e_velo_vel",
                newName: "IX_t_e_velo_vel_idmodele");

            migrationBuilder.RenameIndex(
                name: "IX_t_e_velo_vel_art_idarticle",
                schema: "public",
                table: "t_e_velo_vel",
                newName: "IX_t_e_velo_vel_idarticle");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_velo_vel_t_e_article_art_idarticle",
                schema: "public",
                table: "t_e_velo_vel",
                column: "idarticle",
                principalSchema: "public",
                principalTable: "t_e_article_art",
                principalColumn: "art_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_velo_vel_t_e_modele_mod_idmodele",
                schema: "public",
                table: "t_e_velo_vel",
                column: "idmodele",
                principalSchema: "public",
                principalTable: "t_e_modele_mod",
                principalColumn: "mod_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
