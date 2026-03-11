using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("estenlienavec")]
[Index("Idarticle", Name = "estenlienavec2_fk")]
[Index("Idmateriau", "Idcouleur", "Idtaille", "VelIdarticle", Name = "estenlienavec_fk")]
[Index("Idestenlienavec", Name = "estenlienavec_pk", IsUnique = true)]
public partial class Estenlienavec
{
    [Key]
    [Column("idestenlienavec")]
    public int Idestenlienavec { get; set; }

    [Column("idmateriau")]
    public int Idmateriau { get; set; }

    [Column("idcouleur")]
    public int Idcouleur { get; set; }

    [Column("idtaille")]
    public int Idtaille { get; set; }

    [Column("vel_idarticle")]
    public int VelIdarticle { get; set; }

    [Column("idarticle")]
    public int Idarticle { get; set; }

    [ForeignKey("Idarticle")]
    [InverseProperty("Estenlienavecs")]
    public virtual Accessoire IdarticleNavigation { get; set; } = null!;

    [ForeignKey("Idmateriau, Idcouleur, Idtaille, VelIdarticle")]
    [InverseProperty("Estenlienavecs")]
    public virtual Velo Velo { get; set; } = null!;
}
