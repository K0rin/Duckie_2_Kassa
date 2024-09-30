using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Duckie2Client.Services.DbmsService.Records;

namespace Duckie2Client.Services;

public static class PropertySetter
{
    /// <summary>
    /// Assigns the attribute values of the sourceClass object to the attributes of the targetClass object.
    /// </summary>
    /// <param name="sourceClass">An object whose attribute values will be assigned to another object.</param>
    /// <param name="targetClass">An object whose attribute values will take values from another object.</param>
    /// <param name="skipNullValues">True - skip set a value for a property, if a source class has the null value in an
    /// appropriate property; otherwise, False - set a property value.</param>
    /// <typeparam name="TS"></typeparam>
    /// <typeparam name="TT"></typeparam>
    public static void SetProperties<TS, TT>(ref TS sourceClass, ref TT targetClass, bool skipNullValues = false)
    {
        var sourceType = sourceClass?.GetType();
        var targetType = targetClass?.GetType();

        foreach (var sourceProperty in sourceType?.GetProperties()!)
        {
            // todo: refact: make generic type for RecordBase. TSkip. To skip specified type.

            var isRecordBaseChild = IsRecordBaseChild<RecordBase>(sourceProperty);
            if (isRecordBaseChild) continue;

            // Look for the corresponding property in the target object.
            var targetProperty = targetType?.GetProperty(sourceProperty.Name);

            // If the property is not found or has no the same type, go the next property.
            if (targetProperty == null || targetProperty.PropertyType != sourceProperty.PropertyType) continue;

            var value = sourceProperty.GetValue(sourceClass);
            if (value != null || !skipNullValues) targetProperty.SetValue(targetClass, value);
        }
    }

    /// <summary>
    /// Indicator that determines whether the sourceProperty type is derived from <see cref="RecordBase"/>.
    /// </summary>
    /// <param name="sourceProperty">A property whose type should be checked against the specified type.</param>
    /// <returns>True - if the property type is compliant to the specified type. Otherwise - False.</returns>
    /// <typeparam name="T">The type for which the compliance check is being performed.</typeparam>
    private static bool IsRecordBaseChild<T>(PropertyInfo sourceProperty)
    {
        var condition = new List<bool> { false, false };
        try
        {
            condition[0] = sourceProperty.PropertyType.IsSubclassOf(typeof(T));
            // In case of List type take the type of list elements.
            condition[1] = sourceProperty.PropertyType.GenericTypeArguments[0].IsSubclassOf(typeof(T));
        }
        catch (IndexOutOfRangeException)
        {
        }

        var result = condition.Aggregate((acc, val) => acc || val);
        return result;
    }
}