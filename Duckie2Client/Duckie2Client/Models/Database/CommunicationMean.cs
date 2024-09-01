using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Duckie2Client.Models.Database;

/// <summary>
/// Represents an entity of the “Communication means” type.
/// Contains information about the means of communication with the client (phone, e-mail).
/// </summary>
public class CommunicationMean
{
    /// <summary>
    /// Record identifier.
    /// </summary>
    [Required]
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// Email address of the client.
    /// </summary>
    [Column(TypeName = "VARCHAR")]
    [StringLength(512)]
    public string? Email { get; set; }

    /// <summary>
    /// Phone number of the client.
    /// </summary>
    [Column(TypeName = "VARCHAR")]
    [StringLength(50)]
    public string? Phone { get; set; }

    // ONE-TO-MANY: Client. Each client can have many communication means (e.g., many phone numbers).

    /// <summary>
    /// The identifier of the customer to which the communication method is bound.
    /// </summary>
    public Guid ClientId { get; set; }

    /// <summary>
    /// A reference to the “Client” object to access the client through its communication means.
    /// </summary>
    public Client? Client { get; set; }
}