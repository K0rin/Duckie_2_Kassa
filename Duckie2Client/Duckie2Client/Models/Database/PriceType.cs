using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Duckie2Client.Models.Database;

/// <summary>
/// <para>Represents an entity such as “Price Category.”</para>
/// <para>Each vehicle must have its own price category. Categories are distinguished by their prices.</para>
/// </summary>
public class PriceType
{
    /// <summary>
    /// Price Category ID.
    /// </summary>
    [Required]
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// A Price Category name.
    /// </summary>
    [Column(TypeName = "VARCHAR")]
    [StringLength(50)]
    public string Name { get; set; }
}