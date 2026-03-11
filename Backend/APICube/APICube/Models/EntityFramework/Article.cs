using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("article")]
[Index("Idcategorie", Name = "appartienta_fk")]
[Index("Idarticle", Name = "article_pk", IsUnique = true)]
[Index("Disponibiliteenligne", Name = "idx_article_dispo")]
[Index("Nomarticle", Name = "idx_article_nom")]
[Index("Prix", Name = "idx_article_prix")]
[Index("Reference", Name = "idx_article_reference")]
[Index("Qtestock", Name = "idx_article_stock")]
public partial class Article
{
    [Key]
    [Column("idarticle")]
    public int Idarticle { get; set; }

    [Column("idcategorie")]
    public int Idcategorie { get; set; }

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
    public virtual Accessoire? Accessoire { get; set; }

    [InverseProperty("IdarticleNavigation")]
    public virtual ICollection<Estcaracterisepar> Estcaracterisepars { get; set; } = new List<Estcaracterisepar>();

    [InverseProperty("IdarticleNavigation")]
    public virtual ICollection<Estdisponibledan> Estdisponibledans { get; set; } = new List<Estdisponibledan>();

    [InverseProperty("ArtIdarticleNavigation")]
    public virtual ICollection<Estsimilairea> EstsimilaireaArtIdarticleNavigations { get; set; } = new List<Estsimilairea>();

    [InverseProperty("IdarticleNavigation")]
    public virtual ICollection<Estsimilairea> EstsimilaireaIdarticleNavigations { get; set; } = new List<Estsimilairea>();

    [InverseProperty("IdarticleNavigation")]
    public virtual ICollection<Faitreferencea> Faitreferenceas { get; set; } = new List<Faitreferencea>();

    [ForeignKey("Idcategorie")]
    [InverseProperty("Articles")]
    public virtual Categorie IdcategorieNavigation { get; set; } = null!;

    [InverseProperty("IdarticleNavigation")]
    public virtual ICollection<Lignepanier> Lignepaniers { get; set; } = new List<Lignepanier>();

    [InverseProperty("IdarticleNavigation")]
    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

    [InverseProperty("IdarticleNavigation")]
    public virtual ICollection<Veloelectrique> Veloelectriques { get; set; } = new List<Veloelectrique>();

    [InverseProperty("IdarticleNavigation")]
    public virtual ICollection<Velo> Velos { get; set; } = new List<Velo>();
}
