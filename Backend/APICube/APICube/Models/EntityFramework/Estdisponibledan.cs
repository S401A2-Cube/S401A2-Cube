using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("estdisponibledans")]
[Index("Idnommagasin", Name = "estdisponibledans2_fk")]
[Index("Idarticle", Name = "estdisponibledans_fk")]
[Index("Idestdisponibledans", Name = "estdisponibledans_pk", IsUnique = true)]
public partial class Estdisponibledan
{
    [Key]
    [Column("idestdisponibledans")]
    public int Idestdisponibledans { get; set; }

    [Column("idarticle")]
    public int Idarticle { get; set; }

    [Column("idnommagasin")]
    public int Idnommagasin { get; set; }

    [Column("disponibilite")]
    public bool Disponibilite { get; set; }

    [ForeignKey("Idarticle")]
    [InverseProperty("Estdisponibledans")]
    public virtual Article IdarticleNavigation { get; set; } = null!;

    [ForeignKey("Idnommagasin")]
    [InverseProperty("Estdisponibledans")]
    public virtual Magasinpartenaire IdnommagasinNavigation { get; set; } = null!;
}
