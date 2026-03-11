using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;

[Table("question")]
public partial class Question
{
    [Key]
    [Column("idquestion")]
    public int Idquestion { get; set; }

    [Column("idcatquestion")]
    public int? Idcatquestion { get; set; }

    [Column("question")]
    [StringLength(1000)]
    public string? Question1 { get; set; }

    [Column("reponse")]
    [StringLength(1000)]
    public string? Reponse { get; set; }

    [ForeignKey("Idcatquestion")]
    [InverseProperty("Questions")]
    public virtual Categoriequestion? IdcatquestionNavigation { get; set; }
}
