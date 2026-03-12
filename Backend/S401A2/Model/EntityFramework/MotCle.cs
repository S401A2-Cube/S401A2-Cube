using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace S401A2.Model.EntityFramework
{
    [Table("t_e_motcle_mcl")]
    [PrimaryKey(nameof(MotCleId))]
    public partial class MotCle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("mcl_id")]
        public int MotCleId { get; set; }

        [Required]
        [StringLength(100)]
        [Column("mcl_nom")]
        public string Nom { get; set; } = null!;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual ICollection<Article>? Articles { get; set; } = new List<Article>();
    }
}
