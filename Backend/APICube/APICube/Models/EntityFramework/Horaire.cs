using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("horaires")]
[Index("Idhoraire", Name = "horaires_pk", IsUnique = true)]
public partial class Horaire
{
    [Key]
    [Column("idhoraire")]
    public int Idhoraire { get; set; }

    [Column("heuredebut")]
    public TimeOnly Heuredebut { get; set; }

    [Column("heurefin")]
    public TimeOnly Heurefin { get; set; }

    [InverseProperty("IdhoraireNavigation")]
    public virtual ICollection<Estouvert> Estouverts { get; set; } = new List<Estouvert>();
}
