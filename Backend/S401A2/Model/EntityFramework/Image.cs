using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace S401A2.Model.EntityFramework
{
    [Table("t_e_image_img")]
    public partial class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("img_id")]
        public int ImageId { get; set; }

        [Required]
        [StringLength(255)]
        [Column("img_chemin")]
        public string Chemin { get; set; } = null!;

        [Column("art_id")]
        public int ArticleId { get; set; }

        [ForeignKey(nameof(ArticleId))]
        [InverseProperty("Images")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual Article Article { get; set; } = null!;
    }
}