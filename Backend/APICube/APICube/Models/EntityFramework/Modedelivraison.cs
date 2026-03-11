using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("modedelivraison")]
[Index("Idmodelivraison", Name = "modedelivraison_pk", IsUnique = true)]
public partial class Modedelivraison
{
    [Key]
    [Column("idmodelivraison")]
    public int Idmodelivraison { get; set; }

    [Column("nommodelivraison")]
    [StringLength(30)]
    public string Nommodelivraison { get; set; } = null!;

    [InverseProperty("IdmodelivraisonNavigation")]
    public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();
}
