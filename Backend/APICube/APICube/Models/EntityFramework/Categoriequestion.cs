using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("categoriequestion")]
public partial class Categoriequestion
{
    [Key]
    [Column("idcatquestion")]
    public int Idcatquestion { get; set; }

    [Column("nomcatquestion")]
    [StringLength(50)]
    public string? Nomcatquestion { get; set; }

    [InverseProperty("IdcatquestionNavigation")]
    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
