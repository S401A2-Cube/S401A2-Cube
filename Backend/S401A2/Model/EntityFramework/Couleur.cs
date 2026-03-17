using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using APICube.Models.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace S401A2.Model.EntityFramework
{
    [Table("t_s_couleur_clr")]
    [PrimaryKey(nameof(IdCouleur))]
    public partial class Couleur
    {
        [Key]
        [Column("clr_id")]
        public int IdCouleur { get; set; }

        [Required]
        [StringLength(50)]
        [Column("clr_nom")]
        public string NomCouleur { get; set; } = null!;

        [StringLength(50)]
        [Column("clr_effet_peinture")]
        public string? EffetPeinture { get; set; }

        [InverseProperty(nameof(Velo.Couleurs))]
        public virtual ICollection<Velo> Velos { get; set; } = new List<Velo>();
    }
}
