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

    #region Tables

    // Database entities.

    /// <summary>
    /// List of clients.
    /// </summary>
    public DbSet<Client> Clients { get; set; } = null!;

    /// <summary>
    /// List of communication methods with clients.
    /// </summary>
    public DbSet<CommunicationMean> CommunicationMeans { get; set; } = null!;

    /// <summary>
    /// List of client bonus summa.
    /// </summary>
    public DbSet<ClientBonus> ClientBonuses { get; set; } = null!;

    /// <summary>
    /// List of vehicle price types.
    /// </summary>
    public DbSet<PriceType> PriceTypes { get; set; } = null!;

    /// <summary>
    /// List of vehicles.
    /// </summary>
    public DbSet<Vehicle> Vehicles { get; set; } = null!;

    /// <summary>
    /// List of rates for Operator salaries, pollution levels, sale taxes.
    /// </summary>
    public DbSet<Rate> Rates { get; set; } = null;

    /// <summary>
    /// List of the Application users.
    /// </summary>
    public DbSet<User> Users { get; set; } = null;

    #endregion

    #region Legasy Tables

    public DbSet<LegacyVehicle> LegacyVehicles { get; set; } = null!;
    public DbSet<LegacyCompany> LegacyCompanies { get; set; } = null!;

    #endregion
}