using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Duckie2Client.Models.Database;

/// <summary>
/// Represents the entity of the “Vehicle” type.
/// Contains information about vehicle license plate number, list of vehicle owners.
/// </summary>
[Index(nameof(Licence), IsUnique = true)]
public class Vehicle
{
    /// <summary>
    /// Identifier of the record.
    /// </summary>
    [Required]
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// License plate number.
    /// </summary>
    [Required]
    [Column(TypeName = "VARCHAR")]
    [StringLength(50)]
    public required string Licence { get; set; }

    /// <summary>
    /// Price type of the vehicle.
    /// </summary>
    [Required]
    public required PriceType PriceType { get; set; }

    /// <summary>
    /// A list of customers registered as owners of the vehicle.
    /// </summary>
    public List<Client>? Clients { get; set; } = [];
}