using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("corresponda")]
[Index("Idgeometrie", Name = "corresponda2_fk")]
[Index("Idtaille", Name = "corresponda3_fk")]
[Index("Idmateriau", "Idcouleur", "VelIdtaille", "Idarticle", Name = "corresponda_fk")]
[Index("Idcorresponda", Name = "corresponda_pk", IsUnique = true)]
public partial class Correspondum
{
    [Key]
    [Column("idcorresponda")]
    public int Idcorresponda { get; set; }

    [Column("idmateriau")]
    public int Idmateriau { get; set; }

    [Column("idcouleur")]
    public int Idcouleur { get; set; }

    [Column("vel_idtaille")]
    public int VelIdtaille { get; set; }

    [Column("idarticle")]
    public int Idarticle { get; set; }

    [Column("idgeometrie")]
    public int Idgeometrie { get; set; }

    [Column("idtaille")]
    public int Idtaille { get; set; }

    [ForeignKey("Idgeometrie")]
    [InverseProperty("Corresponda")]
    public virtual Geometrie IdgeometrieNavigation { get; set; } = null!;

    [ForeignKey("Idtaille")]
    [InverseProperty("Corresponda")]
    public virtual Taille IdtailleNavigation { get; set; } = null!;

    [ForeignKey("Idmateriau, Idcouleur, VelIdtaille, Idarticle")]
    [InverseProperty("Corresponda")]
    public virtual Velo Velo { get; set; } = null!;
}
