using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[PrimaryKey("Idmateriau", "Idcouleur", "Idtaille", "Idarticle")]
[Table("velomusculaire")]
[Index("Idmateriau", "Idcouleur", "Idtaille", "Idarticle", Name = "velomusculaire_pk", IsUnique = true)]
public partial class Velomusculaire
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

    [Column("promotion")]
    public decimal? Promotion { get; set; }

    [Column("lienvue360")]
    [StringLength(150)]
    public string? Lienvue360 { get; set; }
}
