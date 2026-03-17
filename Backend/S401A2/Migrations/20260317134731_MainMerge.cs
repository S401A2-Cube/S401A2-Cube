using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace S401A2.Migrations
{
    /// <inheritdoc />
    public partial class MainMerge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_e_adresse_adr",
                schema: "public",
                columns: table => new
                {
                    adr_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    adr_rue = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    adr_codepostale = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    adr_ville = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    adr_pays = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_adresse_adr", x => x.adr_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_civilite_civ",
                schema: "public",
                columns: table => new
                {
                    civ_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    div_nom = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_civilite_civ", x => x.civ_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_modele_mod",
                schema: "public",
                columns: table => new
                {
                    mod_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mod_nom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_modele_mod", x => x.mod_id);
                });

            migrationBuilder.CreateTable(
                name: "t_s_cadre_cad",
                schema: "public",
                columns: table => new
                {
                    cad_id_materiau = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cad_nom_mat = table.Column<string>(type: "text", nullable: false),
                    cad_forme = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_s_cadre_cad", x => x.cad_id_materiau);
                });

            migrationBuilder.CreateTable(
                name: "t_s_couleur_clr",
                schema: "public",
                columns: table => new
                {
                    clr_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    clr_nom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    clr_effet_peinture = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_s_couleur_clr", x => x.clr_id);
                });

            migrationBuilder.CreateTable(
                name: "t_s_geometrie_geo",
                schema: "public",
                columns: table => new
                {
                    geo_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    geo_nom_piece = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    geo_taille_piece = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_s_geometrie_geo", x => x.geo_id);
                });

            migrationBuilder.CreateTable(
                name: "t_s_taille_tli",
                schema: "public",
                columns: table => new
                {
                    tli_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tli_libelle = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_s_taille_tli", x => x.tli_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_client_cli",
                schema: "public",
                columns: table => new
                {
                    cli_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    civ_id = table.Column<int>(type: "integer", nullable: false),
                    cli_nom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    cli_prenom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    cli_email = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: false),
                    cli_mdp = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    client_datenaissance = table.Column<DateOnly>(type: "date", nullable: false),
                    cli_role = table.Column<int>(type: "integer", nullable: false),
                    adr_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_client_cli", x => x.cli_id);
                    table.ForeignKey(
                        name: "FK_t_e_client_cli_t_e_adresse_adr_adr_id",
                        column: x => x.adr_id,
                        principalSchema: "public",
                        principalTable: "t_e_adresse_adr",
                        principalColumn: "adr_id");
                    table.ForeignKey(
                        name: "FK_t_e_client_cli_t_e_civilite_civ_civ_id",
                        column: x => x.civ_id,
                        principalSchema: "public",
                        principalTable: "t_e_civilite_civ",
                        principalColumn: "civ_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_velo_vel",
                schema: "public",
                columns: table => new
                {
                    idVelo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idarticle = table.Column<int>(type: "integer", nullable: false),
                    lienvue360 = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    idmodele = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_velo_vel", x => x.idVelo);
                    table.ForeignKey(
                        name: "FK_t_e_velo_vel_t_e_modele_mod_idmodele",
                        column: x => x.idmodele,
                        principalSchema: "public",
                        principalTable: "t_e_modele_mod",
                        principalColumn: "mod_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_commande_com",
                schema: "public",
                columns: table => new
                {
                    com_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    com_livraison = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    adr_idlivraison = table.Column<int>(type: "integer", nullable: false),
                    adr_idfacturation = table.Column<int>(type: "integer", nullable: true),
                    cli_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_commande_com", x => x.com_id);
                    table.ForeignKey(
                        name: "FK_t_e_commande_com_t_e_adresse_adr_adr_idfacturation",
                        column: x => x.adr_idfacturation,
                        principalSchema: "public",
                        principalTable: "t_e_adresse_adr",
                        principalColumn: "adr_id");
                    table.ForeignKey(
                        name: "FK_t_e_commande_com_t_e_adresse_adr_adr_idlivraison",
                        column: x => x.adr_idlivraison,
                        principalSchema: "public",
                        principalTable: "t_e_adresse_adr",
                        principalColumn: "adr_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_e_commande_com_t_e_client_cli_cli_id",
                        column: x => x.cli_id,
                        principalSchema: "public",
                        principalTable: "t_e_client_cli",
                        principalColumn: "cli_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_lignepanier_lig",
                schema: "public",
                columns: table => new
                {
                    lig_idpanier = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    art_id = table.Column<int>(type: "integer", nullable: false),
                    cli_id = table.Column<int>(type: "integer", nullable: false),
                    com_id = table.Column<int>(type: "integer", nullable: false),
                    lig_qtepanier = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_lignepanier_lig", x => x.lig_idpanier);
                    table.ForeignKey(
                        name: "FK_t_e_lignepanier_lig_t_e_article_art_art_id",
                        column: x => x.art_id,
                        principalSchema: "public",
                        principalTable: "t_e_article_art",
                        principalColumn: "art_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_e_lignepanier_lig_t_e_client_cli_cli_id",
                        column: x => x.cli_id,
                        principalSchema: "public",
                        principalTable: "t_e_client_cli",
                        principalColumn: "cli_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CadreVelo",
                schema: "public",
                columns: table => new
                {
                    CadresIdMateriau = table.Column<int>(type: "integer", nullable: false),
                    VelosIdVelo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CadreVelo", x => new { x.CadresIdMateriau, x.VelosIdVelo });
                    table.ForeignKey(
                        name: "FK_CadreVelo_t_e_velo_vel_VelosIdVelo",
                        column: x => x.VelosIdVelo,
                        principalSchema: "public",
                        principalTable: "t_e_velo_vel",
                        principalColumn: "idVelo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CadreVelo_t_s_cadre_cad_CadresIdMateriau",
                        column: x => x.CadresIdMateriau,
                        principalSchema: "public",
                        principalTable: "t_s_cadre_cad",
                        principalColumn: "cad_id_materiau",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CouleurVelo",
                schema: "public",
                columns: table => new
                {
                    CouleursIdCouleur = table.Column<int>(type: "integer", nullable: false),
                    VelosIdVelo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouleurVelo", x => new { x.CouleursIdCouleur, x.VelosIdVelo });
                    table.ForeignKey(
                        name: "FK_CouleurVelo_t_e_velo_vel_VelosIdVelo",
                        column: x => x.VelosIdVelo,
                        principalSchema: "public",
                        principalTable: "t_e_velo_vel",
                        principalColumn: "idVelo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CouleurVelo_t_s_couleur_clr_CouleursIdCouleur",
                        column: x => x.CouleursIdCouleur,
                        principalSchema: "public",
                        principalTable: "t_s_couleur_clr",
                        principalColumn: "clr_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GeometrieVelo",
                schema: "public",
                columns: table => new
                {
                    GeometriesIdGeometrie = table.Column<int>(type: "integer", nullable: false),
                    VelosIdVelo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeometrieVelo", x => new { x.GeometriesIdGeometrie, x.VelosIdVelo });
                    table.ForeignKey(
                        name: "FK_GeometrieVelo_t_e_velo_vel_VelosIdVelo",
                        column: x => x.VelosIdVelo,
                        principalSchema: "public",
                        principalTable: "t_e_velo_vel",
                        principalColumn: "idVelo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeometrieVelo_t_s_geometrie_geo_GeometriesIdGeometrie",
                        column: x => x.GeometriesIdGeometrie,
                        principalSchema: "public",
                        principalTable: "t_s_geometrie_geo",
                        principalColumn: "geo_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TailleVelo",
                schema: "public",
                columns: table => new
                {
                    TaillesIdTaille = table.Column<int>(type: "integer", nullable: false),
                    VelosIdVelo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TailleVelo", x => new { x.TaillesIdTaille, x.VelosIdVelo });
                    table.ForeignKey(
                        name: "FK_TailleVelo_t_e_velo_vel_VelosIdVelo",
                        column: x => x.VelosIdVelo,
                        principalSchema: "public",
                        principalTable: "t_e_velo_vel",
                        principalColumn: "idVelo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TailleVelo_t_s_taille_tli_TaillesIdTaille",
                        column: x => x.TaillesIdTaille,
                        principalSchema: "public",
                        principalTable: "t_s_taille_tli",
                        principalColumn: "tli_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_e_article_art_art_dispo_en_ligne",
                schema: "public",
                table: "t_e_article_art",
                column: "art_dispo_en_ligne");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_article_art_art_id",
                schema: "public",
                table: "t_e_article_art",
                column: "art_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_article_art_art_prix",
                schema: "public",
                table: "t_e_article_art",
                column: "art_prix");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_article_art_art_reference",
                schema: "public",
                table: "t_e_article_art",
                column: "art_reference");

            migrationBuilder.CreateIndex(
                name: "IX_CadreVelo_VelosIdVelo",
                schema: "public",
                table: "CadreVelo",
                column: "VelosIdVelo");

            migrationBuilder.CreateIndex(
                name: "IX_CouleurVelo_VelosIdVelo",
                schema: "public",
                table: "CouleurVelo",
                column: "VelosIdVelo");

            migrationBuilder.CreateIndex(
                name: "IX_GeometrieVelo_VelosIdVelo",
                schema: "public",
                table: "GeometrieVelo",
                column: "VelosIdVelo");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_adresse_adr_adr_codepostale",
                schema: "public",
                table: "t_e_adresse_adr",
                column: "adr_codepostale");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_adresse_adr_adr_id",
                schema: "public",
                table: "t_e_adresse_adr",
                column: "adr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_adresse_adr_adr_ville",
                schema: "public",
                table: "t_e_adresse_adr",
                column: "adr_ville");

            migrationBuilder.CreateIndex(
                name: "civilite_pk",
                schema: "public",
                table: "t_e_civilite_civ",
                column: "civ_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_client_cli_adr_id",
                schema: "public",
                table: "t_e_client_cli",
                column: "adr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_client_cli_civ_id",
                schema: "public",
                table: "t_e_client_cli",
                column: "civ_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_client_cli_cli_email",
                schema: "public",
                table: "t_e_client_cli",
                column: "cli_email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_client_cli_cli_id",
                schema: "public",
                table: "t_e_client_cli",
                column: "cli_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_client_cli_cli_nom",
                schema: "public",
                table: "t_e_client_cli",
                column: "cli_nom");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_client_cli_cli_prenom",
                schema: "public",
                table: "t_e_client_cli",
                column: "cli_prenom");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_commande_com_adr_idfacturation",
                schema: "public",
                table: "t_e_commande_com",
                column: "adr_idfacturation");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_commande_com_adr_idlivraison",
                schema: "public",
                table: "t_e_commande_com",
                column: "adr_idlivraison");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_commande_com_cli_id",
                schema: "public",
                table: "t_e_commande_com",
                column: "cli_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_commande_com_com_id",
                schema: "public",
                table: "t_e_commande_com",
                column: "com_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_lignepanier_lig_art_id",
                schema: "public",
                table: "t_e_lignepanier_lig",
                column: "art_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_lignepanier_lig_cli_id",
                schema: "public",
                table: "t_e_lignepanier_lig",
                column: "cli_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_lignepanier_lig_lig_idpanier",
                schema: "public",
                table: "t_e_lignepanier_lig",
                column: "lig_idpanier");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_lignepanier_lig_lig_qtepanier",
                schema: "public",
                table: "t_e_lignepanier_lig",
                column: "lig_qtepanier");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_velo_vel_idmodele",
                schema: "public",
                table: "t_e_velo_vel",
                column: "idmodele");

            migrationBuilder.CreateIndex(
                name: "IX_TailleVelo_VelosIdVelo",
                schema: "public",
                table: "TailleVelo",
                column: "VelosIdVelo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CadreVelo",
                schema: "public");

            migrationBuilder.DropTable(
                name: "CouleurVelo",
                schema: "public");

            migrationBuilder.DropTable(
                name: "GeometrieVelo",
                schema: "public");

            migrationBuilder.DropTable(
                name: "t_e_commande_com",
                schema: "public");

            migrationBuilder.DropTable(
                name: "t_e_lignepanier_lig",
                schema: "public");

            migrationBuilder.DropTable(
                name: "TailleVelo",
                schema: "public");

            migrationBuilder.DropTable(
                name: "t_s_cadre_cad",
                schema: "public");

            migrationBuilder.DropTable(
                name: "t_s_couleur_clr",
                schema: "public");

            migrationBuilder.DropTable(
                name: "t_s_geometrie_geo",
                schema: "public");

            migrationBuilder.DropTable(
                name: "t_e_client_cli",
                schema: "public");

            migrationBuilder.DropTable(
                name: "t_e_velo_vel",
                schema: "public");

            migrationBuilder.DropTable(
                name: "t_s_taille_tli",
                schema: "public");

            migrationBuilder.DropTable(
                name: "t_e_adresse_adr",
                schema: "public");

            migrationBuilder.DropTable(
                name: "t_e_civilite_civ",
                schema: "public");

            migrationBuilder.DropTable(
                name: "t_e_modele_mod",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "IX_t_e_article_art_art_dispo_en_ligne",
                schema: "public",
                table: "t_e_article_art");

            migrationBuilder.DropIndex(
                name: "IX_t_e_article_art_art_id",
                schema: "public",
                table: "t_e_article_art");

            migrationBuilder.DropIndex(
                name: "IX_t_e_article_art_art_prix",
                schema: "public",
                table: "t_e_article_art");

            migrationBuilder.DropIndex(
                name: "IX_t_e_article_art_art_reference",
                schema: "public",
                table: "t_e_article_art");
        }
    }
}
