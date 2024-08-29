using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Duckie2Client.Models.Database;

public class PriceType
{
    [Required] [Key] public Guid Id { get; set; }

    [Column(TypeName = "VARCHAR")]
    [StringLength(50)]
    public string Name { get; set; }
}