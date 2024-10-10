using System;
using System.Collections.Generic;
using System.Linq;
using Duckie2Client.Enums.Flags;
using Duckie2Client.Models.Database;
using Duckie2Client.Services.DbmsService.Records;
using Duckie2Client.Services.DbmsService.Records.Builders;

namespace Duckie2Client.Services.DbmsService;

/// <summary>
/// Contains functionality for working with the “Client” database entity.
/// </summary>
public class Clients : CrudOperationsBase
{
    public override object Read<TDataModel>(RecordReadFlags readFlags)
    {
        object result;

        using (var db = new DbmsService())
        {
            result = db.Clients.ToList();
        }

        return (List<TDataModel>)result;
    }

    public override DataModelOperationResult Create<T>(RecordBuilderBase<T> builder)
    {
        if (builder.Build() is not ClientRecord builtClient) return DataModelOperationResult.RecordNotFound;

        var newClientCommunicationMeans = CreateCommunicationMeanList(builtClient);
        var newClientBonus = CreateClientBonus(builtClient);

        using var db = new DbmsService();

        List<Vehicle> newClientVehicles;
        try
        {
            newClientVehicles = CreateVehicleList(builtClient, db);
        }
        catch (Exception e) when (e is ArgumentNullException or InvalidOperationException)
        {
            return DataModelOperationResult.RecordNotFound;
        }

        var newClient = CreateClient(builtClient, newClientCommunicationMeans, newClientVehicles, newClientBonus);
        return AddAndSave(db.Clients, db, newClient);
    }


    private static List<CommunicationMean> CreateCommunicationMeanList(ClientRecord? builtClient)
    {
        var newClientCommunicationMeans = new List<CommunicationMean>();

        foreach (var communicationMean in builtClient?.CommunicationMeans!)
        {
            var newCommunicationMean = new CommunicationMean();
            var communicationMeanRecord = communicationMean;
            PropertySetter.SetProperties<CommunicationMeanRecord, CommunicationMean>(
                ref communicationMeanRecord,
                ref newCommunicationMean);
            newClientCommunicationMeans.Add(newCommunicationMean);
        }

        return newClientCommunicationMeans;
    }

    private static ClientBonus? CreateClientBonus(ClientRecord? builtClient)
    {
        if (builtClient?.Bonus != null) return null;

        var bonusRecord = builtClient?.Bonus;
        var newClientBonus = new ClientBonus();

        PropertySetter.SetProperties<ClientBonusRecord, ClientBonus>(ref bonusRecord!, ref newClientBonus);

        return newClientBonus;
    }

    // ReSharper disable once MemberCanBeMadeStatic.Local
#pragma warning disable CA1822
    private List<Vehicle> CreateVehicleList(ClientRecord? builtClient, DbmsService db)
#pragma warning restore CA1822
    {
        var newClientVehicles = new List<Vehicle>();

        foreach (var vehicle in builtClient?.Vehicles!)
        {
            var existingPriceType = db.PriceTypes.First(pt => pt.Id.Equals(vehicle.PriceType.Id));
            var newVehicle = new Vehicle
            {
                Licence = null!,
                PriceType = existingPriceType
            };
            var vehicleRecord = vehicle;

            PropertySetter.SetProperties<VehicleRecord, Vehicle>(ref vehicleRecord, ref newVehicle);
            newClientVehicles.Add(newVehicle);
        }

        return newClientVehicles;
    }

    private static Client CreateClient(
        ClientRecord? builtClient,
        List<CommunicationMean> newClientCommunicationMeans,
        List<Vehicle> newClientVehicles,
        ClientBonus? newClientBonus)
    {
        var newClient = new Client
        {
            CommunicationMeans = newClientCommunicationMeans,
            Vehicles = newClientVehicles
        };
        if (builtClient?.Bonus != null) newClient.Bonus = newClientBonus;
        PropertySetter.SetProperties<ClientRecord, Client>(ref builtClient!, ref newClient);
        return newClient;
    }
}