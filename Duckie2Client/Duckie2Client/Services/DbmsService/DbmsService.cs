using Duckie2Client.Libs.DatabaseManager;
using Duckie2Client.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Duckie2Client.Services.DbmsService;

public class DbmsService : BaseDbmsService
{
    // todo: the data goes from the settings.
    protected override string ServiceName => "MSSQL$SQLEXPRESS";

    // todo: the data goes from the settings.
    protected override string ServerName => "DESKTOP-H1O55SG\\SQLEXPRESS";

    // todo: the data goes from the settings.
    protected override string DatabaseName => "CarWash";

    // -- tables
    public DbSet<Client> Clients { get; set; } = null!;
    public DbSet<CommunicationMean> CommunicationMeans { get; set; } = null!;

    public DbSet<ClientBonus> ClientBonuses { get; set; } = null!;
    // public DbSet<PriceType> PriceTypes { get; set; } = null!;
    // public DbSet<Vehicle> Vehicles { get; set; } = null!;
}