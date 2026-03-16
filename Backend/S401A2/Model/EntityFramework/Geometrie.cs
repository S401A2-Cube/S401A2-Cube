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
        [StringLength(100, ErrorMessage = "Le nom de la pièce ne peut pas dépasser 100 caractères")]
        [Column("geo_nom_piece")]
        public string NomPiece { get; set; } = null!;

        [Column("geo_taille_piece")]
        [Range(0.1, 100.0, ErrorMessage = "La taille de la pièce doit être comprise entre 0.1 et 100.0")]
        public decimal TaillePiece { get; set; }

        [InverseProperty(nameof(Velo.Geometries))]
        public virtual ICollection<Velo> Velos { get; set; } = new List<Velo>();
    }
}
