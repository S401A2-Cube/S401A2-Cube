using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("adresse")]
[Index("Idadresse", Name = "adresse_pk", IsUnique = true)]
public partial class Adresse
{
    [Key]
    [Column("idadresse")]
    public int Idadresse { get; set; }

    [Column("rue")]
    [StringLength(100)]
    public string Rue { get; set; } = null!;

    [Column("codepostal")]
    [StringLength(10)]
    public string Codepostal { get; set; } = null!;

    [Column("ville")]
    [StringLength(60)]
    public string Ville { get; set; } = null!;

    [Column("pays")]
    [StringLength(40)]
    public string Pays { get; set; } = null!;

    [InverseProperty("AdrIdadresseNavigation")]
    public virtual ICollection<Commande> CommandeAdrIdadresseNavigations { get; set; } = new List<Commande>();

    [InverseProperty("IdadresseNavigation")]
    public virtual ICollection<Commande> CommandeIdadresseNavigations { get; set; } = new List<Commande>();

    [InverseProperty("IdadresseNavigation")]
    public virtual ICollection<Enregistre> Enregistres { get; set; } = new List<Enregistre>();
}
