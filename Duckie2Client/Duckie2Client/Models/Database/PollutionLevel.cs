using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Duckie2Client.Models.Database;

/// <summary>
/// Represent data about the pollution level of a vehicle.
/// </summary>
public class PollutionLevel
{
    /// <summary>
    /// The pollution level identifier.
    /// </summary>
    [Required]
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// The name of the pollution level.
    /// </summary>
    [Required]
    [Column(TypeName = "NVARCHAR")]
    [StringLength(25)]
    public required string Name { get; set; }

    /// <summary>
    /// A percentage added to the price for the level of pollution. 
    /// </summary>
    public required List<Rate>? Rates { get; set; } = [];
}