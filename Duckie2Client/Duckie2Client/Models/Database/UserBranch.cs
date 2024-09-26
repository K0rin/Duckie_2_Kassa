using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Duckie2Client.Models.Database;

public class UserBranch
{
    [Key] [Column(Order = 0)] public Guid UserId { get; set; }
    public User User { get; set; }

    [Key] [Column(Order = 1)] public Guid BranchId { get; set; }
    public Branch Branch { get; set; }
}