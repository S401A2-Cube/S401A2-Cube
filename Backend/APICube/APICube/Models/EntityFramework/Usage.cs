using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("usage")]
[Index("Nomusage", Name = "idx_usage_nom")]
public partial class Usage
{
    [Key]
    [Column("idusage")]
    public int Idusage { get; set; }

    [Column("nomusage")]
    [StringLength(50)]
    public string? Nomusage { get; set; }

    [InverseProperty("IdusageNavigation")]
    public virtual ICollection<Veloelectrique> Veloelectriques { get; set; } = new List<Veloelectrique>();

    [InverseProperty("IdusageNavigation")]
    public virtual ICollection<Velo> Velos { get; set; } = new List<Velo>();
}
