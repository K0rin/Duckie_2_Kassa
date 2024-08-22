using System;
using System.Collections.Generic;
using System.Linq;

namespace Duckie2Client.Libs.Enums;

public enum CredentialTypes
{
    Windows,
    SqlServer
}

public static class Extensions
{
    public static DatabaseService? GetDatabaseService(this CredentialTypes ct,
        params string[] args)
    {
        var myClassType = typeof(DatabaseService);

        // Check if DatabaseService has no suitable constructor.
        // todo: refact: make method.

        var constructorArgumentNumbers = new List<int>();
        var constructorList = myClassType.GetConstructors();

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var constructor in constructorList)
        {
            var parameterCounter = constructor.GetParameters().Length;
            constructorArgumentNumbers.Add(parameterCounter);
        }

        if (!constructorArgumentNumbers.Contains(args.Length))
        {
            var errorText = $"Object has no constructor with {args.Length} parameters.";
            throw new Exception(errorText);
        }

        var constr = ct switch
        {
            CredentialTypes.Windows => myClassType.GetConstructor([typeof(string), typeof(string)]),
            CredentialTypes.SqlServer => myClassType.GetConstructor([
                typeof(string), typeof(string), typeof(string), typeof(string)
            ]),
            _ => throw new ArgumentOutOfRangeException("Unknown credential type.")
        };

        var myObject = (DatabaseService)constr?.Invoke(args.Cast<object>().ToArray())!;

        return myObject;

        // var result = credentialType switch
        // {
        // CredentialTypes.Windows => func(serverName, initialCatalog),
        // CredentialTypes.SqlServer => new DatabaseService(
        // serverName,
        // initialCatalog,
        // userId,
        // userPassword),
        // _ => throw new Exception("Unknown credential type.")
        // };
        // return result;
    }
}