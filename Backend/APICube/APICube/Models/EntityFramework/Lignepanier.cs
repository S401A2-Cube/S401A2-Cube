using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("lignepanier")]
[Index("Idclient", Name = "panier2_fk")]
[Index("Idcommande", Name = "panier3_fk")]
[Index("Idarticle", Name = "panier_fk")]
[Index("Idpanier", Name = "panier_pk", IsUnique = true)]
public partial class Lignepanier
{
    [Key]
    [Column("idpanier")]
    public int Idpanier { get; set; }

    [Column("idarticle")]
    public int Idarticle { get; set; }

    [Column("idclient")]
    public int Idclient { get; set; }

    [Column("idcommande")]
    public int Idcommande { get; set; }

    [Column("qtepanier")]
    public int Qtepanier { get; set; }

    [ForeignKey("Idarticle")]
    [InverseProperty("Lignepaniers")]
    public virtual Article IdarticleNavigation { get; set; } = null!;

    [ForeignKey("Idclient")]
    [InverseProperty("Lignepaniers")]
    public virtual Client IdclientNavigation { get; set; } = null!;

    [ForeignKey("Idcommande")]
    [InverseProperty("Lignepaniers")]
    public virtual Commande IdcommandeNavigation { get; set; } = null!;
}
