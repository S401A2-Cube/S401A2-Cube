using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("geometrie")]
[Index("Idgeometrie", Name = "geometrie_pk", IsUnique = true)]
public partial class Geometrie
{
    [Key]
    [Column("idgeometrie")]
    public int Idgeometrie { get; set; }

    [Column("nompiece")]
    [StringLength(100)]
    public string Nompiece { get; set; } = null!;

    [Column("taillepiece")]
    public decimal Taillepiece { get; set; }

    [InverseProperty("IdgeometrieNavigation")]
    public virtual ICollection<Correspondum> Corresponda { get; set; } = new List<Correspondum>();
}
