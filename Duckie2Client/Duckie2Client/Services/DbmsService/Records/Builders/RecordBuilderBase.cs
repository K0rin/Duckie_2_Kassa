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

    private void AddId()
    {
        Product.Id = Guid.NewGuid();
    }

    public void AddId(Guid value)
    {
        Product.Id = value;
    }

    public TRecord Build()
    {
        var result = Product;
        // Reset();
        return result;
    }

    protected TRecord GetProduct()
    {
        return Product;
    }
}