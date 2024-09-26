using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Duckie2Client.Models.Database;

/// <summary>
/// Represents information about the branch of the company.
/// </summary>
public class Branch
{
    /// <summary>
    /// The branch identifier.
    /// </summary>
    [Required]
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// The name of the branch.
    /// </summary>
    [Required]
    [Column(TypeName = "NVARCHAR")]
    [StringLength(100)]
    public required string Name { get; set; }

    /// <summary>
    /// The address of the branch.
    /// </summary>
    [Required]
    [Column(TypeName = "NVARCHAR")]
    [StringLength(200)]
    public required string Address { get; set; }

    /// <summary>
    /// The list of users belonging to a specific branch.
    /// </summary>
    public ICollection<User> Users { get; set; }
}