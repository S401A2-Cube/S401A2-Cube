using System.Collections.Generic;
using APICube.Models.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace S401A2.Model.EntityFramework
{
    public partial class CubeDBContext : DbContext
    {
        public CubeDBContext() { }

        public CubeDBContext(DbContextOptions<CubeDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Articles { get; set; } = null!;
        public virtual DbSet<Categorie> Categories { get; set; } = null!;
        public virtual DbSet<MotCle> MotCles { get; set; } = null!;

        public virtual DbSet<Velo> Velos { get; set; } = null!;
        public virtual DbSet<Modele> Modeles { get; set; } = null!;
        public virtual DbSet<Taille> Tailles { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                string? connectionString = configuration.GetConnectionString("LocalConnectionString");
                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            // Example of configuring the Utilisateur entity with default values and unique index
            //modelBuilder.Entity<Utilisateur>(entity =>
            //{
            //    entity.Property(e => e.Pays)
            //    .HasDefaultValue("France");

            //    entity.Property(e => e.DateCreation)
            //    .HasDefaultValueSql("now()");

            //    entity.HasIndex(e => e.Email)
            //    .IsUnique();
            //});

            modelBuilder.Entity<Article>()
                .HasMany(a => a.MotsCles)
                .WithMany(m => m.Articles)
                .UsingEntity(j => j.ToTable("t_j_article_motcle_amc"));
            
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
