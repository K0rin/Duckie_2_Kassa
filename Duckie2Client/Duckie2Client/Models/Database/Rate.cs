using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Duckie2Client.Models.Database;

/// <summary>
/// <para>
/// Represents an entity of type “Rate.”
/// </para>
/// <para>
/// Contains information on wage rates, pollution levels, sales tax, prices.
/// Also includes information on the rate validity period.
/// </para>
/// </summary>
public class Rate
{
    /// <summary>
    /// Rate identifier.
    /// </summary>
    [Required]
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// <para>
    /// Value of the rate.
    /// </para>
    /// </summary>
    [Required]
    [Column(TypeName = "DECIMAL(10,2)")]
    public decimal Value { get; set; }

    /// <summary>
    /// Rate commencement date.
    /// </summary>
    [Required]
    public DateOnly StartDate { get; set; }

    /// <summary>
    /// Rate end date.
    /// </summary>
    [Required]
    public DateOnly EndDate { get; set; }

    public List<User>? Users { get; set; } = [];
    public List<PollutionLevel>? PollutionLevels { get; set; } = [];
}