using System;

namespace Duckie2Client.Models;

public record class LoginUserRecord(Guid Id, string Initials)
{
    public bool IsBusy { get; set; } = false;
    public string OrderID { get; set; } = "";
}