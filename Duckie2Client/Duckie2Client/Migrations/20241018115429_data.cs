using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Duckie2Client.Migrations
{
    /// <inheritdoc />
    public partial class data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "NVARCHAR(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: true),
                    Notes = table.Column<string>(type: "NVARCHAR(2000)", maxLength: 2000, nullable: true),
                    FirstRegistration = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "NVARCHAR(200)", maxLength: 200, nullable: false),
                    RegistrationNumber = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PollutionLevels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollutionLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TradeUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsGood = table.Column<bool>(type: "bit", nullable: false),
                    ProcessTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    IsIgnoreDiscounts = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Login = table.Column<string>(type: "VARCHAR(25)", maxLength: 25, nullable: false),
                    Password = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    FirstName = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false),
                    IsStaff = table.Column<bool>(type: "bit", nullable: false),
                    RegistrationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientBonuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Summa = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientBonuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientBonuses_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Licence = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    PriceTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Vehicles_PriceTypes_PriceTypeId",
                        column: x => x.PriceTypeId,
                        principalTable: "PriceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PollutionLevelRate",
                columns: table => new
                {
                    PollutionLevelsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RatesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollutionLevelRate", x => new { x.PollutionLevelsId, x.RatesId });
                    table.ForeignKey(
                        name: "FK_PollutionLevelRate_PollutionLevels_PollutionLevelsId",
                        column: x => x.PollutionLevelsId,
                        principalTable: "PollutionLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PollutionLevelRate_Rates_RatesId",
                        column: x => x.RatesId,
                        principalTable: "Rates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriceTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prices_PriceTypes_PriceTypeId",
                        column: x => x.PriceTypeId,
                        principalTable: "PriceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prices_Rates_ValueId",
                        column: x => x.ValueId,
                        principalTable: "Rates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TradeUnitLocalizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Locale = table.Column<string>(type: "VARCHAR(2)", maxLength: 2, nullable: false),
                    Value = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    TradeUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeUnitLocalizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TradeUnitLocalizations_TradeUnits_TradeUnitId",
                        column: x => x.TradeUnitId,
                        principalTable: "TradeUnits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BranchUser",
                columns: table => new
                {
                    BranchesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchUser", x => new { x.BranchesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_BranchUser_Branches_BranchesId",
                        column: x => x.BranchesId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BranchUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommunicationMeans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(512)", maxLength: 512, nullable: true),
                    Phone = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: true),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicationMeans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommunicationMeans_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CommunicationMeans_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RateUser",
                columns: table => new
                {
                    SalaryRatesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateUser", x => new { x.SalaryRatesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RateUser_Rates_SalaryRatesId",
                        column: x => x.SalaryRatesId,
                        principalTable: "Rates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RateUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientVehicle",
                columns: table => new
                {
                    ClientsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehiclesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientVehicle", x => new { x.ClientsId, x.VehiclesId });
                    table.ForeignKey(
                        name: "FK_ClientVehicle_Clients_ClientsId",
                        column: x => x.ClientsId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientVehicle_Vehicles_VehiclesId",
                        column: x => x.VehiclesId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Washes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentType = table.Column<int>(type: "int", nullable: false),
                    BonusesUses = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PollutionLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ReceiptNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Washes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Washes_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Washes_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Washes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Washes_PollutionLevels_PollutionLevelId",
                        column: x => x.PollutionLevelId,
                        principalTable: "PollutionLevels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Washes_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PriceTradeUnit",
                columns: table => new
                {
                    PricesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TradeUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceTradeUnit", x => new { x.PricesId, x.TradeUnitId });
                    table.ForeignKey(
                        name: "FK_PriceTradeUnit_Prices_PricesId",
                        column: x => x.PricesId,
                        principalTable: "Prices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PriceTradeUnit_TradeUnits_TradeUnitId",
                        column: x => x.TradeUnitId,
                        principalTable: "TradeUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TradeUnitWash",
                columns: table => new
                {
                    TradeUnitsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WashesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeUnitWash", x => new { x.TradeUnitsId, x.WashesId });
                    table.ForeignKey(
                        name: "FK_TradeUnitWash_TradeUnits_TradeUnitsId",
                        column: x => x.TradeUnitsId,
                        principalTable: "TradeUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TradeUnitWash_Washes_WashesId",
                        column: x => x.WashesId,
                        principalTable: "Washes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BranchUser_UsersId",
                table: "BranchUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientBonuses_ClientId",
                table: "ClientBonuses",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientVehicle_VehiclesId",
                table: "ClientVehicle",
                column: "VehiclesId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationMeans_ClientId",
                table: "CommunicationMeans",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationMeans_UserId",
                table: "CommunicationMeans",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PollutionLevelRate_RatesId",
                table: "PollutionLevelRate",
                column: "RatesId");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_PriceTypeId",
                table: "Prices",
                column: "PriceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_ValueId",
                table: "Prices",
                column: "ValueId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceTradeUnit_TradeUnitId",
                table: "PriceTradeUnit",
                column: "TradeUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_RateUser_UsersId",
                table: "RateUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeUnitLocalizations_TradeUnitId",
                table: "TradeUnitLocalizations",
                column: "TradeUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeUnitWash_WashesId",
                table: "TradeUnitWash",
                column: "WashesId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CompanyId",
                table: "Vehicles",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Licence",
                table: "Vehicles",
                column: "Licence",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_PriceTypeId",
                table: "Vehicles",
                column: "PriceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Washes_BranchId",
                table: "Washes",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Washes_ClientId",
                table: "Washes",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Washes_CompanyId",
                table: "Washes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Washes_PollutionLevelId",
                table: "Washes",
                column: "PollutionLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Washes_VehicleId",
                table: "Washes",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BranchUser");

            migrationBuilder.DropTable(
                name: "ClientBonuses");

            migrationBuilder.DropTable(
                name: "ClientVehicle");

            migrationBuilder.DropTable(
                name: "CommunicationMeans");

            migrationBuilder.DropTable(
                name: "PollutionLevelRate");

            migrationBuilder.DropTable(
                name: "PriceTradeUnit");

            migrationBuilder.DropTable(
                name: "RateUser");

            migrationBuilder.DropTable(
                name: "TradeUnitLocalizations");

            migrationBuilder.DropTable(
                name: "TradeUnitWash");

            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "TradeUnits");

            migrationBuilder.DropTable(
                name: "Washes");

            migrationBuilder.DropTable(
                name: "Rates");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "PollutionLevels");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "PriceTypes");
        }
    }
}
