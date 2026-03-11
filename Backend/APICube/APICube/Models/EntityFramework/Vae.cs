using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("vae")]
[Index("Nomtypevae", Name = "idx_vae_nom")]
[Index("Idtypevae", Name = "vae_pk", IsUnique = true)]
public partial class Vae
{
    [Key]
    [Column("idtypevae")]
    public int Idtypevae { get; set; }

    [Column("nomtypevae")]
    [StringLength(60)]
    public string Nomtypevae { get; set; } = null!;

    [InverseProperty("IdtypevaeNavigation")]
    public virtual ICollection<Veloelectrique> Veloelectriques { get; set; } = new List<Veloelectrique>();
}
