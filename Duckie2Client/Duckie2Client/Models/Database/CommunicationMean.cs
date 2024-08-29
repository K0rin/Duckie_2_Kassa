using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Duckie2Client.Models.Database;

public class CommunicationMean
{
    [Required] [Key] public Guid Id { get; set; }

    [Column(TypeName = "VARCHAR")]
    [StringLength(512)]
    public string? Email { get; set; }

    [Column(TypeName = "VARCHAR")]
    [StringLength(50)]
    public string? Phone { get; set; }

    // ONE-TO-MANY: Client
    public Guid ClientId { get; set; }
    public Client? Client { get; set; }
}