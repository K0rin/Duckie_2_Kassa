using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Duckie2Client.Controls.ManagerConsole;

public partial class ClientCardBatchAddItemControl : UserControl
{
    private string? _clientFirstName;

    public static readonly DirectProperty<ClientCardBatchAddItemControl, string?> ClientFirstNameProperty =
        AvaloniaProperty.RegisterDirect<ClientCardBatchAddItemControl, string?>("ClientFirstName",
            o => o.ClientFirstName, (o, v) => o.ClientFirstName = v);

    private string? _clientLastName;

    public static readonly DirectProperty<ClientCardBatchAddItemControl, string?> ClientLastNameProperty =
        AvaloniaProperty.RegisterDirect<ClientCardBatchAddItemControl, string?>("ClientLastName", o => o.ClientLastName,
            (o, v) => o.ClientLastName = v);

    private string? _clientPhone;

    public static readonly DirectProperty<ClientCardBatchAddItemControl, string?> ClientPhoneProperty =
        AvaloniaProperty.RegisterDirect<ClientCardBatchAddItemControl, string?>("ClientPhone", o => o.ClientPhone,
            (o, v) => o.ClientPhone = v);

    private string? _companyName;

    public static readonly DirectProperty<ClientCardBatchAddItemControl, string?> CompanyNameProperty =
        AvaloniaProperty.RegisterDirect<ClientCardBatchAddItemControl, string?>("CompanyName", o => o.CompanyName,
            (o, v) => o.CompanyName = v);

    private string? _vehicleLicence;

    public static readonly DirectProperty<ClientCardBatchAddItemControl, string?> VehicleLicenceProperty =
        AvaloniaProperty.RegisterDirect<ClientCardBatchAddItemControl, string?>("VehicleLicence", o => o.VehicleLicence,
            (o, v) => o.VehicleLicence = v);

    private string? _vehiclePriceCategory;

    public static readonly DirectProperty<ClientCardBatchAddItemControl, string?> VehiclePriceCategoryProperty =
        AvaloniaProperty.RegisterDirect<ClientCardBatchAddItemControl, string?>("VehiclePriceCategory",
            o => o.VehiclePriceCategory, (o, v) => o.VehiclePriceCategory = v);

    private int? _vehicleDiscount;

    public static readonly DirectProperty<ClientCardBatchAddItemControl, int?> VehicleDiscountProperty =
        AvaloniaProperty.RegisterDirect<ClientCardBatchAddItemControl, int?>("VehicleDiscount", o => o.VehicleDiscount,
            (o, v) => o.VehicleDiscount = v);

    public ClientCardBatchAddItemControl()
    {
        InitializeComponent();
    }

    public string? ClientFirstName
    {
        get => _clientFirstName;
        set => SetAndRaise(ClientFirstNameProperty, ref _clientFirstName, value);
    }

    public string? ClientLastName
    {
        get => _clientLastName;
        set => SetAndRaise(ClientLastNameProperty, ref _clientLastName, value);
    }

    public string? ClientPhone
    {
        get => _clientPhone;
        set => SetAndRaise(ClientPhoneProperty, ref _clientPhone, value);
    }

    public string? CompanyName
    {
        get => _companyName;
        set => SetAndRaise(CompanyNameProperty, ref _companyName, value);
    }

    public string? VehicleLicence
    {
        get => _vehicleLicence;
        set => SetAndRaise(VehicleLicenceProperty, ref _vehicleLicence, value);
    }

    public string? VehiclePriceCategory
    {
        get => _vehiclePriceCategory;
        set => SetAndRaise(VehiclePriceCategoryProperty, ref _vehiclePriceCategory, value);
    }

    public int? VehicleDiscount
    {
        get => _vehicleDiscount;
        set => SetAndRaise(VehicleDiscountProperty, ref _vehicleDiscount, value);
    }
}