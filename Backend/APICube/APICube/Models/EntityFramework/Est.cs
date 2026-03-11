using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("est")]
[Index("Iddate", Name = "est2_fk")]
[Index("Idcommande", Name = "est_fk")]
[Index("Idest", Name = "est_pk", IsUnique = true)]
[Index("Codeetat", Name = "idx_est_codeetat")]
public partial class Est
{
    [Key]
    [Column("idest")]
    public int Idest { get; set; }

    [Column("idcommande")]
    public int Idcommande { get; set; }

    [Column("iddate")]
    public int Iddate { get; set; }

    [Column("nometat")]
    [StringLength(20)]
    public string? Nometat { get; set; }

    [Column("codeetat")]
    [StringLength(20)]
    public string Codeetat { get; set; } = null!;

    [ForeignKey("Idcommande")]
    [InverseProperty("Ests")]
    public virtual Commande IdcommandeNavigation { get; set; } = null!;

    [ForeignKey("Iddate")]
    [InverseProperty("Ests")]
    public virtual Date IddateNavigation { get; set; } = null!;
}
