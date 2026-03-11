using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("typecaracteristique")]
[Index("Idtypecaracteristique", Name = "typecaracteristique_pk", IsUnique = true)]
public partial class Typecaracteristique
{
    [Key]
    [Column("idtypecaracteristique")]
    public int Idtypecaracteristique { get; set; }

    [Column("nomtypecaracteristique")]
    [StringLength(60)]
    public string Nomtypecaracteristique { get; set; } = null!;

    [InverseProperty("IdtypecaracteristiqueNavigation")]
    public virtual ICollection<Caracteristique> Caracteristiques { get; set; } = new List<Caracteristique>();
}
