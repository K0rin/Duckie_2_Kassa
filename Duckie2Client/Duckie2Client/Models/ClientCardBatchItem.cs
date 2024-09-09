using ReactiveUI;

namespace Duckie2Client.Models;

public class ClientCardBatchItem : ReactiveObject
{
    public ClientCardBatchItem(string? clientFirstName, string? clientLastName, string? clientPhone,
        string? companyName, string? vehicleLicence, string? vehiclePriceCategory, int? vehicleDiscount)
    {
        ClientFirstName = clientFirstName;
        ClientLastName = clientLastName;
        ClientPhone = clientPhone;
        CompanyName = companyName;
        VehicleLicence = vehicleLicence;
        VehiclePriceCategory = vehiclePriceCategory;
        VehicleDiscount = vehicleDiscount;
    }

    public string? ClientFirstName { get; set; }
    public string? ClientLastName { get; set; }
    public string? ClientPhone { get; set; }
    public string? CompanyName { get; set; }
    public string? VehicleLicence { get; set; }
    public string? VehiclePriceCategory { get; set; }
    public int? VehicleDiscount { get; set; }
}