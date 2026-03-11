using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("batterie")]
[Index("Idtaillebatterie", Name = "batterie_pk", IsUnique = true)]
public partial class Batterie
{
    [Key]
    [Column("idtaillebatterie")]
    public int Idtaillebatterie { get; set; }

    [Column("taillebatterie")]
    public decimal Taillebatterie { get; set; }

    [InverseProperty("IdtaillebatterieNavigation")]
    public virtual ICollection<Peutavoir4> Peutavoir4s { get; set; } = new List<Peutavoir4>();

    [InverseProperty("IdtaillebatterieNavigation")]
    public virtual ICollection<Veloelectrique> Veloelectriques { get; set; } = new List<Veloelectrique>();
}
