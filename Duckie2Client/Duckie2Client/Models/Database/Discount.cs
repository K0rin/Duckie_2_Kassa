using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Duckie2Client.Models.Database;

/// <summary>
/// Represents an entity of type “Discount.”
/// Contains information about types of discounts, their values and validity period.
/// </summary>
[Index(nameof(Name), IsUnique = true)]
public class Discount
{
    /// <summary>
    /// The discount identifier.
    /// </summary>
    [Required]
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// Name of the discount company.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Value of the discount percent.
    /// </summary>
    public required uint Value { get; set; } = 0;

    /// <summary>
    /// Discount company start date.
    /// </summary>
    public DateTime StartDateTime { get; set; }

    /// <summary>
    /// Discount company end date.
    /// </summary>
    public DateTime EndDateTime { get; set; }

    /// <summary>
    /// Indicates whether the discount company has infinite term of validity or not.
    /// </summary>
    public bool IsPermanent { get; set; } = false;
}