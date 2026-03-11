using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[PrimaryKey("Idmateriau", "Idcouleur", "Idtaille", "Idarticle")]
[Table("velo")]
[Index("Idmateriau", Name = "a2_fk")]
[Index("Idcouleur", Name = "a3_fk")]
[Index("Idtaille", Name = "a6_fk")]
[Index("Idmodele", Name = "estde_fk")]
[Index("Idmillesime", Name = "estsortien_fk")]
[Index("Idarticle", Name = "heritage_1_fk")]
[Index("Disponibiliteenligne", Name = "idx_velo_dispo")]
[Index("Nomarticle", Name = "idx_velo_nom")]
[Index("Prix", Name = "idx_velo_prix")]
[Index("Reference", Name = "idx_velo_reference")]
[Index("Qtestock", Name = "idx_velo_stock")]
[Index("Idmateriau", "Idcouleur", "Idtaille", "Idarticle", Name = "velo_pk", IsUnique = true)]
public partial class Velo
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

    [Column("idmodele")]
    public int Idmodele { get; set; }

    [Column("idmillesime")]
    public int Idmillesime { get; set; }

    [Column("idusage")]
    public int? Idusage { get; set; }

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

    [InverseProperty("Velo")]
    public virtual ICollection<Correspondum> Corresponda { get; set; } = new List<Correspondum>();

    [InverseProperty("Velo")]
    public virtual ICollection<Estenlienavec> Estenlienavecs { get; set; } = new List<Estenlienavec>();

    [ForeignKey("Idarticle")]
    [InverseProperty("Velos")]
    public virtual Article IdarticleNavigation { get; set; } = null!;

    [ForeignKey("Idcouleur")]
    [InverseProperty("Velos")]
    public virtual Couleur IdcouleurNavigation { get; set; } = null!;

    [ForeignKey("Idmateriau")]
    [InverseProperty("Velos")]
    public virtual Cadre IdmateriauNavigation { get; set; } = null!;

    [ForeignKey("Idmillesime")]
    [InverseProperty("Velos")]
    public virtual Millesime IdmillesimeNavigation { get; set; } = null!;

    [ForeignKey("Idmodele")]
    [InverseProperty("Velos")]
    public virtual Modele IdmodeleNavigation { get; set; } = null!;

    [ForeignKey("Idtaille")]
    [InverseProperty("Velos")]
    public virtual Taille IdtailleNavigation { get; set; } = null!;

    [ForeignKey("Idusage")]
    [InverseProperty("Velos")]
    public virtual Usage? IdusageNavigation { get; set; }

    [InverseProperty("Velo")]
    public virtual ICollection<Peutavoir3> Peutavoir3s { get; set; } = new List<Peutavoir3>();

    [InverseProperty("Velo")]
    public virtual ICollection<Peutavoir5> Peutavoir5s { get; set; } = new List<Peutavoir5>();

    [InverseProperty("Velo")]
    public virtual ICollection<Veloelectrique> Veloelectriques { get; set; } = new List<Veloelectrique>();
}
