using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using APICube.Models.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace S401A2.Model.EntityFramework
{
    [Table("t_s_cadre_cad")]
    [PrimaryKey(nameof(IdMateriau))]
    public partial class Cadre
    {
        [Key]
        [Column("cad_id_materiau")]
        public int IdMateriau { get; set; }

        [Required]
        [StringLength(50)]
        [Column("cad_nom_mat")]
        public string NomMat { get; set; } = null!;

        [StringLength(50)]
        [Column("cad_forme")]
        public string FormeCadre { get; set; }

        [InverseProperty(nameof(Velo.Cadres))]
        public virtual ICollection<Velo> Velos { get; set; } = new List<Velo>();
    }
}
