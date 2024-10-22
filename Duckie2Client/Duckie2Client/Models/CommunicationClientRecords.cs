using Duckie2Client.Models.Database;
using System;
using System.Collections.Generic;

namespace Duckie2Client.Models;

public record CommunicationClientRecords(Guid ?Id, string ?Phone, string FirstName, string LastName, Guid ?ClientId);