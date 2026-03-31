using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace S401A2.Model.EntityFramework
{
    [Table("t_e_accessoire_acc")]
    public partial class Accessoire : Article
    {
        [Required]
        [StringLength(50)]
        [Column("acc_type")]
        public string TypeAccessoire { get; set; } = null!;

        [Column("acc_taille_unique")]
        public bool TailleUnique { get; set; }

        [Required]
        [StringLength(255)]
        [Column("acc_materiaux")]
        public string Materiaux { get; set; } = null!;

        [StringLength(100)]
        [Column("acc_dimensions")]
        public string? Dimensions { get; set; }

        [Required]
        [Column("acc_caracteristiques")]
        public string Caracteristiques { get; set; } = null!;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual ICollection<Taille>? Tailles { get; set; } = new List<Taille>();
    }
}