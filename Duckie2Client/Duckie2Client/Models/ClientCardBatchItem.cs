using Duckie2Client.Libs;
using ReactiveUI;

namespace Duckie2Client.Models;

public class ClientCardBatchItem : ReactiveObject
{
    public ClientCardBatchItem(NullOrResult clientFirstName, NullOrResult clientLastName, string? clientPhone,
        NullOrResult companyName, string? vehicleLicence, string? vehiclePriceCategory, int? vehicleDiscount)
    {
        ClientFirstName = clientFirstName;
        ClientLastName = clientLastName;
        ClientPhone = clientPhone;
        CompanyName = companyName;
        VehicleLicence = vehicleLicence;
        VehiclePriceCategory = vehiclePriceCategory;
        VehicleDiscount = vehicleDiscount;
        IsSelected = false;
    }

    public bool IsSelected { get; set; }

    public NullOrResult ClientFirstName { get; set; }
    public NullOrResult ClientLastName { get; set; }
    public string? ClientPhone { get; set; }
    public NullOrResult CompanyName { get; set; }
    public string? VehicleLicence { get; set; }
    public string? VehiclePriceCategory { get; set; }
    public int? VehicleDiscount { get; set; }
}