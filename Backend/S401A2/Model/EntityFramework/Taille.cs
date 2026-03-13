using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using APICube.Models.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace S401A2.Model.EntityFramework
{
    [Table("t_s_taille_tli")]
    [PrimaryKey(nameof(IdTaille))]
    public partial class Taille
    {
        [Key]
        [Column("tli_id")]
        public int IdTaille { get; set; }

        [Required]
        [StringLength(10)]
        [Column("tli_libelle")]
        public string LibelleTaille { get; set; } = null!;

        [InverseProperty(nameof(Velo.Tailles))]
        public virtual ICollection<Velo> Velos { get; set; } = new List<Velo>();
    }
}
