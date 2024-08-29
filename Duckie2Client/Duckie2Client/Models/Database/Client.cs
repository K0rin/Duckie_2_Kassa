using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Duckie2Client.Models.Database;

public class Client
{
    [Required] [Key] public Guid Id { get; set; }

    [Column(TypeName = "VARCHAR")]
    [StringLength(100)]
    public string? FirstName { get; set; }

    [Column(TypeName = "VARCHAR")]
    [StringLength(100)]
    public string? LastName { get; set; }

    [Column(TypeName = "VARCHAR")]
    [StringLength(2000)]
    public string? Notes { get; set; }

    public DateTime FirstRegistration { get; set; } = DateTime.UtcNow;

    public List<CommunicationMean>? CommunicationMeans { get; set; } = new();

    public required ClientBonus Bonus { get; set; }
    // public ICollection<Vehicle>? Vehicles { get; set; } = [];
}