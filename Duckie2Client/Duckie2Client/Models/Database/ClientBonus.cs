using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Duckie2Client.Models.Database;

/// <summary>
/// Represents an entity of the “Client's bonuses” type.
/// Contains information about the size of the bonus amount accumulated by the client and the date of resetting
/// this amount.
/// </summary>
public class ClientBonus
{
    /// <summary>
    /// Record Identifier.
    /// </summary>
    [Required]
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// Client identifier.
    /// </summary>
    [Required]
    public Guid ClientId { get; set; }

    /// <summary>
    /// The value of the sum of accumulated bonuses.
    /// </summary>
    [Column(TypeName = "DECIMAL(10,2)")]
    public decimal Summa { get; set; }

    /// <summary>
    /// Term (date and time) until which the client can use the number of accumulated bonuses. When the deadline
    /// is reached, the amount is reset.
    /// </summary>
    [Required]
    public DateTime EndDateTime { get; set; }
}