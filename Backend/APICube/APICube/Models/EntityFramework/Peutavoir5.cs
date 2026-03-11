using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("peutavoir5")]
[Index("Idpeutavoir5", Name = "peutavoir5_pk", IsUnique = true)]
[Index("Idmateriau", Name = "peutavoir7_fk")]
[Index("VelIdmateriau", "Idcouleur", "Idtaille", "Idarticle", Name = "peutavoir8_fk")]
public partial class Peutavoir5
{
    [Key]
    [Column("idpeutavoir5")]
    public int Idpeutavoir5 { get; set; }

    [Column("idmateriau")]
    public int Idmateriau { get; set; }

    [Column("vel_idmateriau")]
    public int VelIdmateriau { get; set; }

    [Column("idcouleur")]
    public int Idcouleur { get; set; }

    [Column("idtaille")]
    public int Idtaille { get; set; }

    [Column("idarticle")]
    public int Idarticle { get; set; }

    [ForeignKey("Idmateriau")]
    [InverseProperty("Peutavoir5s")]
    public virtual Cadre IdmateriauNavigation { get; set; } = null!;

    [ForeignKey("VelIdmateriau, Idcouleur, Idtaille, Idarticle")]
    [InverseProperty("Peutavoir5s")]
    public virtual Velo Velo { get; set; } = null!;
}
