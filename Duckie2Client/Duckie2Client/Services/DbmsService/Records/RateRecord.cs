using System;

namespace Duckie2Client.Services.DbmsService.Records;

public class RateRecord : RecordBase
{
    public decimal Value { get; set; }
    public DateOnly StartDate { get; set; }

    /// <summary>
    /// Срок окончания ставки.
    /// Ставка по умолчанию действует до начала следующего дня.
    /// </summary>
    public DateOnly EndDate { get; set; }
    // todo: Функционал продления ставок.
}