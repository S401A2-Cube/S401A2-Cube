using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("taille")]
[Index("Libelletaille", Name = "idx_taille_libelle")]
[Index("Idtaille", Name = "taille_pk", IsUnique = true)]
public partial class Taille
{
    [Key]
    [Column("idtaille")]
    public int Idtaille { get; set; }

    [Column("libelletaille")]
    [StringLength(50)]
    public string Libelletaille { get; set; } = null!;

    [InverseProperty("IdtailleNavigation")]
    public virtual ICollection<Accessoire> Accessoires { get; set; } = new List<Accessoire>();

    [InverseProperty("IdtailleNavigation")]
    public virtual ICollection<Correspondum> Corresponda { get; set; } = new List<Correspondum>();

    [InverseProperty("IdtailleNavigation")]
    public virtual ICollection<Peutavoir> Peutavoirs { get; set; } = new List<Peutavoir>();

    [InverseProperty("IdtailleNavigation")]
    public virtual ICollection<Velo> Velos { get; set; } = new List<Velo>();
}
