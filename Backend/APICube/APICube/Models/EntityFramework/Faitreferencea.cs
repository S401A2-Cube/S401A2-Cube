using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("faitreferencea")]
[Index("Idmotcle", Name = "faitreferencea2_fk")]
[Index("Idarticle", Name = "faitreferencea_fk")]
[Index("Idfaitreferencea", Name = "faitreferencea_pk", IsUnique = true)]
public partial class Faitreferencea
{
    [Key]
    [Column("idfaitreferencea")]
    public int Idfaitreferencea { get; set; }

    [Column("idarticle")]
    public int Idarticle { get; set; }

    [Column("idmotcle")]
    public int Idmotcle { get; set; }

    [ForeignKey("Idarticle")]
    [InverseProperty("Faitreferenceas")]
    public virtual Article IdarticleNavigation { get; set; } = null!;

    [ForeignKey("Idmotcle")]
    [InverseProperty("Faitreferenceas")]
    public virtual Motcle IdmotcleNavigation { get; set; } = null!;
}
