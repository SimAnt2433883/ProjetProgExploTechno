using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Prog3A25_AntoineTommy_Blazor.Models;

[Table("Question")]
public partial class Question
{
    [Key]
    [Column("noQuestion")]
    public int NoQuestion { get; set; }

    [Column("titre")]
    [StringLength(50)]
    [Unicode(false)]
    public string Titre { get; set; } = null!;

    [Column("question")]
    [StringLength(1000)]
    [Unicode(false)]
    public string Question1 { get; set; } = null!;

    [Column("noUtilisateur")]
    public int? NoUtilisateur { get; set; }

    [ForeignKey("NoUtilisateur")]
    [InverseProperty("Questions")]
    public virtual Utilisateur? NoUtilisateurNavigation { get; set; }

    [InverseProperty("NoQuestionNavigation")]
    public virtual ICollection<Reponse> Reponses { get; set; } = new List<Reponse>();

    [ForeignKey("NoQuestion")]
    [InverseProperty("NoQuestions")]
    public virtual ICollection<Utilisateur> NoUtilisateurs { get; set; } = new List<Utilisateur>();
}
