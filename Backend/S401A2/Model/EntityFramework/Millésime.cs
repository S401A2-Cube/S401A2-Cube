using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework
{
    [Table("t_s_millesime_mls")]
    [PrimaryKey(nameof(IdMillesime))]
    public partial class Millesime
    {
        [Key]
        [Column("mls_id")]
        public int IdMillesime { get; set; }

        [Required]
        [Column("mls_annee")]
        [Range(1900, 2100, ErrorMessage = "L'année doit être comprise entre 1900 et 2100")]
        public int Annee { get; set; }

        [StringLength(100)]
        [Column("mls_description")]
        public string? Description { get; set; }

        [InverseProperty(nameof(Velo.Millesimes))]
        [JsonIgnore]
        public virtual ICollection<Velo> Velos { get; set; } = new List<Velo>();
    }
}