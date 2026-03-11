using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("commande")]
[Index("Idcommande", Name = "commande_pk", IsUnique = true)]
[Index("Idadresse", Name = "estfacturea_fk")]
[Index("AdrIdadresse", Name = "estlivrea_fk")]
[Index("Idnommagasin", Name = "estlivreedans_fk")]
[Index("Idmodelivraison", Name = "estlivreeen_fk")]
[Index("Etat", Name = "idx_commande_etat")]
[Index("Idclient", Name = "passe_fk")]
public partial class Commande
{
    [Key]
    [Column("idcommande")]
    public int Idcommande { get; set; }

    [Column("idnommagasin")]
    public int? Idnommagasin { get; set; }

    [Column("idmodelivraison")]
    public int Idmodelivraison { get; set; }

    [Column("idadresse")]
    public int Idadresse { get; set; }

    [Column("adr_idadresse")]
    public int? AdrIdadresse { get; set; }

    [Column("idclient")]
    public int Idclient { get; set; }

    [Column("idcodepromo")]
    public int? Idcodepromo { get; set; }

    [Column("fraisdeport")]
    public decimal? Fraisdeport { get; set; }

    [Column("etat")]
    [StringLength(50)]
    public string? Etat { get; set; }

    [ForeignKey("AdrIdadresse")]
    [InverseProperty("CommandeAdrIdadresseNavigations")]
    public virtual Adresse? AdrIdadresseNavigation { get; set; }

    [InverseProperty("IdcommandeNavigation")]
    public virtual ICollection<Est> Ests { get; set; } = new List<Est>();

    [ForeignKey("Idadresse")]
    [InverseProperty("CommandeIdadresseNavigations")]
    public virtual Adresse IdadresseNavigation { get; set; } = null!;

    [ForeignKey("Idclient")]
    [InverseProperty("Commandes")]
    public virtual Client IdclientNavigation { get; set; } = null!;

    [ForeignKey("Idcodepromo")]
    [InverseProperty("Commandes")]
    public virtual Codepromo? IdcodepromoNavigation { get; set; }

    [ForeignKey("Idmodelivraison")]
    [InverseProperty("Commandes")]
    public virtual Modedelivraison IdmodelivraisonNavigation { get; set; } = null!;

    [ForeignKey("Idnommagasin")]
    [InverseProperty("Commandes")]
    public virtual Magasinpartenaire? IdnommagasinNavigation { get; set; }

    [InverseProperty("IdcommandeNavigation")]
    public virtual ICollection<Lignepanier> Lignepaniers { get; set; } = new List<Lignepanier>();
}
