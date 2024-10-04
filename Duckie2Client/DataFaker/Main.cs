using Bogus;
using static Bogus.DataSets.Name;

namespace DataFaker;

public class Main
{
    public FakeClient GetClient()
    {
        var fkr = new Faker();
        var gender = fkr.Random.Number(1);
        var bonus = new FakeBonus
        {
            Summa = Math.Round(new Faker().Random.Decimal(1.0m, 100.0m), 2),
            BonusEndDateTime = new Faker().Date.Future()
        };
        var vehicle = new FakeVehicle
        {
            Licence = GetVehicleLicence()
        };

        var firstName = fkr.Name.FirstName(gender as Gender?);
        var lastName = fkr.Name.LastName(gender as Gender?);

        var fakeClient = new Faker<FakeClient>()
            .RuleFor(c => c.FirstName, firstName)
            .RuleFor(c => c.LastName, lastName)
            .RuleFor(c => c.Notes, f => f.Lorem.Sentence(9))
            .RuleFor(c => c.RegistrationDateTime, f => f.Date.Recent(90))
            .RuleFor(c => c.Email, f => f.Internet.Email(firstName, lastName))
            .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber("+###########"))
            .RuleFor(c => c.Bonus, bonus)
            .RuleFor(c => c.Vehicle, vehicle);

        return fakeClient.Generate();
    }

    public string GetVehicleLicence()
    {
        var faker = new Faker();
        var numbers = faker.Random.Number(100, 999);
        var letters = faker.Random.String2(3, "ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        var carNumber = $"{numbers}{letters}";
        return carNumber;
    }

    public FakeCompany GetCompany()
    {
        var company = new Faker<FakeCompany>();
        company
            .RuleFor(c => c.Name, f => f.Company.CompanyName())
            .RuleFor(c => c.Address, f => f.Address.FullAddress());

        var result = company.Generate();
        result.RegistrationNumber = $"REGNUM-{result.Name}";
        return result;
    }
}