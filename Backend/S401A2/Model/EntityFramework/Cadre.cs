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
        [RegularExpression(@"^(Aluminium|Carbone|Acier|Titane)$", ErrorMessage = "Le matériau doit être Aluminium, Carbone, Acier ou Titane")]
        [Column("cad_nom_mat")]
        public string NomMat { get; set; } = null!;

        [RegularExpression(@"^(Wave|Diamant|Trapèze|VTT)$", ErrorMessage = "Le cadre doit être diamant, wave trapèze ou vtt")]
        [Column("cad_forme")]
        public string FormeCadre { get; set; }

        [InverseProperty(nameof(Velo.Cadres))]
        public virtual ICollection<Velo> Velos { get; set; } = new List<Velo>();
    }
}
