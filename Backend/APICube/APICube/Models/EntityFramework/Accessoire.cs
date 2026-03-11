using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("accessoire")]
[Index("Idarticle", Name = "accessoire_pk", IsUnique = true)]
[Index("Disponibiliteenligne", Name = "idx_accessoire_dispo")]
[Index("Nomarticle", Name = "idx_accessoire_nom")]
[Index("Prix", Name = "idx_accessoire_prix")]
[Index("Reference", Name = "idx_accessoire_reference")]
[Index("Qtestock", Name = "idx_accessoire_stock")]
public partial class Accessoire
{
    [Key]
    [Column("idarticle")]
    public int Idarticle { get; set; }

    [Column("idmatiere")]
    public int? Idmatiere { get; set; }

    [Column("idtaille")]
    public int? Idtaille { get; set; }

    [Column("idannee")]
    public int? Idannee { get; set; }

    [Column("idcategorie")]
    public int? Idcategorie { get; set; }

    [Column("reference")]
    [StringLength(20)]
    public string Reference { get; set; } = null!;

    [Column("prix")]
    public decimal Prix { get; set; }

    [Column("nomarticle")]
    [StringLength(100)]
    public string Nomarticle { get; set; } = null!;

    [Column("description")]
    [StringLength(5000)]
    public string? Description { get; set; }

    [Column("poids")]
    public decimal Poids { get; set; }

    [Column("disponibiliteenligne")]
    public bool Disponibiliteenligne { get; set; }

    [Column("resume")]
    [StringLength(5000)]
    public string? Resume { get; set; }

    [Column("pourcentpromotion")]
    public decimal? Pourcentpromotion { get; set; }

    [Column("qtestock")]
    public int? Qtestock { get; set; }

    [InverseProperty("IdarticleNavigation")]
    public virtual ICollection<Estenlienavec> Estenlienavecs { get; set; } = new List<Estenlienavec>();

    [ForeignKey("Idannee")]
    [InverseProperty("Accessoires")]
    public virtual Anneesortie? IdanneeNavigation { get; set; }

    [ForeignKey("Idarticle")]
    [InverseProperty("Accessoire")]
    public virtual Article IdarticleNavigation { get; set; } = null!;

    [ForeignKey("Idmatiere")]
    [InverseProperty("Accessoires")]
    public virtual Matiere? IdmatiereNavigation { get; set; }

    [ForeignKey("Idtaille")]
    [InverseProperty("Accessoires")]
    public virtual Taille? IdtailleNavigation { get; set; }
}
