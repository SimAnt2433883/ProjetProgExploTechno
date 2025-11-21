using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Prog3A25_AntoineTommy_Blazor.Models;

[Keyless]
public partial class VueInfoUtilisateur
{
    [Column("noUtilisateur")]
    public int NoUtilisateur { get; set; }

    [Column("noPlante")]
    public int? NoPlante { get; set; }

    [Column("utilisateurNom")]
    [StringLength(100)]
    [Unicode(false)]
    public string UtilisateurNom { get; set; } = null!;

    [Column("planteNom")]
    [StringLength(100)]
    [Unicode(false)]
    public string? PlanteNom { get; set; }

    [Column("administrateur")]
    public bool Administrateur { get; set; }

    [Column("nbQuestions")]
    public int? NbQuestions { get; set; }
}
