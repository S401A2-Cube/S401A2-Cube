using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("peutavoir")]
[Index("Idtaille", Name = "peutavoir2_fk")]
[Index("Idmateriau", "Idcouleur", "VelIdtaille", "Idarticle", Name = "peutavoir_fk")]
[Index("Idpeutavoir", Name = "peutavoir_pk", IsUnique = true)]
public partial class Peutavoir
{
    [Key]
    [Column("idpeutavoir")]
    public int Idpeutavoir { get; set; }

    [Column("idmateriau")]
    public int Idmateriau { get; set; }

    [Column("idcouleur")]
    public int Idcouleur { get; set; }

    [Column("vel_idtaille")]
    public int VelIdtaille { get; set; }

    [Column("idarticle")]
    public int Idarticle { get; set; }

    [Column("idtaille")]
    public int Idtaille { get; set; }

    [ForeignKey("Idtaille")]
    [InverseProperty("Peutavoirs")]
    public virtual Taille IdtailleNavigation { get; set; } = null!;
}
