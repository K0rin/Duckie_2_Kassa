using System;
using System.Collections.Generic;
using System.Linq;
using Duckie2Client.Models;
using Duckie2Client.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Duckie2Client.Services.DbmsService;

/// <summary>
/// Contains functionality for working with the "CommunicationMeans" database entity.
/// </summary>
public class CommunicationMeans : CrudOperationsBase
{

    public static CommunicationMeansRecords? FindCommunicationClient(string phone)
    {
        CommunicationMeansRecords returnResult = null;

        using var db = new DbmsService();

        var foundCommunication = db.CommunicationMeans
            .Where(com=> com.Phone.Equals(phone))
            .FirstOrDefault(com => com.Phone == phone);

        if (foundCommunication == null) return returnResult;

        returnResult = new CommunicationMeansRecords(foundCommunication.Id,foundCommunication.Phone);

        return returnResult;
    }
}