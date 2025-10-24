using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Prog3A25_AntoineTommy_Blazor.Models;

[Table("Adresse")]
public partial class Adresse
{
    [Key]
    [Column("noAdresse")]
    public int NoAdresse { get; set; }

    [Column("nomRue")]
    [StringLength(100)]
    [Unicode(false)]
    public string NomRue { get; set; } = null!;

    [Column("noCivique")]
    public short NoCivique { get; set; }

    [Column("codePostal")]
    [StringLength(6)]
    [Unicode(false)]
    public string? CodePostal { get; set; }

    [Column("ville")]
    [StringLength(100)]
    [Unicode(false)]
    public string Ville { get; set; } = null!;

    [InverseProperty("NoAdresseNavigation")]
    public virtual ICollection<Utilisateur> Utilisateurs { get; set; } = new List<Utilisateur>();
}
