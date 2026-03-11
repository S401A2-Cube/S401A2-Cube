using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("peutavoir4")]
[Index("Idmateriau", "Idcouleur", "Idtaille", "Idarticle", "Idtypevae", Name = "peutavoir4_fk")]
[Index("Idpeutavoir4", Name = "peutavoir4_pk", IsUnique = true)]
[Index("Idtaillebatterie", Name = "peutavoir5_fk")]
public partial class Peutavoir4
{
    [Key]
    [Column("idpeutavoir4")]
    public int Idpeutavoir4 { get; set; }

    [Column("idmateriau")]
    public int Idmateriau { get; set; }

    [Column("idcouleur")]
    public int Idcouleur { get; set; }

    [Column("idtaille")]
    public int Idtaille { get; set; }

    [Column("idarticle")]
    public int Idarticle { get; set; }

    [Column("idtypevae")]
    public int Idtypevae { get; set; }

    [Column("idtaillebatterie")]
    public int? Idtaillebatterie { get; set; }

    [ForeignKey("Idtaillebatterie")]
    [InverseProperty("Peutavoir4s")]
    public virtual Batterie? IdtaillebatterieNavigation { get; set; }

    [ForeignKey("Idmateriau, Idcouleur, Idtaille, Idarticle, Idtypevae")]
    [InverseProperty("Peutavoir4s")]
    public virtual Veloelectrique Veloelectrique { get; set; } = null!;
}
