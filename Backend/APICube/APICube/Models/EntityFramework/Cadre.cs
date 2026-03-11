using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("cadre")]
[Index("Idmateriau", Name = "cadre_pk", IsUnique = true)]
[Index("Formecadre", Name = "idx_cadre_forme")]
[Index("Nommateriau", Name = "idx_cadre_nom")]
public partial class Cadre
{
    [Key]
    [Column("idmateriau")]
    public int Idmateriau { get; set; }

    [Column("nommateriau")]
    [StringLength(50)]
    public string Nommateriau { get; set; } = null!;

    [Column("formecadre")]
    [StringLength(50)]
    public string Formecadre { get; set; } = null!;

    [InverseProperty("IdmateriauNavigation")]
    public virtual ICollection<Peutavoir5> Peutavoir5s { get; set; } = new List<Peutavoir5>();

    [InverseProperty("IdmateriauNavigation")]
    public virtual ICollection<Velo> Velos { get; set; } = new List<Velo>();
}
