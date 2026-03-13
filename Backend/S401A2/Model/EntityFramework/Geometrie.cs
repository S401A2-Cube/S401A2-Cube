using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using APICube.Models.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace S401A2.Model.EntityFramework
{
    [Table("t_s_geometrie_geo")]
    [PrimaryKey(nameof(IdGeometrie))]
    public partial class Geometrie
    {
        [Key]
        [Column("geo_id")]
        public int IdGeometrie { get; set; }

        [Required]
        [StringLength(50)]
        [Column("geo_nom_piece")]
        public string NomPiece { get; set; } = null!;

        [Column("geo_taille_piece")]
        public double TaillePiece { get; set; }

        [InverseProperty(nameof(Velo.Geometries))]
        public virtual ICollection<Velo> Velos { get; set; } = new List<Velo>();
    }
}
