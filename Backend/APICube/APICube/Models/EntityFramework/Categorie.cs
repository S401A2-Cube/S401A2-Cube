using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("categorie")]
[Index("Idcategorie", Name = "categorie_pk", IsUnique = true)]
[Index("CatIdcategorie", Name = "estsouscategorie_fk")]
[Index("Nomcategorie", Name = "idx_categorie_nom")]
public partial class Categorie
{
    [Key]
    [Column("idcategorie")]
    public int Idcategorie { get; set; }

    [Column("cat_idcategorie")]
    public int? CatIdcategorie { get; set; }

    [Column("nomcategorie")]
    [StringLength(60)]
    public string Nomcategorie { get; set; } = null!;

    [InverseProperty("IdcategorieNavigation")]
    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    [ForeignKey("CatIdcategorie")]
    [InverseProperty("InverseCatIdcategorieNavigation")]
    public virtual Categorie? CatIdcategorieNavigation { get; set; }

    [InverseProperty("CatIdcategorieNavigation")]
    public virtual ICollection<Categorie> InverseCatIdcategorieNavigation { get; set; } = new List<Categorie>();
}
