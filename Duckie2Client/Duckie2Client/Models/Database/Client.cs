using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Duckie2Client.Models.Database;

/// <summary>
/// Represents an entity of type “Client.” Contains information about the client.
/// </summary>
public class Client
{
    /// <summary>
    /// Client Identifier.
    /// </summary>
    [Required]
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// Client's name.
    /// </summary>
    [Column(TypeName = "NVARCHAR")]
    [StringLength(50)]
    public string? FirstName { get; set; }

    /// <summary>
    /// Client's last name.
    /// </summary>
    [Column(TypeName = "NVARCHAR")]
    [StringLength(50)]
    public string? LastName { get; set; }

    /// <summary>
    /// Notes about the client. This field is filled in by a company employee and is used to provide additional
    /// information about the client.
    /// </summary>
    [Column(TypeName = "NVARCHAR")]
    [StringLength(2000)]
    public string? Notes { get; set; }

    /// <summary>
    /// The date and time of the client's registration.
    /// </summary>
    public DateTime FirstRegistration { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Ways to communicate with the customer.
    /// </summary>
    public List<CommunicationMean>? CommunicationMeans { get; set; } = [];

    /// <summary>
    /// The number of bonuses earned by the client and their validity date.
    /// </summary>
    public required ClientBonus Bonus { get; set; }

    /// <summary>
    /// A list of vehicles registered to the client.
    /// </summary>
    public List<Vehicle>? Vehicles { get; set; } = [];
}