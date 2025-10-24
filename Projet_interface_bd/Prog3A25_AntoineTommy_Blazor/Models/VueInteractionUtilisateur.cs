using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Prog3A25_AntoineTommy_Blazor.Models;

[Keyless]
public partial class VueInteractionUtilisateur
{
    [Column("noUtilisateur")]
    public int NoUtilisateur { get; set; }

    [Column("nom")]
    [StringLength(100)]
    [Unicode(false)]
    public string Nom { get; set; } = null!;

    [Column("email")]
    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [Column("administrateur")]
    public bool Administrateur { get; set; }

    [Column("noInteraction")]
    public int NoInteraction { get; set; }

    [Column("titre")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Titre { get; set; }

    [Column("texte")]
    [StringLength(1000)]
    [Unicode(false)]
    public string Texte { get; set; } = null!;

    [Column("nbLikes")]
    public int? NbLikes { get; set; }

    [Column("noQuestion")]
    public int NoQuestion { get; set; }
}
