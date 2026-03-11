using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("client")]
[Index("Idcivilite", Name = "choisit_fk")]
[Index("Idclient", Name = "client_pk", IsUnique = true)]
[Index("Datederniereactivite", Name = "idx_client_derniere_activite")]
[Index("Email", Name = "idx_client_email", IsUnique = true)]
public partial class Client
{
    [Key]
    [Column("idclient")]
    public int Idclient { get; set; }

    [Column("idcivilite")]
    public int Idcivilite { get; set; }

    [Column("nomclient")]
    [StringLength(50)]
    public string Nomclient { get; set; } = null!;

    [Column("prenomclient")]
    [StringLength(50)]
    public string Prenomclient { get; set; } = null!;

    [Column("email")]
    [StringLength(50)]
    public string Email { get; set; } = null!;

    [Column("motdepasse")]
    [StringLength(255)]
    public string? Motdepasse { get; set; }

    [Column("datenaissance")]
    public DateOnly? Datenaissance { get; set; }

    [Column("newsletter")]
    public bool Newsletter { get; set; }

    [Column("datederniereactivite")]
    public DateOnly Datederniereactivite { get; set; }

    [Column("role")]
    [StringLength(10)]
    public string? Role { get; set; }

    [Column("remember_token")]
    [StringLength(100)]
    public string? RememberToken { get; set; }

    [Column("google_id")]
    [StringLength(255)]
    public string? GoogleId { get; set; }

    [Column("two_factor_code")]
    [StringLength(255)]
    public string? TwoFactorCode { get; set; }

    [Column("two_factor_expires_at", TypeName = "timestamp(0) without time zone")]
    public DateTime? TwoFactorExpiresAt { get; set; }

    [Column("mobile")]
    [StringLength(10)]
    public string? Mobile { get; set; }

    [Column("is_2fa_enabled")]
    public bool? Is2faEnabled { get; set; }

    [InverseProperty("IdclientNavigation")]
    public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();

    [InverseProperty("IdclientNavigation")]
    public virtual ICollection<Enregistre> Enregistres { get; set; } = new List<Enregistre>();

    [ForeignKey("Idcivilite")]
    [InverseProperty("Clients")]
    public virtual Civilite IdciviliteNavigation { get; set; } = null!;

    [InverseProperty("IdclientNavigation")]
    public virtual ICollection<Lignepanier> Lignepaniers { get; set; } = new List<Lignepanier>();
}
