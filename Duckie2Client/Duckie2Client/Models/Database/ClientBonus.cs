using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Duckie2Client.Models.Database;

public class ClientBonus
{
    [Required] [Key] public Guid Id { get; set; }
    [Required] public Guid ClientId { get; set; }
    [Column(TypeName = "DECIMAL(10,2)")] public decimal Summa { get; set; }
    [Required] public DateTime EndDateTime { get; set; }
}