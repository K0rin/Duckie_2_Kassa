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

    public DateTime DateTime { get; set; }
    public Vehicle Vehicle { get; set; }
    public Client Client { get; set; }
    public Company? Company { get; set; }
    public int PaymentType { get; set; }
    [Column(TypeName = "DECIMAL(10,2)")] public decimal BonusesUses { get; set; } = 0;
    public Branch Branch { get; set; }
    public PollutionLevel PollutionLevel { get; set; }
    public int Status { get; set; }
    public string ReceiptNumber { get; set; }
    public List<TradeUnit> TradeUnits { get; set; }
}