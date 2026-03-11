using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("anneesortie")]
public partial class Anneesortie
{
    [Key]
    [Column("idannee")]
    public int Idannee { get; set; }

    [Column("anneesortie")]
    public int Anneesortie1 { get; set; }

    [InverseProperty("IdanneeNavigation")]
    public virtual ICollection<Accessoire> Accessoires { get; set; } = new List<Accessoire>();
}
