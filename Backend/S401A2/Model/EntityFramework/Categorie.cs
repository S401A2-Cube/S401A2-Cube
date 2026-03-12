using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace S401A2.Model.EntityFramework
{
    [Table("t_e_categorie_cat")]
    [PrimaryKey(nameof(CategorieId))]
    public partial class Categorie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("cat_id")]
        public int CategorieId { get; set; }

        [Required]
        [StringLength(100)]
        [Column("cat_nom")]
        public string Nom { get; set; } = null!;

        [InverseProperty(nameof(Article.CategorieArticle))]
        public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
    }
}
