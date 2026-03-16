using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using S401A2.Model.EntityFramework;

namespace APICube.Models.EntityFramework;

[Index(nameof(Email), IsUnique = true)]
[Index(nameof(Prenom))]
[Index(nameof(Nom))]
[Index(nameof(Id))]
[Table("t_e_client_cli")]
public partial class Client
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("cli_id")]
    public int Id { get; set; }

    [Column("civ_id")]
    public int CiviliteId { get; set; }

    [Required]
    [StringLength(50)]
    [Column("cli_nom")]
    public string Nom { get; set; } = null!;

    [Required]
    [StringLength(50)]
    [Column("cli_prenom")]
    public string Prenom { get; set; } = null!;

    [Required]
    [StringLength(75)]
    [Column("cli_email")]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(255)]
    [Column("cli_mdp")]
    public string? Mdp { get; set; }

    [Required]
    [Column("client_datenaissance")]
    public DateOnly? DateNaissance { get; set; }

    [Required]
    [Range(0, 5)]
    [Column("cli_role")]
    public int? Role { get; set; }

    [Column("adr_id")]
    public int? AdresseId { get; set; }

    [ForeignKey(nameof(CiviliteId))]
    [InverseProperty(nameof(Civilite.CiviliteClient))]
    public virtual Civilite CiviliteClient { get; set; } = null!;

    [ForeignKey(nameof(AdresseId))]
    [InverseProperty(nameof(Adresse.AdresseClient))]
    public virtual Adresse AdresseClient { get; set; } = null!;

    [InverseProperty(nameof(LignePanier.ClientLignePanier))]
    public virtual ICollection<LignePanier> ClientLignePanier { get; set; } = new List<LignePanier>();


    [InverseProperty(nameof(Commande.ClientCommande))]
    public virtual ICollection<Commande> ClientCommande { get; set; } = new List<Commande>();
}


/*     (pk) Idclient int
 *     (fk) IdCivilite int
 *     nomClient string
 *     prenomClient string
 *     emailClient string
 *     motDePasse   string
 *     dateNaissance DateTime
 *     role          int
 *     (fk) idAdresse int
 *     
 *     
 *     (has one) idCivile
 *     (has one) idAdresse
 *     
 *     
 *     other class:
 *     commande BELONG TO
 *     lignepanier BELONG TO
*/