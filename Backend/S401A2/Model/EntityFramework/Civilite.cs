using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APICube.Models.EntityFramework;
[Index(nameof(Id), Name = "civilite_pk", IsUnique = true)]
[Table("t_e_civilite_civ")]
public partial class Civilite
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("civ_id")]
    public int Id { get; set; }

    [Required]
    [StringLength(25)]
    [Column("div_nom")]
    public string Nom { get; set; } = null!;

    [InverseProperty(nameof(Client.CiviliteClient))]
    public virtual ICollection<Client> CiviliteClient { get; set; } = new List<Client>();
}