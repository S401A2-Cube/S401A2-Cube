using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using APICube.Models.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;


namespace S401A2.Model.EntityFramework
{
    [Index(nameof(Id))]
    [Index(nameof(Ville))]
    [Index(nameof(CodePostale))]
    [Table("t_e_adresse_adr")]
    public partial class Adresse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("adr_id")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Column("adr_rue")]
        public string Rue { get; set; } = null!;

        [Required]
        [StringLength(5)]
        [Column("adr_codepostale")]
        public string CodePostale { get; set; } = null!;

        [Required]
        [StringLength(50)]
        [Column("adr_ville")]
        public string Ville { get; set; } = null!;

        [Required]
        [StringLength(50)]
        [Column("adr_pays")]
        public string Pays { get; set; } = null!;

        [InverseProperty(nameof(Client.AdresseClient))]
        public virtual ICollection<Client> AdresseClient { get; set; } = new List<Client>();

        [InverseProperty(nameof(Commande.AdresseCommandeLivr))]
        public virtual ICollection<Commande> AdresseCommandeLivr { get; set; } = new List<Commande>();

        [InverseProperty(nameof(Commande.AdresseCommandeFact))]
        public virtual ICollection<Commande> AdresseCommandeFact { get; set; } = new List<Commande>();
    }

    /*     (pk) IdAdresse int
     *     rue String
     *     codePostale  string
     *     ville        string
     *     pays         string
 *     
 *     (has one) idCommande
 *     (has one) idClient
 *     
 *     
 *     
*/
}
