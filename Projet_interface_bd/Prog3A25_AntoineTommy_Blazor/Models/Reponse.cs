using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Prog3A25_AntoineTommy_Blazor.Models;

[Table("Reponse")]
public partial class Reponse
{
    [Key]
    [Column("noReponse")]
    public int NoReponse { get; set; }

    [Column("reponse")]
    [StringLength(1000)]
    [Unicode(false)]
    public string Reponse1 { get; set; } = null!;

    [Column("noUtilisateur")]
    public int? NoUtilisateur { get; set; }

    [Column("noQuestion")]
    public int NoQuestion { get; set; }

    [ForeignKey("NoQuestion")]
    [InverseProperty("Reponses")]
    public virtual Question NoQuestionNavigation { get; set; } = null!;

    [ForeignKey("NoUtilisateur")]
    [InverseProperty("Reponses")]
    public virtual Utilisateur? NoUtilisateurNavigation { get; set; }

    [ForeignKey("NoReponse")]
    [InverseProperty("NoReponses")]
    public virtual ICollection<Utilisateur> NoUtilisateurs { get; set; } = new List<Utilisateur>();
}
