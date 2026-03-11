using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("codepromo")]
[Index("Estactif", Name = "idx_codepromo_actif")]
[Index("Codepromo1", Name = "idx_codepromo_code", IsUnique = true)]
public partial class Codepromo
{
    [Key]
    [Column("idcodepromo")]
    public int Idcodepromo { get; set; }

    [Column("codepromo")]
    [StringLength(50)]
    public string Codepromo1 { get; set; } = null!;

    [Column("pourcentpromo")]
    public decimal Pourcentpromo { get; set; }

    [Column("livraisongratuite")]
    public bool Livraisongratuite { get; set; }

    [Column("estactif")]
    public bool Estactif { get; set; }

    [InverseProperty("IdcodepromoNavigation")]
    public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();
}
