using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("modele")]
[Index("Nommodele", Name = "idx_modele_nom")]
[Index("Idmodele", Name = "modele_pk", IsUnique = true)]
public partial class Modele
{
    [Key]
    [Column("idmodele")]
    public int Idmodele { get; set; }

    [Column("nommodele")]
    [StringLength(100)]
    public string Nommodele { get; set; } = null!;

    [InverseProperty("IdmodeleNavigation")]
    public virtual ICollection<Velo> Velos { get; set; } = new List<Velo>();
}
