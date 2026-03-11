using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("motcle")]
[Index("Nommotcle", Name = "idx_motcle_nom")]
[Index("Idmotcle", Name = "motcle_pk", IsUnique = true)]
public partial class Motcle
{
    [Key]
    [Column("idmotcle")]
    public int Idmotcle { get; set; }

    [Column("nommotcle")]
    [StringLength(100)]
    public string Nommotcle { get; set; } = null!;

    [InverseProperty("IdmotcleNavigation")]
    public virtual ICollection<Faitreferencea> Faitreferenceas { get; set; } = new List<Faitreferencea>();
}
