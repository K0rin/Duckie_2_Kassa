using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Bogus.DataSets;
using Duckie2Client.Models;
using Duckie2Client.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Duckie2Client.Services.DbmsService;

/// <summary>
/// Contains functionality for working with the "Vehicle" database entity.
/// </summary>
public class Prices : CrudOperationsBase
{

    public static List<PricesRecord>? GetServicesListInEn()
    {
        List<PricesRecord> returnResult = new List<PricesRecord>();

        using var db = new DbmsService();

        var services = db.Prices
            .Include(p => p.Value)
            .Include(price => price.TradeUnit.Where(td => td.IsGood == false))
            .ThenInclude(td => td.Names.Where(nm => nm.Locale == "EN"));
        
        if (services == null) return returnResult;

        foreach (var service in services)
        {
            if (service.TradeUnit != null) 
            {
                foreach (var tradeunit in service.TradeUnit)
                {
                    if (tradeunit.Names != null) 
                    {
                        foreach (var name in tradeunit.Names)
                        {
                            TimeOnly currentTime = TimeOnly.FromDateTime(DateTime.Now);
                            TimeOnly? timeToAdd = tradeunit.ProcessTime;
                            TimeOnly resultTime;
                            if (timeToAdd.HasValue)
                            {
                                // Добавляем только если значение существует
                                resultTime = currentTime.Add(timeToAdd.Value.ToTimeSpan());
                                PricesRecord record = new PricesRecord(service.Id, tradeunit.Id, name.Value, service.Value.Id, service.Value.Value, tradeunit.ProcessTime, currentTime, resultTime);
                                returnResult.Add(record);
                            }
                        }
                    }                    
                }
            }
        }
        return returnResult;
    }

    public static List<PricesRecord>? GetGoodsListInEn()
    {
        List<PricesRecord> returnResult = new List<PricesRecord>();

        using var db = new DbmsService();

        var services = db.Prices
            .Include(p => p.Value)
            .Include(price => price.TradeUnit.Where(td => td.IsGood == true))
            .ThenInclude(td => td.Names.Where(nm => nm.Locale == "EN"));

        if (services == null) return returnResult;

        foreach (var service in services)
        {
            if (service.TradeUnit != null)
            {
                foreach (var tradeunit in service.TradeUnit)
                {
                    if (tradeunit.Names != null)
                    {
                        foreach (var name in tradeunit.Names)
                        {
                            PricesRecord record = new PricesRecord(service.Id, tradeunit.Id, name.Value, service.Value.Id, service.Value.Value, null, null, null);
                            returnResult.Add(record);
                        }
                    }
                }
            }
        }
        return returnResult;
    }
}