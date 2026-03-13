using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using APICube.Models.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace S401A2.Model.EntityFramework
{
    [Table("t_e_modele_mod")]
    [PrimaryKey(nameof(IdModele))]
    public partial class Modele
    {
        [Key]
        [Column("mod_id")]
        public int IdModele { get; set; }

        [Required]
        [StringLength(50)]
        [Column("mod_nom")]
        public string NomModele { get; set; } = null!;

        [InverseProperty(nameof(Velo.ModeleVelo))]
        public virtual ICollection<Velo> Velos { get; set; } = new List<Velo>();
    }
}
