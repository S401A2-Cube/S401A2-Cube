using Microsoft.EntityFrameworkCore;
using S401A2.Model.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICube.Models.EntityFramework;
[Index(nameof(Id))]
[Index(nameof(AdresseIdLivr))]
[Index(nameof(ClientId))]
[Table("t_e_commande_com")]

public partial class Commande

{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("com_id")]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    [Column("com_livraison")]
    public string Livraison { get; set; } = null!;

    [Column("adr_idlivraison")]
    public int AdresseIdLivr { get; set; }

    [Column("adr_idfacturation")]
    public int? AdresseIdFact { get; set; }

    [Column("cli_id")]
    public int ClientId { get; set; }

    [ForeignKey(nameof(AdresseIdLivr))]
    [InverseProperty(nameof(Adresse.AdresseCommandeLivr))]
    public virtual Adresse AdresseCommandeLivr { get; set; } = null!;

    [ForeignKey(nameof(AdresseIdFact))]
    [InverseProperty(nameof(Adresse.AdresseCommandeFact))]
    public virtual Adresse AdresseCommandeFact { get; set; } = null!;

    [ForeignKey(nameof(ClientId))]
    [InverseProperty(nameof(Client.ClientCommande))]
    public virtual Client ClientCommande { get; set; } = null!;

    [InverseProperty(nameof(Article.ArticleCommande))]
    public virtual ICollection<Article> ArticleCommande { get; set; } = new List<Article>();
}
/*     (pk) commandeId int
 *     modelivraison string
 *     (fk) adresseId int
 *     (fk) adresseId int
 *     (fk) clientId int
 *     */