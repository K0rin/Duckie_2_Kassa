using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        using var db = new DbmsService();

        var builtClient = builder.Build() as ClientRecord;

        // communication mean

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

        // bonus

        var newClientBonus = new ClientBonus();
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (builtClient.Bonus != null)
        {
            var bonusRecord = builtClient.Bonus;
            newClientBonus = new ClientBonus();
            PropertySetter.SetProperties<ClientBonusRecord, ClientBonus>(ref bonusRecord, ref newClientBonus);
        }

        // vehicle

        var newClientVehicles = new List<Vehicle>();

        foreach (var vehicle in builtClient.Vehicles)
        {
            PriceType existingPriceType;

            try
            {
                existingPriceType = db.PriceTypes.First(pt => pt.Id.Equals(vehicle.PriceType.Id));
            }
            catch (Exception e) when (e is ArgumentNullException or InvalidOperationException)
            {
                return DataModelOperationResult.RecordNotFound;
            }

            var newVehicle = new Vehicle
            {
                Licence = null!,
                PriceType = existingPriceType
            };
            var vehicleRecord = vehicle;

            PropertySetter.SetProperties<VehicleRecord, Vehicle>(ref vehicleRecord, ref newVehicle);
            newClientVehicles.Add(newVehicle);
        }

        var newClient = new Client
        {
            CommunicationMeans = newClientCommunicationMeans,
            Vehicles = newClientVehicles
        };
        if (builtClient.Bonus != null) newClient.Bonus = newClientBonus;
        PropertySetter.SetProperties<ClientRecord, Client>(ref builtClient, ref newClient);

        db.Clients.Add(newClient);
        // todo: Учитывать количество сделанных изменений. Если их 0, тогда, это ошибка.
        db.SaveChanges();

        return DataModelOperationResult.Successful;
    }
}