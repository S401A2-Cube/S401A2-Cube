using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("date")]
[Index("Iddate", Name = "date_pk", IsUnique = true)]
public partial class Date
{
    [Key]
    [Column("iddate")]
    public int Iddate { get; set; }

    [Column("datedate")]
    public DateOnly Datedate { get; set; }

    [InverseProperty("IddateNavigation")]
    public virtual ICollection<Est> Ests { get; set; } = new List<Est>();
}
