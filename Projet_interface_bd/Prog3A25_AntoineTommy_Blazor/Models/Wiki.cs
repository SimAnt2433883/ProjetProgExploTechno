using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Prog3A25_AntoineTommy_Blazor.Models;

[Table("Wiki")]
public partial class Wiki
{
    [Key]
    [Column("noWiki")]
    public int NoWiki { get; set; }

    [Column("imagePlante")]
    [StringLength(1000)]
    [Unicode(false)]
    public string? ImagePlante { get; set; }

    [Column("info")]
    [StringLength(1000)]
    [Unicode(false)]
    public string Info { get; set; } = null!;

    [Column("minHumidite", TypeName = "decimal(4, 1)")]
    public decimal MinHumidite { get; set; }

    [Column("maxHumidite", TypeName = "decimal(4, 1)")]
    public decimal MaxHumidite { get; set; }

    [Column("minTemperature", TypeName = "decimal(4, 1)")]
    public decimal MinTemperature { get; set; }

    [Column("maxTemperature", TypeName = "decimal(4, 1)")]
    public decimal MaxTemperature { get; set; }

    [Column("minRayonsUV", TypeName = "decimal(4, 1)")]
    public decimal MinRayonsUv { get; set; }

    [Column("maxRayonsUV", TypeName = "decimal(4, 1)")]
    public decimal MaxRayonsUv { get; set; }

    [InverseProperty("NoWikiNavigation")]
    public virtual ICollection<Plante> Plantes { get; set; } = new List<Plante>();
}
