using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Duckie2Client.Models.Database;

/// <summary>
/// Represents data about the trade unit localization.
/// </summary>
public class TradeUnitLocalization
{
    /// <summary>
    /// Record Identifier.
    /// </summary>
    [Required]
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// The trade unit locale with two letters.
    /// </summary>
    [Required]
    [Column(TypeName = "VARCHAR")]
    [StringLength(2)]
    public required string Locale { get; set; }

    /// <summary>
    /// The translation of the trade unit name into a denoted language.
    /// </summary>
    [Required]
    [Column(TypeName = "NVARCHAR")]
    [StringLength(100)]
    public required string Value { get; set; }
}