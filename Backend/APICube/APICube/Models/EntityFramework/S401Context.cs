using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

public partial class S401Context : DbContext
{
    public S401Context()
    {
    }

    public S401Context(DbContextOptions<S401Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Accessoire> Accessoires { get; set; }

    public virtual DbSet<Adresse> Adresses { get; set; }

    public virtual DbSet<Anneesortie> Anneesorties { get; set; }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<Batterie> Batteries { get; set; }

    public virtual DbSet<Cadre> Cadres { get; set; }

    public virtual DbSet<Caracteristique> Caracteristiques { get; set; }

    public virtual DbSet<Categorie> Categories { get; set; }

    public virtual DbSet<Categoriequestion> Categoriequestions { get; set; }

    public virtual DbSet<Civilite> Civilites { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Codepromo> Codepromos { get; set; }

    public virtual DbSet<Commande> Commandes { get; set; }

    public virtual DbSet<Correspondum> Corresponda { get; set; }

    public virtual DbSet<Couleur> Couleurs { get; set; }

    public virtual DbSet<Date> Dates { get; set; }

    public virtual DbSet<Enregistre> Enregistres { get; set; }

    public virtual DbSet<Est> Ests { get; set; }

    public virtual DbSet<Estcaracterisepar> Estcaracterisepars { get; set; }

    public virtual DbSet<Estdisponibledan> Estdisponibledans { get; set; }

    public virtual DbSet<Estenlienavec> Estenlienavecs { get; set; }

    public virtual DbSet<Estouvert> Estouverts { get; set; }

    public virtual DbSet<Estsimilairea> Estsimilaireas { get; set; }

    public virtual DbSet<FailedJob> FailedJobs { get; set; }

    public virtual DbSet<Faitreferencea> Faitreferenceas { get; set; }

    public virtual DbSet<Geometrie> Geometries { get; set; }

    public virtual DbSet<HistoArticle> HistoArticles { get; set; }

    public virtual DbSet<Horaire> Horaires { get; set; }

    public virtual DbSet<Lignepanier> Lignepaniers { get; set; }

    public virtual DbSet<Magasinpartenaire> Magasinpartenaires { get; set; }

    public virtual DbSet<Matiere> Matieres { get; set; }

    public virtual DbSet<Migration> Migrations { get; set; }

    public virtual DbSet<Millesime> Millesimes { get; set; }

    public virtual DbSet<Modedelivraison> Modedelivraisons { get; set; }

    public virtual DbSet<Modele> Modeles { get; set; }

    public virtual DbSet<Motcle> Motcles { get; set; }

    public virtual DbSet<PasswordResetToken> PasswordResetTokens { get; set; }

    public virtual DbSet<PersonalAccessToken> PersonalAccessTokens { get; set; }

    public virtual DbSet<Peutavoir> Peutavoirs { get; set; }

    public virtual DbSet<Peutavoir3> Peutavoir3s { get; set; }

    public virtual DbSet<Peutavoir4> Peutavoir4s { get; set; }

    public virtual DbSet<Peutavoir5> Peutavoir5s { get; set; }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Taille> Tailles { get; set; }

    public virtual DbSet<Typecaracteristique> Typecaracteristiques { get; set; }

    public virtual DbSet<Usage> Usages { get; set; }

    public virtual DbSet<Vae> Vaes { get; set; }

    public virtual DbSet<Velo> Velos { get; set; }

    public virtual DbSet<Veloelectrique> Veloelectriques { get; set; }

    public virtual DbSet<Velomusculaire> Velomusculaires { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;port=5432;Database=S4_01; uid=postgres;\npassword=postgres;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Accessoire>(entity =>
        {
            entity.HasKey(e => e.Idarticle).HasName("pk_accessoire");

            entity.Property(e => e.Idarticle).ValueGeneratedNever();

            entity.HasOne(d => d.IdanneeNavigation).WithMany(p => p.Accessoires)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_accessoi_reference_anneesor");

            entity.HasOne(d => d.IdarticleNavigation).WithOne(p => p.Accessoire)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_accessoi_heritage__article");

            entity.HasOne(d => d.IdmatiereNavigation).WithMany(p => p.Accessoires)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_accessoi_reference_matiere");

            entity.HasOne(d => d.IdtailleNavigation).WithMany(p => p.Accessoires)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_accessoi_reference_taille");
        });

        modelBuilder.Entity<Adresse>(entity =>
        {
            entity.HasKey(e => e.Idadresse).HasName("pk_adresse");
        });

        modelBuilder.Entity<Anneesortie>(entity =>
        {
            entity.HasKey(e => e.Idannee).HasName("pk_anneesortie");
        });

        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.Idarticle).HasName("pk_article");

            entity.HasOne(d => d.IdcategorieNavigation).WithMany(p => p.Articles)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_article_appartien_categori");
        });

        modelBuilder.Entity<Batterie>(entity =>
        {
            entity.HasKey(e => e.Idtaillebatterie).HasName("pk_batterie");
        });

        modelBuilder.Entity<Cadre>(entity =>
        {
            entity.HasKey(e => e.Idmateriau).HasName("pk_cadre");
        });

        modelBuilder.Entity<Caracteristique>(entity =>
        {
            entity.HasKey(e => e.Idcaracteristique).HasName("pk_caracteristique");

            entity.HasOne(d => d.IdtypecaracteristiqueNavigation).WithMany(p => p.Caracteristiques)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_caracter_estdans_typecara");
        });

        modelBuilder.Entity<Categorie>(entity =>
        {
            entity.HasKey(e => e.Idcategorie).HasName("pk_categorie");

            entity.HasOne(d => d.CatIdcategorieNavigation).WithMany(p => p.InverseCatIdcategorieNavigation)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_categori_estsousca_categori");
        });

        modelBuilder.Entity<Categoriequestion>(entity =>
        {
            entity.HasKey(e => e.Idcatquestion).HasName("pk_catquestion_idcat");
        });

        modelBuilder.Entity<Civilite>(entity =>
        {
            entity.HasKey(e => e.Idcivilite).HasName("pk_civilite");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Idclient).HasName("pk_client");

            entity.Property(e => e.Is2faEnabled).HasDefaultValue(false);

            entity.HasOne(d => d.IdciviliteNavigation).WithMany(p => p.Clients)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_client_choisit_civilite");
        });

        modelBuilder.Entity<Codepromo>(entity =>
        {
            entity.HasKey(e => e.Idcodepromo).HasName("pk_codepromo");
        });

        modelBuilder.Entity<Commande>(entity =>
        {
            entity.HasKey(e => e.Idcommande).HasName("pk_commande");

            entity.HasOne(d => d.AdrIdadresseNavigation).WithMany(p => p.CommandeAdrIdadresseNavigations)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_commande_estlivrea_adresse");

            entity.HasOne(d => d.IdadresseNavigation).WithMany(p => p.CommandeIdadresseNavigations)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_commande_estfactur_adresse");

            entity.HasOne(d => d.IdclientNavigation).WithMany(p => p.Commandes)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_commande_passe_client");

            entity.HasOne(d => d.IdcodepromoNavigation).WithMany(p => p.Commandes)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_commande_reference_codeprom");

            entity.HasOne(d => d.IdmodelivraisonNavigation).WithMany(p => p.Commandes)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_commande_estlivree_modedeli");

            entity.HasOne(d => d.IdnommagasinNavigation).WithMany(p => p.Commandes)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_commande_estlivree_magasinp");
        });

        modelBuilder.Entity<Correspondum>(entity =>
        {
            entity.HasKey(e => e.Idcorresponda).HasName("pk_corresponda");

            entity.HasOne(d => d.IdgeometrieNavigation).WithMany(p => p.Corresponda)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_correspo_correspon_geometri");

            entity.HasOne(d => d.IdtailleNavigation).WithMany(p => p.Corresponda)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_correspo_correspon_taille");

            entity.HasOne(d => d.Velo).WithMany(p => p.Corresponda)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_correspo_correspon_velo");
        });

        modelBuilder.Entity<Couleur>(entity =>
        {
            entity.HasKey(e => e.Idcouleur).HasName("pk_couleur");

            entity.Property(e => e.Codehexacouleur).IsFixedLength();
        });

        modelBuilder.Entity<Date>(entity =>
        {
            entity.HasKey(e => e.Iddate).HasName("pk_date");
        });

        modelBuilder.Entity<Enregistre>(entity =>
        {
            entity.HasKey(e => e.Idenregistre).HasName("pk_enregistre");

            entity.HasOne(d => d.IdadresseNavigation).WithMany(p => p.Enregistres)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_enregist_enregistr_adresse");

            entity.HasOne(d => d.IdclientNavigation).WithMany(p => p.Enregistres)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_enregist_enregistr_client");
        });

        modelBuilder.Entity<Est>(entity =>
        {
            entity.HasKey(e => e.Idest).HasName("pk_est");

            entity.HasOne(d => d.IdcommandeNavigation).WithMany(p => p.Ests)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_est_est_commande");

            entity.HasOne(d => d.IddateNavigation).WithMany(p => p.Ests)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_est_est2_date");
        });

        modelBuilder.Entity<Estcaracterisepar>(entity =>
        {
            entity.HasKey(e => e.Idestcaracterisepar).HasName("pk_estcaracterisepar");

            entity.HasOne(d => d.IdarticleNavigation).WithMany(p => p.Estcaracterisepars)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_estcarac_estcaract_article");

            entity.HasOne(d => d.IdcaracteristiqueNavigation).WithMany(p => p.Estcaracterisepars)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_estcarac_estcaract_caracter");
        });

        modelBuilder.Entity<Estdisponibledan>(entity =>
        {
            entity.HasKey(e => e.Idestdisponibledans).HasName("pk_estdisponibledans");

            entity.HasOne(d => d.IdarticleNavigation).WithMany(p => p.Estdisponibledans)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_estdispo_estdispon_article");

            entity.HasOne(d => d.IdnommagasinNavigation).WithMany(p => p.Estdisponibledans)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_estdispo_estdispon_magasinp");
        });

        modelBuilder.Entity<Estenlienavec>(entity =>
        {
            entity.HasKey(e => e.Idestenlienavec).HasName("pk_estenlienavec");

            entity.HasOne(d => d.IdarticleNavigation).WithMany(p => p.Estenlienavecs)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_estenlie_estenlien_accessoi");

            entity.HasOne(d => d.Velo).WithMany(p => p.Estenlienavecs)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_estenlie_estenlien_velo");
        });

        modelBuilder.Entity<Estouvert>(entity =>
        {
            entity.HasKey(e => e.Idestouvert).HasName("pk_estouvert");

            entity.HasOne(d => d.IdhoraireNavigation).WithMany(p => p.Estouverts)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_estouver_estouvert_horaires");

            entity.HasOne(d => d.IdnommagasinNavigation).WithMany(p => p.Estouverts)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_estouver_estouvert_magasinp");
        });

        modelBuilder.Entity<Estsimilairea>(entity =>
        {
            entity.HasKey(e => e.Idestsimilairea).HasName("pk_estsimilairea");

            entity.HasOne(d => d.ArtIdarticleNavigation).WithMany(p => p.EstsimilaireaArtIdarticleNavigations)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_estsimil_est2simil_article");

            entity.HasOne(d => d.IdarticleNavigation).WithMany(p => p.EstsimilaireaIdarticleNavigations)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_estsimil_estsimila_article");
        });

        modelBuilder.Entity<FailedJob>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("failed_jobs_pkey");

            entity.Property(e => e.FailedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Faitreferencea>(entity =>
        {
            entity.HasKey(e => e.Idfaitreferencea).HasName("pk_faitreferencea");

            entity.HasOne(d => d.IdarticleNavigation).WithMany(p => p.Faitreferenceas)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_faitrefe_faitrefer_article");

            entity.HasOne(d => d.IdmotcleNavigation).WithMany(p => p.Faitreferenceas)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_faitrefe_faitrefer_motcle");
        });

        modelBuilder.Entity<Geometrie>(entity =>
        {
            entity.HasKey(e => e.Idgeometrie).HasName("pk_geometrie");
        });

        modelBuilder.Entity<HistoArticle>(entity =>
        {
            entity.HasKey(e => e.Idhistoa).HasName("pk_idhistoa_histoa");

            entity.Property(e => e.Datemodif).HasDefaultValueSql("CURRENT_DATE");
        });

        modelBuilder.Entity<Horaire>(entity =>
        {
            entity.HasKey(e => e.Idhoraire).HasName("pk_horaires");
        });

        modelBuilder.Entity<Lignepanier>(entity =>
        {
            entity.HasKey(e => e.Idpanier).HasName("pk_lignepanier");

            entity.HasOne(d => d.IdarticleNavigation).WithMany(p => p.Lignepaniers)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_lignepan_panier_article");

            entity.HasOne(d => d.IdclientNavigation).WithMany(p => p.Lignepaniers)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_lignepan_panier2_client");

            entity.HasOne(d => d.IdcommandeNavigation).WithMany(p => p.Lignepaniers)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_lignepan_panier3_commande");
        });

        modelBuilder.Entity<Magasinpartenaire>(entity =>
        {
            entity.HasKey(e => e.Idnommagasin).HasName("pk_magasinpartenaire");
        });

        modelBuilder.Entity<Matiere>(entity =>
        {
            entity.HasKey(e => e.Idmatiere).HasName("pk_matiere");
        });

        modelBuilder.Entity<Migration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("migrations_pkey");
        });

        modelBuilder.Entity<Millesime>(entity =>
        {
            entity.HasKey(e => e.Idmillesime).HasName("pk_millesime");
        });

        modelBuilder.Entity<Modedelivraison>(entity =>
        {
            entity.HasKey(e => e.Idmodelivraison).HasName("pk_modedelivraison");
        });

        modelBuilder.Entity<Modele>(entity =>
        {
            entity.HasKey(e => e.Idmodele).HasName("pk_modele");
        });

        modelBuilder.Entity<Motcle>(entity =>
        {
            entity.HasKey(e => e.Idmotcle).HasName("pk_motcle");
        });

        modelBuilder.Entity<PasswordResetToken>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("password_reset_tokens_pkey");
        });

        modelBuilder.Entity<PersonalAccessToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("personal_access_tokens_pkey");
        });

        modelBuilder.Entity<Peutavoir>(entity =>
        {
            entity.HasKey(e => e.Idpeutavoir).HasName("pk_peutavoir");

            entity.HasOne(d => d.IdtailleNavigation).WithMany(p => p.Peutavoirs)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_peutavoi_peutavoir_taille");
        });

        modelBuilder.Entity<Peutavoir3>(entity =>
        {
            entity.HasKey(e => e.Idpeutavoir3).HasName("pk_peutavoir3");

            entity.HasOne(d => d.IdcouleurNavigation).WithMany(p => p.Peutavoir3s)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_peutavoi_peutavoir_couleur");

            entity.HasOne(d => d.Velo).WithMany(p => p.Peutavoir3s)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_peutavoi_peutavoir_velo");
        });

        modelBuilder.Entity<Peutavoir4>(entity =>
        {
            entity.HasKey(e => e.Idpeutavoir4).HasName("pk_peutavoir4");

            entity.HasOne(d => d.IdtaillebatterieNavigation).WithMany(p => p.Peutavoir4s)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_peutavoi_peutavoir_batterie");

            entity.HasOne(d => d.Veloelectrique).WithMany(p => p.Peutavoir4s)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_peutavoi_peutavoir_veloelec");
        });

        modelBuilder.Entity<Peutavoir5>(entity =>
        {
            entity.HasKey(e => e.Idpeutavoir5).HasName("pk_peutavoir5");

            entity.HasOne(d => d.IdmateriauNavigation).WithMany(p => p.Peutavoir5s)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_peutavoi_peutavoir_cadre");

            entity.HasOne(d => d.Velo).WithMany(p => p.Peutavoir5s)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_peutavoi_peutavoir_velo");
        });

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.HasKey(e => e.Idphoto).HasName("pk_photo");

            entity.Property(e => e.Lienphoto).IsFixedLength();

            entity.HasOne(d => d.IdarticleNavigation).WithMany(p => p.Photos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_photo_estassoci_article");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Idquestion).HasName("pk_question_idquestion");

            entity.HasOne(d => d.IdcatquestionNavigation).WithMany(p => p.Questions).HasConstraintName("fk_question_catquest");
        });

        modelBuilder.Entity<Taille>(entity =>
        {
            entity.HasKey(e => e.Idtaille).HasName("pk_taille");
        });

        modelBuilder.Entity<Typecaracteristique>(entity =>
        {
            entity.HasKey(e => e.Idtypecaracteristique).HasName("pk_typecaracteristique");
        });

        modelBuilder.Entity<Usage>(entity =>
        {
            entity.HasKey(e => e.Idusage).HasName("pk_usage");
        });

        modelBuilder.Entity<Vae>(entity =>
        {
            entity.HasKey(e => e.Idtypevae).HasName("pk_vae");
        });

        modelBuilder.Entity<Velo>(entity =>
        {
            entity.HasKey(e => new { e.Idmateriau, e.Idcouleur, e.Idtaille, e.Idarticle }).HasName("pk_velo");

            entity.HasOne(d => d.IdarticleNavigation).WithMany(p => p.Velos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_velo_heritage__article");

            entity.HasOne(d => d.IdcouleurNavigation).WithMany(p => p.Velos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_velo_a3_couleur");

            entity.HasOne(d => d.IdmateriauNavigation).WithMany(p => p.Velos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_velo_a2_cadre");

            entity.HasOne(d => d.IdmillesimeNavigation).WithMany(p => p.Velos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_velo_estsortie_millesim");

            entity.HasOne(d => d.IdmodeleNavigation).WithMany(p => p.Velos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_velo_estde_modele");

            entity.HasOne(d => d.IdtailleNavigation).WithMany(p => p.Velos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_velo_a6_taille");

            entity.HasOne(d => d.IdusageNavigation).WithMany(p => p.Velos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_velo_reference_usage");
        });

        modelBuilder.Entity<Veloelectrique>(entity =>
        {
            entity.HasKey(e => new { e.Idmateriau, e.Idcouleur, e.Idtaille, e.Idarticle, e.Idtypevae }).HasName("pk_veloelectrique");

            entity.HasOne(d => d.IdarticleNavigation).WithMany(p => p.Veloelectriques)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_veloel_herita__article");

            entity.HasOne(d => d.IdmillesimeNavigation).WithMany(p => p.Veloelectriques)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_veloel_estsort_millesim");

            entity.HasOne(d => d.IdtaillebatterieNavigation).WithMany(p => p.Veloelectriques)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_veloelec_a5_batterie");

            entity.HasOne(d => d.IdtypevaeNavigation).WithMany(p => p.Veloelectriques)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_veloelec_a4_vae");

            entity.HasOne(d => d.IdusageNavigation).WithMany(p => p.Veloelectriques)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_veloel_referen_usage");

            entity.HasOne(d => d.Velo).WithMany(p => p.Veloelectriques)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_veloelec_heritage__velo");
        });

        modelBuilder.Entity<Velomusculaire>(entity =>
        {
            entity.HasKey(e => new { e.Idmateriau, e.Idcouleur, e.Idtaille, e.Idarticle }).HasName("pk_velomusculaire");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
