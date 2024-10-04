using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Duckie2Client.Models.Database;

/// <summary>
/// Represents data about the customer company.
/// </summary>
public class Company
{
    /// <summary>
    /// The customer company identifier.
    /// </summary>
    [Required]
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// The name of the customer company.
    /// </summary>
    [Column(TypeName = "NVARCHAR")]
    [StringLength(100)]
    public required string Name { get; set; }

    /// <summary>
    /// The address of the customer company.
    /// </summary>
    [Required]
    [Column(TypeName = "NVARCHAR")]
    [StringLength(200)]
    public required string Address { get; set; }

    /// <summary>
    /// The registration number of the customer company.
    /// </summary>
    [Column(TypeName = "VARCHAR")]
    [StringLength(50)]
    public string? RegistrationNumber { get; set; }

    /// <summary>
    /// Indicator: whether the customer company marked as "deleted" (false) or not (true).
    /// </summary>
    [Required]
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// The list of vehicles belonging to the customer company.
    /// </summary>
    [Required]
    public List<Vehicle>? Vehicles { get; set; }
}