using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("photo")]
[Index("Idarticle", Name = "estassociea_fk")]
[Index("Idphoto", Name = "photo_pk", IsUnique = true)]
public partial class Photo
{
    [Key]
    [Column("idphoto")]
    public int Idphoto { get; set; }

    [Column("idarticle")]
    public int Idarticle { get; set; }

    [Column("lienphoto")]
    [StringLength(255)]
    public string Lienphoto { get; set; } = null!;

    [Column("positionphoto")]
    public int? Positionphoto { get; set; }

    [ForeignKey("Idarticle")]
    [InverseProperty("Photos")]
    public virtual Article IdarticleNavigation { get; set; } = null!;
}
