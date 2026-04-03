using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using APICube.Models.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

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
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [ValidateNever]
        public virtual Categorie? CategorieArticle { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [ValidateNever]
        public virtual ICollection<MotCle>? MotsCles { get; set; } = new List<MotCle>();

        [InverseProperty(nameof(LignePanier.ArticleLignePanier))]
        [ValidateNever]
        public virtual ICollection<LignePanier> ArticleLignePanier { get; set; } = new List<LignePanier>();

        [InverseProperty(nameof(Commande.ArticleCommande))]
        [ValidateNever]
        public virtual Commande? ArticleCommande { get; set; }

        [InverseProperty(nameof(Velo.Article))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [ValidateNever]
        public virtual ICollection<Velo>? Velos { get; set; } = new List<Velo>();

        [InverseProperty(nameof(Image.Article))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [ValidateNever]
        public virtual ICollection<Image>? Images { get; set; } = new List<Image>();
    }
}