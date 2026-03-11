using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("estcaracterisepar")]
[Index("Idcaracteristique", Name = "estcaracterisepar2_fk")]
[Index("Idarticle", Name = "estcaracterisepar_fk")]
[Index("Idestcaracterisepar", Name = "estcaracterisepar_pk", IsUnique = true)]
public partial class Estcaracterisepar
{
    [Key]
    [Column("idestcaracterisepar")]
    public int Idestcaracterisepar { get; set; }

    [Column("idarticle")]
    public int Idarticle { get; set; }

    [Column("idcaracteristique")]
    public int Idcaracteristique { get; set; }

    [Column("valeurcaracterisque")]
    [StringLength(5000)]
    public string? Valeurcaracterisque { get; set; }

    [ForeignKey("Idarticle")]
    [InverseProperty("Estcaracterisepars")]
    public virtual Article IdarticleNavigation { get; set; } = null!;

    [ForeignKey("Idcaracteristique")]
    [InverseProperty("Estcaracterisepars")]
    public virtual Caracteristique IdcaracteristiqueNavigation { get; set; } = null!;
}
