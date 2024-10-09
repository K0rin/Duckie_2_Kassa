using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Duckie2Client.Models.Database;

/// <summary>
/// <para>
/// Represents data about the price of the trade unit.
/// </para>
/// <para>
/// Each trade unit has its own price. Each price has its own validity period.
/// </para>
/// </summary>
public class Price
{
    /// <summary>
    /// The price identifier.
    /// </summary>
    [Required]
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// The trade unit that the price belongs to.
    /// </summary>
    [Required]
    public required List<TradeUnit> TradeUnit { get; set; }

    /// <summary>
    /// The value of the price.
    /// </summary>
    [Required]
    public required Rate Value { get; set; }

    /// <summary>
    /// The price type of the value price.
    /// </summary>
    [Required]
    public required PriceType PriceType { get; set; }
}