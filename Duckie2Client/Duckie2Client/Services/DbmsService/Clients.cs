using System;
using System.Collections.Generic;
using Duckie2Client.Models.Database;
using System.Globalization;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Duckie2Client.Services.DbmsService;

public class Clients
{
    public static void Add()
    {
        var db = new DbmsService();

        var client1Id = Guid.NewGuid();
        var client2Id = Guid.NewGuid();

        // clients
        var client1 = new Client
        {
            Id = client1Id,
            FirstName = "First1",
            LastName = "Last1",
            Notes = "Notes1",
            Bonus = new ClientBonus
            {
                ClientId = client1Id,
                EndDateTime = DateTime.UtcNow,
                Summa = 1
            }
        };
        var client2 = new Client
        {
            Id = client2Id,
            FirstName = "First2",
            LastName = "Last2",
            Notes = "Notes2",
            Bonus = new ClientBonus
            {
                ClientId = client2Id,
                EndDateTime = DateTime.UtcNow,
                Summa = 2
            }
        };

        // contact information
        var client1Contacts1 = new CommunicationMean
        {
            Email = "email1",
            Phone = "phone1",
            Client = client1
        };
        var client1Contacts2 = new CommunicationMean
        {
            Phone = "phone11",
            Client = client1
        };
        var client2Contacts1 = new CommunicationMean
        {
            Phone = "phone2",
            Client = client2
        };

        // price types
        var priceTypeA = db.PriceTypes.Find(new Guid("4F94CF26-FC9A-4026-878D-929381B58FE8")); // cat a

        // vehicles
        var client1Vehicle = new Vehicle
        {
            Id = Guid.NewGuid(),
            Licence = "CLIENT1",
            PriceType = priceTypeA
        };
        var client2Vehicle = new Vehicle
        {
            Id = Guid.NewGuid(),
            Licence = "CLIENT2",
            PriceType = priceTypeA
        };

        client1.Vehicles?.Add(client1Vehicle);
        client2.Vehicles?.Add(client2Vehicle);

        client1.CommunicationMeans?.AddRange(new List<CommunicationMean> { client1Contacts1, client1Contacts2 });
        client2.CommunicationMeans?.AddRange(new List<CommunicationMean> { client2Contacts1 });

        db.Clients.AddRange(client1, client2);

        db.SaveChanges();

        // Получение данных со связями.
        // var companies = db.Clients
        //     .Include(c => c.CommunicationMeans)
        //     .Include(b => b.Bonus).ToList();

        {
        }
    }
}