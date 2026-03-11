using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("couleur")]
[Index("Idcouleur", Name = "couleur_pk", IsUnique = true)]
[Index("Nomcouleur", Name = "idx_couleur_nom")]
public partial class Couleur
{
    [Key]
    [Column("idcouleur")]
    public int Idcouleur { get; set; }

    [Column("codehexacouleur")]
    [StringLength(6)]
    public string? Codehexacouleur { get; set; }

    [Column("effetpeinture")]
    [StringLength(50)]
    public string? Effetpeinture { get; set; }

    [Column("nomcouleur")]
    [StringLength(100)]
    public string? Nomcouleur { get; set; }

    [InverseProperty("IdcouleurNavigation")]
    public virtual ICollection<Peutavoir3> Peutavoir3s { get; set; } = new List<Peutavoir3>();

    [InverseProperty("IdcouleurNavigation")]
    public virtual ICollection<Velo> Velos { get; set; } = new List<Velo>();
}
