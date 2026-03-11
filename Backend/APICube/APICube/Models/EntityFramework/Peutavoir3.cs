using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("peutavoir3")]
[Index("Idcouleur", Name = "peutavoir3_fk")]
[Index("Idpeutavoir3", Name = "peutavoir3_pk", IsUnique = true)]
[Index("Idmateriau", "VelIdcouleur", "Idtaille", "Idarticle", Name = "peutavoir6_fk")]
public partial class Peutavoir3
{
    [Key]
    [Column("idpeutavoir3")]
    public int Idpeutavoir3 { get; set; }

    [Column("idcouleur")]
    public int Idcouleur { get; set; }

    [Column("idmateriau")]
    public int Idmateriau { get; set; }

    [Column("vel_idcouleur")]
    public int VelIdcouleur { get; set; }

    [Column("idtaille")]
    public int Idtaille { get; set; }

    [Column("idarticle")]
    public int Idarticle { get; set; }

    [ForeignKey("Idcouleur")]
    [InverseProperty("Peutavoir3s")]
    public virtual Couleur IdcouleurNavigation { get; set; } = null!;

    [ForeignKey("Idmateriau, VelIdcouleur, Idtaille, Idarticle")]
    [InverseProperty("Peutavoir3s")]
    public virtual Velo Velo { get; set; } = null!;
}
