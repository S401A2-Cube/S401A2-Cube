using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("histo_article")]
public partial class HistoArticle
{
    [Key]
    [Column("idhistoa")]
    public int Idhistoa { get; set; }

    [Column("idarticle")]
    public int Idarticle { get; set; }

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

    [Column("utilisateur")]
    [StringLength(50)]
    public string? Utilisateur { get; set; }

    [Column("datemodif")]
    public DateOnly? Datemodif { get; set; }

    [Column("typemodif")]
    [MaxLength(1)]
    public char? Typemodif { get; set; }
}
