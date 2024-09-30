using System;

namespace Duckie2Client.Services.DbmsService.Records.Builders;

public abstract class RecordBuilderBase<TRecord> : IRecordBuilder where TRecord : RecordBase, new()
{
    private TRecord Product { get; set; } = new();

    protected RecordBuilderBase()
    {
        Reset();
        AddId();
    }

    private void Reset()
    {
        Product = new TRecord();
    }

    public void AddId()
    {
        Product.Id = Guid.NewGuid();
    }

    public TRecord Build()
    {
        var result = Product;
        Reset();
        return result;
    }

    public TRecord GetProduct()
    {
        return Product;
    }
}