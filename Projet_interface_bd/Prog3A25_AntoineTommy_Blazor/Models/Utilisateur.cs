using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Prog3A25_AntoineTommy_Blazor.Models;

[Table("Utilisateur")]
[Index("Email", Name = "UQ_Utilisateur_Email", IsUnique = true)]
public partial class Utilisateur
{
    [Key]
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

    [Column("noAdresse")]
    public int? NoAdresse { get; set; }

    [Column("noPlante")]
    public int? NoPlante { get; set; }

    [Column("administrateur")]
    public bool Administrateur { get; set; }

    [Column("sel")]
    public Guid? Sel { get; set; }

    [Column("motPasseHash")]
    [MaxLength(64)]
    public byte[]? MotPasseHash { get; set; }

    [ForeignKey("NoAdresse")]
    [InverseProperty("Utilisateurs")]
    public virtual Adresse? NoAdresseNavigation { get; set; }

    [ForeignKey("NoPlante")]
    [InverseProperty("Utilisateurs")]
    public virtual Plante? NoPlanteNavigation { get; set; }

    [InverseProperty("NoUtilisateurNavigation")]
    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    [InverseProperty("NoUtilisateurNavigation")]
    public virtual ICollection<Reponse> Reponses { get; set; } = new List<Reponse>();

    [ForeignKey("NoUtilisateur")]
    [InverseProperty("NoUtilisateurs")]
    public virtual ICollection<Question> NoQuestions { get; set; } = new List<Question>();

    [ForeignKey("NoUtilisateur")]
    [InverseProperty("NoUtilisateurs")]
    public virtual ICollection<Reponse> NoReponses { get; set; } = new List<Reponse>();
}
