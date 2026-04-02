using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using APICube.Models.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace S401A2.Model.EntityFramework
{
    [Index(nameof(ArticleId))]
    [Index(nameof(Reference))]
    [Index(nameof(Prix))]
    [Index(nameof(DispoEnLigne))]
    [Index(nameof(CategorieId))]
    [Table("t_e_article_art")]
    [PrimaryKey(nameof(ArticleId))]
    public partial class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("art_id")]
        public int ArticleId { get; set; }

        [Required]
        [StringLength(20)]
        [Column("art_reference")]
        public string Reference { get; set; } = null!;

        [Required]
        [StringLength(100)]
        [Column("art_nom")]
        public string Nom { get; set; } = null!;

        [Required]
        [StringLength(2000)]
        [Column("art_description")]
        public string Description { get; set; } = null!;

        [Column("art_prix")]
        [Precision(10, 2)]
        public decimal Prix { get; set; }

        [Column("art_poids")]
        [Precision(10, 2)]
        public decimal Poids { get; set; }

        [Column("art_qte_stock")]
        [Range(0, 1000)]
        public int QteStock { get; set; }

        [Column("art_annee")]
        [Range(1900, 3000)]
        public int Annee { get; set; }

        [Column("art_dispo_en_ligne")]
        public bool DispoEnLigne { get; set; }

        [Column("cat_id")]
        public int CategorieId { get; set; }

        [ForeignKey(nameof(CategorieId))]
        [InverseProperty(nameof(Categorie.Articles))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] // omit when null
        public virtual Categorie? CategorieArticle { get; set; } = null;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual ICollection<MotCle>? MotsCles { get; set; } = new List<MotCle>();

        [InverseProperty(nameof(LignePanier.ArticleLignePanier))]
        public virtual ICollection<LignePanier> ArticleLignePanier { get; set; } = new List<LignePanier>();

        [InverseProperty(nameof(Commande.ArticleCommande))]
        public virtual Commande? ArticleCommande { get; set; } = null;

        [InverseProperty(nameof(Velo.Article))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual ICollection<Velo>? Velos { get; set; } = new List<Velo>();

        [InverseProperty(nameof(Image.Article))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual ICollection<Image>? Images { get; set; } = new List<Image>();

        // TODO: Navigation towards other entities when they will be 

        /*
         * 
         * (int) id article
         * (string) reference
         * (string) nom article
         * (string) description
         * (double) prix
         * (double) poids
         * (int) qte stock
         * (int) annee
         * (bool) dispo en ligne
         * 
         * (fk HAS ONE)  une categorie
         * (fk HAS ONE)  model 3d 
         * (fk HAS MANY) liste de mots cles
         * (fk HAS MANY) liste articles similaires
         * (fk HAS MANY) liste de photos
         * 
         */
    }
}
