using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("magasinpartenaire")]
[Index("Idnommagasin", Name = "magasinpartenaire_pk", IsUnique = true)]
public partial class Magasinpartenaire
{
    [Key]
    [Column("idnommagasin")]
    public int Idnommagasin { get; set; }

    [Column("nommagasin")]
    [StringLength(60)]
    public string Nommagasin { get; set; } = null!;

    [Column("latitude")]
    [Precision(9, 6)]
    public decimal? Latitude { get; set; }

    [Column("longitude")]
    [Precision(9, 6)]
    public decimal? Longitude { get; set; }

    [Column("ville")]
    [StringLength(100)]
    public string? Ville { get; set; }

    [InverseProperty("IdnommagasinNavigation")]
    public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();

    [InverseProperty("IdnommagasinNavigation")]
    public virtual ICollection<Estdisponibledan> Estdisponibledans { get; set; } = new List<Estdisponibledan>();

    [InverseProperty("IdnommagasinNavigation")]
    public virtual ICollection<Estouvert> Estouverts { get; set; } = new List<Estouvert>();
}
