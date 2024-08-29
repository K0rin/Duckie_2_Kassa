using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Duckie2Client.Models.Database;

[Index(nameof(Licence), IsUnique = true)]
public class Vehicle
{
    [Required] [Key] public Guid Id { get; set; }

    [Required]
    [Column(TypeName = "VARCHAR")]
    [StringLength(50)]
    public required string Licence { get; set; }

    [Required] public required PriceType PriceType { get; set; }
}