using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace personapi_dotnet.Models.Entities;

[Table("telefono")]
public partial class Telefono
{
    [Key]
    [Column("num")]
    [StringLength(50)]
    [Unicode(false)]
    public string Num { get; set; } = null!;

    [Column("operador")]
    [StringLength(50)]
    [Unicode(false)]
    public string Operador { get; set; } = null!;

    [Column("duenio")]
    public int Duenio { get; set; }

    [ForeignKey("Duenio")]
    [InverseProperty("Telefonos")]
    public virtual Persona DuenioNavigation { get; set; } = null!;
}
