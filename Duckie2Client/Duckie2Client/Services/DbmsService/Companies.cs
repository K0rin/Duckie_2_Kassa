using System;
using System.Collections.Generic;
using System.Linq;
using Duckie2Client.Enums.Flags;
using Duckie2Client.Models;
using Duckie2Client.Models.Database;
using Duckie2Client.Services.DbmsService.Records;
using Duckie2Client.Services.DbmsService.Records.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ReactiveUI;

namespace Duckie2Client.Services.DbmsService;

public class Companies : CrudOperationsBase
{
    public override DataModelOperationResult Create<T>(RecordBuilderBase<T> builder)
    {
        if (builder.Build() is not CompanyRecord builtCompany) return DataModelOperationResult.RecordNotFound;

        using var db = new DbmsService();

        List<Vehicle> newCompanyVehicles;
        try
        {
            newCompanyVehicles = CreateVehicleList(builtCompany, db);
        }
        catch (Exception e) when (e is ArgumentNullException or InvalidOperationException)
        {
            return DataModelOperationResult.RecordNotFound;
        }

        var newCompany = CreateCompany(builtCompany, newCompanyVehicles);
        return AddAndSave(db.Companies, db, newCompany);
    }

    private List<Vehicle> CreateVehicleList(CompanyRecord? record, DbmsService db)
    {
        var newCompanyVehicles = new List<Vehicle>();

        // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
        foreach (var vehicle in record?.Vehicles!)
        {
            var vehicleRecord = vehicle;
            var existingPriceType = db.PriceTypes.First(pt => pt.Id.Equals(vehicleRecord.PriceType.Id));
            var newVehicle = CreateVehicle(existingPriceType, vehicleRecord);
            newCompanyVehicles.Add(newVehicle);
        }

        return newCompanyVehicles;
    }

    private static Vehicle CreateVehicle(PriceType existingPriceType, VehicleRecord vehicle)
    {
        var newVehicle = new Vehicle
        {
            Licence = null!,
            PriceType = existingPriceType
        };
        var vehicleRecord = vehicle;
        PropertySetter.SetProperties<VehicleRecord, Vehicle>(ref vehicleRecord, ref newVehicle);
        return newVehicle;
    }

    private static Company CreateCompany(CompanyRecord? builtCompany, List<Vehicle> newCompanyVehicles)
    {
        var newCompany = new Company
        {
            Name = string.Empty,
            Address = string.Empty,
            Vehicles = newCompanyVehicles
        };
        PropertySetter.SetProperties<CompanyRecord, Company>(ref builtCompany!, ref newCompany);
        return newCompany;
    }

    public override object Read<TDataModel>(RecordReadFlags readFlags)
    {
        object result;

        using (var db = new DbmsService())
        {
            result = db.Companies.ToList();
        }

        return (List<TDataModel>)result;
    }

    public static CompaniesRecord? FindCompanyConnectedWithVehicle(Guid VehicleId)
    {
        CompaniesRecord returnResult = null;

        using var db = new DbmsService();

        var foundCompanies = db.Companies
            .Where(company => company.Vehicles.Any(vehicle => vehicle.Id == VehicleId) && company.IsActive == true)
            .FirstOrDefault();

        if (foundCompanies == null) return returnResult;
        returnResult = new CompaniesRecord(foundCompanies.Id, foundCompanies.Name);
        return returnResult;
    }

    public static void AddCompanyToVehicle(string licence, string companyName, string adress, string registrationNumber)
    {

        using var db = new DbmsService();

        var foundVehicle = db.Vehicles
           .Where(v => v.Licence.Equals(licence))
           .FirstOrDefault();

        Company company = new() 
        { 
            Name = companyName,
            Address = adress,
            IsActive = true,
            RegistrationNumber = registrationNumber
        };

        db.Companies.Add(company);
        db.SaveChanges();

        var foundCompany = db.Companies
            .Where(c => c.Name.Equals(companyName) && c.IsActive == true)
            .Include(c => c.Vehicles)
            .FirstOrDefault();

        foundCompany.Vehicles.Add(foundVehicle);
        db.Companies.Update(company);
        db.SaveChanges();

        //foundVehicle.Clients.Add(newClient);
        //db.Vehicles.Update(foundVehicle);
        //db.SaveChanges();
    }

    public static void AddExistedCompanyToVehicle(string licence, string companyName)
    {

        using var db = new DbmsService();

        var foundVehicle = db.Vehicles
           .Where(v => v.Licence.Equals(licence))
           .FirstOrDefault();

        var foundCompany = db.Companies
            .Where(c => c.Name.Equals(companyName) && c.IsActive == true)
            .Include(c => c.Vehicles)
            .FirstOrDefault();

        foundCompany.Vehicles.Add(foundVehicle);
        db.Companies.Update(foundCompany);
        db.SaveChanges();

        //foundVehicle.Clients.Add(newClient);
        //db.Vehicles.Update(foundVehicle);
        //db.SaveChanges();
    }

    public static bool FindCompany(string companyName)
    {

        using var db = new DbmsService();



        var foundCompany = db.Companies
            .Where(c => c.Name.Equals(companyName))
            .FirstOrDefault();

        if (foundCompany != null) 
        { 
            return true;
        }

        return false;

        //foundVehicle.Clients.Add(newClient);
        //db.Vehicles.Update(foundVehicle);
        //db.SaveChanges();
    }
}