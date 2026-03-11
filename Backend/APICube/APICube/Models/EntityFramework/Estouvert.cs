using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("estouvert")]
[Index("Idhoraire", Name = "estouvert2_fk")]
[Index("Idnommagasin", Name = "estouvert_fk")]
[Index("Idestouvert", Name = "estouvert_pk", IsUnique = true)]
public partial class Estouvert
{
    [Key]
    [Column("idestouvert")]
    public int Idestouvert { get; set; }

    [Column("idnommagasin")]
    public int Idnommagasin { get; set; }

    [Column("idhoraire")]
    public int Idhoraire { get; set; }

    [Column("nomjour")]
    [StringLength(10)]
    public string Nomjour { get; set; } = null!;

    [ForeignKey("Idhoraire")]
    [InverseProperty("Estouverts")]
    public virtual Horaire IdhoraireNavigation { get; set; } = null!;

    [ForeignKey("Idnommagasin")]
    [InverseProperty("Estouverts")]
    public virtual Magasinpartenaire IdnommagasinNavigation { get; set; } = null!;
}
