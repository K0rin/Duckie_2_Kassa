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

        //  var client1Id = Guid.NewGuid();
        //  var client2Id = Guid.NewGuid();
        //
        //  var client1 = new Client
        //  {
        //      Id = client1Id,
        //      FirstName = "First1",
        //      LastName = "Last1",
        //      Notes = "Notes1",
        //      Bonus = new ClientBonus
        //      {
        //          ClientId = client1Id,
        //          EndDateTime = DateTime.UtcNow,
        //          Summa = 1
        //      }
        //  };
        //  var client2 = new Client
        //  {
        //      Id = client2Id,
        //      FirstName = "First2",
        //      LastName = "Last2",
        //      Notes = "Notes2",
        //      Bonus = new ClientBonus
        //      {
        //          ClientId = client2Id,
        //          EndDateTime = DateTime.UtcNow,
        //          Summa = 2
        //      }
        //  };
        //
        //  db.Clients.AddRange(client1, client2);
        //  db.SaveChanges(); 
        //  
        //  
        //  var client1contacts1 = new CommunicationMean
        //  {
        //      Email = "email1",
        //      Phone = "phone1",
        //      Client = client1
        //  };
        //  var client1contacts2 = new CommunicationMean
        //  {
        //      Phone = "phone11",
        //      Client = client1
        //  };
        //  
        // db.CommunicationMeans.AddRange(client1contacts1, client1contacts2); 
        //  
        //  db.SaveChanges();

        var companies = db.Clients
            .Include(c => c.CommunicationMeans)
            .Include(b => b.Bonus).ToList();


        // db.PriceTypes.Add(new PriceType { Name = "catA" });
        // db.SaveChanges();

        // client.Vehicles.PriceType = db.PriceTypes.First(e => e.Name.Equals("catA"));

        // db.Clients.Add(client);
        // db.SaveChanges();


        {
        }
    }
}