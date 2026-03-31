using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using APICube.Models.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;


namespace S401A2.Model.EntityFramework
{
    [Index(nameof(Id))]
    [Index(nameof(ClientId))]
    [Index(nameof(QtePanier))]
    [Table("t_e_lignepanier_lig")]
    public partial class LignePanier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("lig_idpanier")]
        public int Id { get; set; }

        [Column("art_id")]
        public int ArticleId { get; set; }

        [Column("cli_id")]
        public int ClientId { get; set; }

        [Column("com_id")]
        public int CommandeId { get; set; }

        [Column("clr_id")]
        public int? CouleurId { get; set; } 

        [Column("tli_id")]
        public int? TailleId { get; set; } 

        [Required]
        [Range(1, 100)]
        [Column("lig_qtepanier")]
        public int QtePanier { get; set; }

        [ForeignKey(nameof(ArticleId))]
        [InverseProperty(nameof(Article.ArticleLignePanier))]
        public virtual Article ArticleLignePanier { get; set; } = null!;

        [ForeignKey(nameof(ClientId))]
        [InverseProperty(nameof(Client.ClientLignePanier))]
        public virtual Client ClientLignePanier { get; set; } = null!;

        [ForeignKey(nameof(CouleurId))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual Couleur? CouleurChoisie { get; set; }

        [ForeignKey(nameof(TailleId))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual Taille? TailleChoisie { get; set; }
    }

    /*     (pk) idpanier
     *     (fk) idarticle
     *     (fk) idclient
     *     (fk) idCommande
     *     qtePanier
*/
}
