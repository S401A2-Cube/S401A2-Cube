using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("matiere")]
[Index("Nommatiere", Name = "idx_matiere_nom")]
public partial class Matiere
{
    [Key]
    [Column("idmatiere")]
    public int Idmatiere { get; set; }

    [Column("nommatiere")]
    [StringLength(50)]
    public string? Nommatiere { get; set; }

    [InverseProperty("IdmatiereNavigation")]
    public virtual ICollection<Accessoire> Accessoires { get; set; } = new List<Accessoire>();
}
