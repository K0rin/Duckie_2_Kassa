namespace DataFaker;

public class FakeClient
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Notes { get; set; }
    public DateTime RegistrationDateTime { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public FakeBonus Bonus { get; set; }
    public FakeVehicle Vehicle { get; set; }
}

public class FakeBonus
{
    public decimal Summa { get; set; }
    public DateTime BonusEndDateTime { get; set; }
}

public class FakeVehicle
{
    public string? Licence { get; set; }
}