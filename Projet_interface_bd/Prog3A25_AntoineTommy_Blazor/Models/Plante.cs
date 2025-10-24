using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Prog3A25_AntoineTommy_Blazor.Models;

[Table("Plante")]
[Index("Nom", Name = "UQ_Plante_Nom", IsUnique = true)]
public partial class Plante
{
    [Key]
    [Column("noPlante")]
    public int NoPlante { get; set; }

    [Column("nom")]
    [StringLength(100)]
    [Unicode(false)]
    public string Nom { get; set; } = null!;

    [Column("noWiki")]
    public int? NoWiki { get; set; }

    [Column("alerte")]
    public bool Alerte { get; set; }

    [InverseProperty("NoPlanteNavigation")]
    public virtual ICollection<Donnee> Donnees { get; set; } = new List<Donnee>();

    [ForeignKey("NoWiki")]
    [InverseProperty("Plantes")]
    public virtual Wiki? NoWikiNavigation { get; set; }

    [InverseProperty("NoPlanteNavigation")]
    public virtual ICollection<Utilisateur> Utilisateurs { get; set; } = new List<Utilisateur>();
}
