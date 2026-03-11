using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("enregistre")]
[Index("Idadresse", Name = "enregistre2_fk")]
[Index("Idclient", Name = "enregistre_fk")]
[Index("Idenregistre", Name = "enregistre_pk", IsUnique = true)]
public partial class Enregistre
{
    [Key]
    [Column("idenregistre")]
    public int Idenregistre { get; set; }

    [Column("idclient")]
    public int Idclient { get; set; }

    [Column("idadresse")]
    public int Idadresse { get; set; }

    [ForeignKey("Idadresse")]
    [InverseProperty("Enregistres")]
    public virtual Adresse IdadresseNavigation { get; set; } = null!;

    [ForeignKey("Idclient")]
    [InverseProperty("Enregistres")]
    public virtual Client IdclientNavigation { get; set; } = null!;
}
