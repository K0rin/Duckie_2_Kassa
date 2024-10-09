using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Duckie2Client.Models.Database;

/// <summary>
/// Represents data about the trade unit (a service or a good) offered by the company.
/// </summary>
public class TradeUnit
{
    /// <summary>
    /// The trade unit identifier.
    /// </summary>
    [Required]
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// Indicates whether the trade unit is a good or not.
    /// </summary>
    public required bool IsGood { get; set; }

    /// <summary>
    /// Shows the execution time of the trade unit with type of a service.
    /// </summary>
    public TimeOnly? ProcessTime { get; set; }

    /// <summary>
    /// Indicator: whether the trade unit ignores all discount campaigns.
    /// </summary>
    public bool IsIgnoreDiscounts { get; set; } = false;


    /// <summary>
    /// The list of prices for the trade unit.
    /// </summary>
    public required List<Price> Prices { get; set; }

    /// <summary>
    /// The list of names for the trade unit translated into denoted languages.
    /// </summary>
    public required List<TradeUnitLocalization> Names { get; set; }

    public List<Wash> Washes { get; set; }
}