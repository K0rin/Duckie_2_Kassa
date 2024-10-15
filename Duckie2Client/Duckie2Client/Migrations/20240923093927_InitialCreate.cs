using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Duckie2Client.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Транспорт",
                columns: table => new
                {
                    Номер = table.Column<string>(type: "NCHAR(10)", maxLength: 10, nullable: false),
                    Категория = table.Column<string>(type: "NCHAR(1)", maxLength: 1, nullable: false),
                    Скидка = table.Column<decimal>(type: "DECIMAL(2,2)", nullable: false),
                    КодФирмы = table.Column<int>(type: "int", nullable: false),
                    new_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Транспорт", x => x.Номер);
                });

            migrationBuilder.CreateTable(
                name: "Фирмы",
                columns: table => new
                {
                    Код = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Наименование = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    Адрес = table.Column<string>(type: "NCHAR(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Фирмы", x => x.Код);
                });

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
                    Value = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.Id);
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
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Login = table.Column<string>(type: "VARCHAR", nullable: false),
                    Password = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    FirstName = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false),
                    IsStaff = table.Column<bool>(type: "bit", nullable: false),
                    RegistrationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    SalaryRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Rates_SalaryRateId",
                        column: x => x.SalaryRateId,
                        principalTable: "Rates",
                        principalColumn: "Id");
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
                name: "CommunicationMeans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(512)", maxLength: 512, nullable: true),
                    Phone = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: true),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicationMeans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommunicationMeans_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommunicationMeans_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

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
                name: "IX_Users_BranchId",
                table: "Users",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SalaryRateId",
                table: "Users",
                column: "SalaryRateId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Транспорт");

            migrationBuilder.DropTable(
                name: "Фирмы");

            migrationBuilder.DropTable(
                name: "ClientBonuses");

            migrationBuilder.DropTable(
                name: "ClientVehicle");

            migrationBuilder.DropTable(
                name: "CommunicationMeans");

            migrationBuilder.DropTable(
                name: "PollutionLevels");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "PriceTypes");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Rates");
        }
    }
}
