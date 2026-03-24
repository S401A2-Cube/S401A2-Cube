using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace S401A2.Migrations
{
    /// <inheritdoc />
    public partial class fixedAutoTableNaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CadreVelo_t_e_velo_vel_VelosIdVelo",
                schema: "public",
                table: "CadreVelo");

            migrationBuilder.DropForeignKey(
                name: "FK_CadreVelo_t_s_cadre_cad_CadresIdMateriau",
                schema: "public",
                table: "CadreVelo");

            migrationBuilder.DropForeignKey(
                name: "FK_CouleurVelo_t_e_velo_vel_VelosIdVelo",
                schema: "public",
                table: "CouleurVelo");

            migrationBuilder.DropForeignKey(
                name: "FK_CouleurVelo_t_s_couleur_clr_CouleursIdCouleur",
                schema: "public",
                table: "CouleurVelo");

            migrationBuilder.DropForeignKey(
                name: "FK_GeometrieVelo_t_e_velo_vel_VelosIdVelo",
                schema: "public",
                table: "GeometrieVelo");

            migrationBuilder.DropForeignKey(
                name: "FK_GeometrieVelo_t_s_geometrie_geo_GeometriesIdGeometrie",
                schema: "public",
                table: "GeometrieVelo");

            migrationBuilder.DropForeignKey(
                name: "FK_MillesimeVelo_t_e_velo_vel_VelosIdVelo",
                schema: "public",
                table: "MillesimeVelo");

            migrationBuilder.DropForeignKey(
                name: "FK_MillesimeVelo_t_s_millesime_mls_MillesimesIdMillesime",
                schema: "public",
                table: "MillesimeVelo");

            migrationBuilder.DropForeignKey(
                name: "FK_TailleVelo_t_e_velo_vel_VelosIdVelo",
                schema: "public",
                table: "TailleVelo");

            migrationBuilder.DropForeignKey(
                name: "FK_TailleVelo_t_s_taille_tli_TaillesIdTaille",
                schema: "public",
                table: "TailleVelo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TailleVelo",
                schema: "public",
                table: "TailleVelo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MillesimeVelo",
                schema: "public",
                table: "MillesimeVelo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GeometrieVelo",
                schema: "public",
                table: "GeometrieVelo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CouleurVelo",
                schema: "public",
                table: "CouleurVelo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CadreVelo",
                schema: "public",
                table: "CadreVelo");

            migrationBuilder.RenameTable(
                name: "TailleVelo",
                schema: "public",
                newName: "t_j_velo_taille_vta",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "MillesimeVelo",
                schema: "public",
                newName: "t_j_velo_millesime_vmi",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "GeometrieVelo",
                schema: "public",
                newName: "t_j_velo_geometrie_vge",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "CouleurVelo",
                schema: "public",
                newName: "t_j_velo_couleur_vco",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "CadreVelo",
                schema: "public",
                newName: "t_j_velo_cadre_vca",
                newSchema: "public");

            migrationBuilder.RenameIndex(
                name: "IX_TailleVelo_VelosIdVelo",
                schema: "public",
                table: "t_j_velo_taille_vta",
                newName: "IX_t_j_velo_taille_vta_VelosIdVelo");

            migrationBuilder.RenameIndex(
                name: "IX_MillesimeVelo_VelosIdVelo",
                schema: "public",
                table: "t_j_velo_millesime_vmi",
                newName: "IX_t_j_velo_millesime_vmi_VelosIdVelo");

            migrationBuilder.RenameIndex(
                name: "IX_GeometrieVelo_VelosIdVelo",
                schema: "public",
                table: "t_j_velo_geometrie_vge",
                newName: "IX_t_j_velo_geometrie_vge_VelosIdVelo");

            migrationBuilder.RenameIndex(
                name: "IX_CouleurVelo_VelosIdVelo",
                schema: "public",
                table: "t_j_velo_couleur_vco",
                newName: "IX_t_j_velo_couleur_vco_VelosIdVelo");

            migrationBuilder.RenameIndex(
                name: "IX_CadreVelo_VelosIdVelo",
                schema: "public",
                table: "t_j_velo_cadre_vca",
                newName: "IX_t_j_velo_cadre_vca_VelosIdVelo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_j_velo_taille_vta",
                schema: "public",
                table: "t_j_velo_taille_vta",
                columns: new[] { "TaillesIdTaille", "VelosIdVelo" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_j_velo_millesime_vmi",
                schema: "public",
                table: "t_j_velo_millesime_vmi",
                columns: new[] { "MillesimesIdMillesime", "VelosIdVelo" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_j_velo_geometrie_vge",
                schema: "public",
                table: "t_j_velo_geometrie_vge",
                columns: new[] { "GeometriesIdGeometrie", "VelosIdVelo" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_j_velo_couleur_vco",
                schema: "public",
                table: "t_j_velo_couleur_vco",
                columns: new[] { "CouleursIdCouleur", "VelosIdVelo" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_t_j_velo_cadre_vca",
                schema: "public",
                table: "t_j_velo_cadre_vca",
                columns: new[] { "CadresIdMateriau", "VelosIdVelo" });

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_velo_cadre_vca_t_e_velo_vel_VelosIdVelo",
                schema: "public",
                table: "t_j_velo_cadre_vca",
                column: "VelosIdVelo",
                principalSchema: "public",
                principalTable: "t_e_velo_vel",
                principalColumn: "idVelo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_velo_cadre_vca_t_s_cadre_cad_CadresIdMateriau",
                schema: "public",
                table: "t_j_velo_cadre_vca",
                column: "CadresIdMateriau",
                principalSchema: "public",
                principalTable: "t_s_cadre_cad",
                principalColumn: "cad_id_materiau",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_velo_couleur_vco_t_e_velo_vel_VelosIdVelo",
                schema: "public",
                table: "t_j_velo_couleur_vco",
                column: "VelosIdVelo",
                principalSchema: "public",
                principalTable: "t_e_velo_vel",
                principalColumn: "idVelo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_velo_couleur_vco_t_s_couleur_clr_CouleursIdCouleur",
                schema: "public",
                table: "t_j_velo_couleur_vco",
                column: "CouleursIdCouleur",
                principalSchema: "public",
                principalTable: "t_s_couleur_clr",
                principalColumn: "clr_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_velo_geometrie_vge_t_e_velo_vel_VelosIdVelo",
                schema: "public",
                table: "t_j_velo_geometrie_vge",
                column: "VelosIdVelo",
                principalSchema: "public",
                principalTable: "t_e_velo_vel",
                principalColumn: "idVelo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_velo_geometrie_vge_t_s_geometrie_geo_GeometriesIdGeomet~",
                schema: "public",
                table: "t_j_velo_geometrie_vge",
                column: "GeometriesIdGeometrie",
                principalSchema: "public",
                principalTable: "t_s_geometrie_geo",
                principalColumn: "geo_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_velo_millesime_vmi_t_e_velo_vel_VelosIdVelo",
                schema: "public",
                table: "t_j_velo_millesime_vmi",
                column: "VelosIdVelo",
                principalSchema: "public",
                principalTable: "t_e_velo_vel",
                principalColumn: "idVelo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_velo_millesime_vmi_t_s_millesime_mls_MillesimesIdMilles~",
                schema: "public",
                table: "t_j_velo_millesime_vmi",
                column: "MillesimesIdMillesime",
                principalSchema: "public",
                principalTable: "t_s_millesime_mls",
                principalColumn: "mls_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_velo_taille_vta_t_e_velo_vel_VelosIdVelo",
                schema: "public",
                table: "t_j_velo_taille_vta",
                column: "VelosIdVelo",
                principalSchema: "public",
                principalTable: "t_e_velo_vel",
                principalColumn: "idVelo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_velo_taille_vta_t_s_taille_tli_TaillesIdTaille",
                schema: "public",
                table: "t_j_velo_taille_vta",
                column: "TaillesIdTaille",
                principalSchema: "public",
                principalTable: "t_s_taille_tli",
                principalColumn: "tli_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_j_velo_cadre_vca_t_e_velo_vel_VelosIdVelo",
                schema: "public",
                table: "t_j_velo_cadre_vca");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_velo_cadre_vca_t_s_cadre_cad_CadresIdMateriau",
                schema: "public",
                table: "t_j_velo_cadre_vca");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_velo_couleur_vco_t_e_velo_vel_VelosIdVelo",
                schema: "public",
                table: "t_j_velo_couleur_vco");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_velo_couleur_vco_t_s_couleur_clr_CouleursIdCouleur",
                schema: "public",
                table: "t_j_velo_couleur_vco");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_velo_geometrie_vge_t_e_velo_vel_VelosIdVelo",
                schema: "public",
                table: "t_j_velo_geometrie_vge");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_velo_geometrie_vge_t_s_geometrie_geo_GeometriesIdGeomet~",
                schema: "public",
                table: "t_j_velo_geometrie_vge");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_velo_millesime_vmi_t_e_velo_vel_VelosIdVelo",
                schema: "public",
                table: "t_j_velo_millesime_vmi");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_velo_millesime_vmi_t_s_millesime_mls_MillesimesIdMilles~",
                schema: "public",
                table: "t_j_velo_millesime_vmi");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_velo_taille_vta_t_e_velo_vel_VelosIdVelo",
                schema: "public",
                table: "t_j_velo_taille_vta");

            migrationBuilder.DropForeignKey(
                name: "FK_t_j_velo_taille_vta_t_s_taille_tli_TaillesIdTaille",
                schema: "public",
                table: "t_j_velo_taille_vta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_j_velo_taille_vta",
                schema: "public",
                table: "t_j_velo_taille_vta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_j_velo_millesime_vmi",
                schema: "public",
                table: "t_j_velo_millesime_vmi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_j_velo_geometrie_vge",
                schema: "public",
                table: "t_j_velo_geometrie_vge");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_j_velo_couleur_vco",
                schema: "public",
                table: "t_j_velo_couleur_vco");

            migrationBuilder.DropPrimaryKey(
                name: "PK_t_j_velo_cadre_vca",
                schema: "public",
                table: "t_j_velo_cadre_vca");

            migrationBuilder.RenameTable(
                name: "t_j_velo_taille_vta",
                schema: "public",
                newName: "TailleVelo",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "t_j_velo_millesime_vmi",
                schema: "public",
                newName: "MillesimeVelo",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "t_j_velo_geometrie_vge",
                schema: "public",
                newName: "GeometrieVelo",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "t_j_velo_couleur_vco",
                schema: "public",
                newName: "CouleurVelo",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "t_j_velo_cadre_vca",
                schema: "public",
                newName: "CadreVelo",
                newSchema: "public");

            migrationBuilder.RenameIndex(
                name: "IX_t_j_velo_taille_vta_VelosIdVelo",
                schema: "public",
                table: "TailleVelo",
                newName: "IX_TailleVelo_VelosIdVelo");

            migrationBuilder.RenameIndex(
                name: "IX_t_j_velo_millesime_vmi_VelosIdVelo",
                schema: "public",
                table: "MillesimeVelo",
                newName: "IX_MillesimeVelo_VelosIdVelo");

            migrationBuilder.RenameIndex(
                name: "IX_t_j_velo_geometrie_vge_VelosIdVelo",
                schema: "public",
                table: "GeometrieVelo",
                newName: "IX_GeometrieVelo_VelosIdVelo");

            migrationBuilder.RenameIndex(
                name: "IX_t_j_velo_couleur_vco_VelosIdVelo",
                schema: "public",
                table: "CouleurVelo",
                newName: "IX_CouleurVelo_VelosIdVelo");

            migrationBuilder.RenameIndex(
                name: "IX_t_j_velo_cadre_vca_VelosIdVelo",
                schema: "public",
                table: "CadreVelo",
                newName: "IX_CadreVelo_VelosIdVelo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TailleVelo",
                schema: "public",
                table: "TailleVelo",
                columns: new[] { "TaillesIdTaille", "VelosIdVelo" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MillesimeVelo",
                schema: "public",
                table: "MillesimeVelo",
                columns: new[] { "MillesimesIdMillesime", "VelosIdVelo" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_GeometrieVelo",
                schema: "public",
                table: "GeometrieVelo",
                columns: new[] { "GeometriesIdGeometrie", "VelosIdVelo" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CouleurVelo",
                schema: "public",
                table: "CouleurVelo",
                columns: new[] { "CouleursIdCouleur", "VelosIdVelo" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CadreVelo",
                schema: "public",
                table: "CadreVelo",
                columns: new[] { "CadresIdMateriau", "VelosIdVelo" });

            migrationBuilder.AddForeignKey(
                name: "FK_CadreVelo_t_e_velo_vel_VelosIdVelo",
                schema: "public",
                table: "CadreVelo",
                column: "VelosIdVelo",
                principalSchema: "public",
                principalTable: "t_e_velo_vel",
                principalColumn: "idVelo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CadreVelo_t_s_cadre_cad_CadresIdMateriau",
                schema: "public",
                table: "CadreVelo",
                column: "CadresIdMateriau",
                principalSchema: "public",
                principalTable: "t_s_cadre_cad",
                principalColumn: "cad_id_materiau",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CouleurVelo_t_e_velo_vel_VelosIdVelo",
                schema: "public",
                table: "CouleurVelo",
                column: "VelosIdVelo",
                principalSchema: "public",
                principalTable: "t_e_velo_vel",
                principalColumn: "idVelo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CouleurVelo_t_s_couleur_clr_CouleursIdCouleur",
                schema: "public",
                table: "CouleurVelo",
                column: "CouleursIdCouleur",
                principalSchema: "public",
                principalTable: "t_s_couleur_clr",
                principalColumn: "clr_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GeometrieVelo_t_e_velo_vel_VelosIdVelo",
                schema: "public",
                table: "GeometrieVelo",
                column: "VelosIdVelo",
                principalSchema: "public",
                principalTable: "t_e_velo_vel",
                principalColumn: "idVelo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GeometrieVelo_t_s_geometrie_geo_GeometriesIdGeometrie",
                schema: "public",
                table: "GeometrieVelo",
                column: "GeometriesIdGeometrie",
                principalSchema: "public",
                principalTable: "t_s_geometrie_geo",
                principalColumn: "geo_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MillesimeVelo_t_e_velo_vel_VelosIdVelo",
                schema: "public",
                table: "MillesimeVelo",
                column: "VelosIdVelo",
                principalSchema: "public",
                principalTable: "t_e_velo_vel",
                principalColumn: "idVelo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MillesimeVelo_t_s_millesime_mls_MillesimesIdMillesime",
                schema: "public",
                table: "MillesimeVelo",
                column: "MillesimesIdMillesime",
                principalSchema: "public",
                principalTable: "t_s_millesime_mls",
                principalColumn: "mls_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TailleVelo_t_e_velo_vel_VelosIdVelo",
                schema: "public",
                table: "TailleVelo",
                column: "VelosIdVelo",
                principalSchema: "public",
                principalTable: "t_e_velo_vel",
                principalColumn: "idVelo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TailleVelo_t_s_taille_tli_TaillesIdTaille",
                schema: "public",
                table: "TailleVelo",
                column: "TaillesIdTaille",
                principalSchema: "public",
                principalTable: "t_s_taille_tli",
                principalColumn: "tli_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
