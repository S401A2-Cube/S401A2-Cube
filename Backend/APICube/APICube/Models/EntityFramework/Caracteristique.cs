using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("caracteristique")]
[Index("Idcaracteristique", Name = "caracteristique_pk", IsUnique = true)]
[Index("Idtypecaracteristique", Name = "estdans_fk")]
public partial class Caracteristique
{
    [Key]
    [Column("idcaracteristique")]
    public int Idcaracteristique { get; set; }

    [Column("idtypecaracteristique")]
    public int Idtypecaracteristique { get; set; }

    [Column("nomcaracteristique")]
    [StringLength(60)]
    public string? Nomcaracteristique { get; set; }

    [InverseProperty("IdcaracteristiqueNavigation")]
    public virtual ICollection<Estcaracterisepar> Estcaracterisepars { get; set; } = new List<Estcaracterisepar>();

    [ForeignKey("Idtypecaracteristique")]
    [InverseProperty("Caracteristiques")]
    public virtual Typecaracteristique IdtypecaracteristiqueNavigation { get; set; } = null!;
}
