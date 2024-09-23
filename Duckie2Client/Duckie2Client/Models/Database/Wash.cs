using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Duckie2Client.Models.Database;

/// <summary>
/// <para>
/// Represents data about the customer order.
/// </para>
/// <para>
/// The order can include services or goods and both.
/// </para>
/// </summary>
public class Wash
{
    /// <summary>
    /// The wash identifier.
    /// </summary>
    [Required]
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// The date and time of the order fulfillment.
    /// </summary>
    public required DateTime DateTime { get; set; }

    /// <summary>
    /// Vehicle Registration Number.
    /// </summary>
    public Vehicle? Vehicle { get; set; }

    /// <summary>
    /// The client who ordered the services.
    /// </summary>
    public Client? Client { get; set; }

    /// <summary>
    /// The firm ordering the services.
    /// </summary>
    public Company? Company { get; set; }

    /// <summary>
    /// Type of payment: 0 - cash payment, 1 - non-cash payment, 2 - deferred payment.
    /// </summary>
    public required int PaymentType { get; set; }

    /// <summary>
    /// The amount of bonuses used to pay for the order.
    /// </summary>
    [Column(TypeName = "DECIMAL(10,2)")]
    public decimal? BonusesUses { get; set; } = 0;

    /// <summary>
    /// The branch of the company where the order was fulfilled.
    /// </summary>
    public required Branch Branch { get; set; }

    /// <summary>
    /// The level of vehicle pollution.
    /// </summary>
    public PollutionLevel? PollutionLevel { get; set; }

    /// <summary>
    /// Order status: 0 - completed. 1 - postponed.
    /// </summary>
    public required int Status { get; set; }

    /// <summary>
    /// Receipt Number.
    /// </summary>
    public string? ReceiptNumber { get; set; }

    /// <summary>
    /// Services or goods ordered by the customer.
    /// </summary>
    public required List<TradeUnit> TradeUnits { get; set; }
}