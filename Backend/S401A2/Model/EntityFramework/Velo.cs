using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using S401A2.Model.EntityFramework;

namespace APICube.Models.EntityFramework
{
    [Table("t_e_velo_vel")]
    [PrimaryKey(nameof(IdVelo))]
    public partial class Velo
    {
        [Key]
        [Column("vel_idVelo")]
        public int IdVelo { get; set; }

        [Column("art_idarticle")]
        public int IdArticle { get; set; }

        [Column("vel_lienvue360")]
        [StringLength(150)]
        public string LienVue360 { get; set; }

        [Column("mod_idmodele")]
        public int IdModele { get; set; }

        [ForeignKey(nameof(IdModele))]
        [InverseProperty("Velos")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [ValidateNever]
        public virtual Modele? ModeleVelo { get; set; }

        [ForeignKey(nameof(IdArticle))]
        [InverseProperty("Velos")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [ValidateNever]
        public virtual Article? Article { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [ValidateNever]
        public virtual ICollection<Couleur>? Couleurs { get; set; } = new List<Couleur>();

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [ValidateNever]
        public virtual ICollection<Taille>? Tailles { get; set; } = new List<Taille>();

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [ValidateNever]
        public virtual ICollection<Cadre>? Cadres { get; set; } = new List<Cadre>();

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [ValidateNever]
        public virtual ICollection<Geometrie>? Geometries { get; set; } = new List<Geometrie>();

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [ValidateNever]
        public virtual ICollection<Millesime>? Millesimes { get; set; } = new List<Millesime>();
    }
}