using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("civilite")]
[Index("Idcivilite", Name = "civilite_pk", IsUnique = true)]
public partial class Civilite
{
    [Key]
    [Column("idcivilite")]
    public int Idcivilite { get; set; }

    [Column("nomcivilite")]
    [StringLength(10)]
    public string Nomcivilite { get; set; } = null!;

    [InverseProperty("IdciviliteNavigation")]
    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
