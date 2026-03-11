using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("estsimilairea")]
[Index("ArtIdarticle", Name = "estsimilairea2_fk")]
[Index("Idarticle", Name = "estsimilairea_fk")]
[Index("Idestsimilairea", Name = "estsimilairea_pk", IsUnique = true)]
public partial class Estsimilairea
{
    [Key]
    [Column("idestsimilairea")]
    public int Idestsimilairea { get; set; }

    [Column("idarticle")]
    public int Idarticle { get; set; }

    [Column("art_idarticle")]
    public int ArtIdarticle { get; set; }

    [ForeignKey("ArtIdarticle")]
    [InverseProperty("EstsimilaireaArtIdarticleNavigations")]
    public virtual Article ArtIdarticleNavigation { get; set; } = null!;

    [ForeignKey("Idarticle")]
    [InverseProperty("EstsimilaireaIdarticleNavigations")]
    public virtual Article IdarticleNavigation { get; set; } = null!;
}
