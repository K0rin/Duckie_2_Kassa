using System.Collections.Generic;
using Duckie2Client.Enums.Flags;
using Duckie2Client.Services.DbmsService.Records;
using Duckie2Client.Services.DbmsService.Records.Builders;
using Microsoft.EntityFrameworkCore;

namespace Duckie2Client.Services.DbmsService;

public abstract class CrudOperationsBase
{
    // ReSharper disable once UnusedMethodReturnValue.Global
    // ReSharper disable once UnusedMemberInSuper.Global
    public virtual DataModelOperationResult Create<T>(RecordBuilderBase<T> builder) where T : RecordBase, new()
    {
        throw new System.NotSupportedException();
    }

    // ReSharper disable once VirtualMemberNeverOverridden.Global
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once UnusedMemberInSuper.Global
    public virtual object Read<TDataModel>(RecordReadFlags readFlags)
    {
        // todo: Постраничное чтение записей из базы.
        throw new System.NotSupportedException();
    }

    // ReSharper disable once VirtualMemberNeverOverridden.Global
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once UnusedParameter.Global
    // ReSharper disable once UnusedMemberInSuper.Global
    public virtual DataModelOperationResult DeleteMany<T>(List<RecordBuilderBase<T>> builder)
        where T : RecordBase, new()
    {
        throw new System.NotSupportedException();
    }

    // ReSharper disable once VirtualMemberNeverOverridden.Global
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once UnusedParameter.Global
    // ReSharper disable once UnusedMemberInSuper.Global
    public virtual DataModelOperationResult Delete<T>(RecordBuilderBase<T> builder) where T : RecordBase, new()
    {
        throw new System.NotSupportedException();
    }

    // ReSharper disable once VirtualMemberNeverOverridden.Global
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once UnusedParameter.Global
    public virtual DataModelOperationResult Update<T>(RecordBuilderBase<T> builder) where T : RecordBase, new()
    {
        throw new System.NotSupportedException();
    }

    protected static DataModelOperationResult AddAndSave<T>(DbSet<T> dbSet, DbmsService dbmsService, T newRecord)
        where T : class
    {
        dbSet.Add(newRecord);
        dbmsService.SaveChanges();
        // todo: Учитывать количество сделанных изменений. Если их 0, тогда, это ошибка.

        return DataModelOperationResult.Successful;
    }
}