using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("millesime")]
[Index("Annee", Name = "idx_millesime_annee")]
[Index("Idmillesime", Name = "millesime_pk", IsUnique = true)]
public partial class Millesime
{
    [Key]
    [Column("idmillesime")]
    public int Idmillesime { get; set; }

    [Column("annee")]
    public int Annee { get; set; }

    [InverseProperty("IdmillesimeNavigation")]
    public virtual ICollection<Veloelectrique> Veloelectriques { get; set; } = new List<Veloelectrique>();

    [InverseProperty("IdmillesimeNavigation")]
    public virtual ICollection<Velo> Velos { get; set; } = new List<Velo>();
}
