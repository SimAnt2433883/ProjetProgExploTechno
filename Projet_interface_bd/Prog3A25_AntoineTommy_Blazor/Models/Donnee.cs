using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Prog3A25_AntoineTommy_Blazor.Models;

[Table("Donnee")]
public partial class Donnee
{
    [Key]
    [Column("noDonnee")]
    public int NoDonnee { get; set; }

    [Column("dateHeure", TypeName = "datetime")]
    public DateTime DateHeure { get; set; }

    [Column("temperature", TypeName = "decimal(4, 1)")]
    public decimal Temperature { get; set; }

    [Column("humidite", TypeName = "decimal(4, 1)")]
    public decimal Humidite { get; set; }

    [Column("rayonsUV", TypeName = "decimal(4, 1)")]
    public decimal RayonsUv { get; set; }

    [Column("noPlante")]
    public int NoPlante { get; set; }

    [ForeignKey("NoPlante")]
    [InverseProperty("Donnees")]
    public virtual Plante NoPlanteNavigation { get; set; } = null!;
}
