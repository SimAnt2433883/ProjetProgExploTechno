using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Prog3A25_AntoineTommy_Blazor.Models;

[Keyless]
public partial class VueReponseUtilisateur
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

    [Column("noReponse")]
    public int NoReponse { get; set; }

    [Column("reponse")]
    [StringLength(1000)]
    [Unicode(false)]
    public string Reponse { get; set; } = null!;

    [Column("nbLikes")]
    public int? NbLikes { get; set; }

    [Column("noQuestion")]
    public int NoQuestion { get; set; }
}
