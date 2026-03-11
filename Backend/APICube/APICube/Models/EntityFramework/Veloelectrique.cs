using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[PrimaryKey("Idmateriau", "Idcouleur", "Idtaille", "Idarticle", "Idtypevae")]
[Table("veloelectrique")]
[Index("Idtypevae", Name = "a4_fk")]
[Index("Idtaillebatterie", Name = "a5_fk")]
[Index("Idmateriau", "Idcouleur", "Idtaille", "Idarticle", Name = "heritage_3_fk")]
[Index("Disponibiliteenligne", Name = "idx_veloelectrique_dispo")]
[Index("Nomarticle", Name = "idx_veloelectrique_nom")]
[Index("Prix", Name = "idx_veloelectrique_prix")]
[Index("Reference", Name = "idx_veloelectrique_reference")]
[Index("Qtestock", Name = "idx_veloelectrique_stock")]
[Index("Idmateriau", "Idcouleur", "Idtaille", "Idarticle", "Idtypevae", Name = "veloelectrique_pk", IsUnique = true)]
public partial class Veloelectrique
{
    [Key]
    [Column("idmateriau")]
    public int Idmateriau { get; set; }

    [Key]
    [Column("idcouleur")]
    public int Idcouleur { get; set; }

    [Key]
    [Column("idtaille")]
    public int Idtaille { get; set; }

    [Key]
    [Column("idarticle")]
    public int Idarticle { get; set; }

    [Key]
    [Column("idtypevae")]
    public int Idtypevae { get; set; }

    [Column("idusage")]
    public int Idusage { get; set; }

    [Column("idtaillebatterie")]
    public int? Idtaillebatterie { get; set; }

    [Column("idmodele")]
    public int? Idmodele { get; set; }

    [Column("idmillesime")]
    public int? Idmillesime { get; set; }

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

    [Column("lienvue360")]
    [StringLength(150)]
    public string? Lienvue360 { get; set; }

    [Column("qtestock")]
    public int? Qtestock { get; set; }

    [ForeignKey("Idarticle")]
    [InverseProperty("Veloelectriques")]
    public virtual Article IdarticleNavigation { get; set; } = null!;

    [ForeignKey("Idmillesime")]
    [InverseProperty("Veloelectriques")]
    public virtual Millesime? IdmillesimeNavigation { get; set; }

    [ForeignKey("Idtaillebatterie")]
    [InverseProperty("Veloelectriques")]
    public virtual Batterie? IdtaillebatterieNavigation { get; set; }

    [ForeignKey("Idtypevae")]
    [InverseProperty("Veloelectriques")]
    public virtual Vae IdtypevaeNavigation { get; set; } = null!;

    [ForeignKey("Idusage")]
    [InverseProperty("Veloelectriques")]
    public virtual Usage IdusageNavigation { get; set; } = null!;

    [InverseProperty("Veloelectrique")]
    public virtual ICollection<Peutavoir4> Peutavoir4s { get; set; } = new List<Peutavoir4>();

    [ForeignKey("Idmateriau, Idcouleur, Idtaille, Idarticle")]
    [InverseProperty("Veloelectriques")]
    public virtual Velo Velo { get; set; } = null!;
}
