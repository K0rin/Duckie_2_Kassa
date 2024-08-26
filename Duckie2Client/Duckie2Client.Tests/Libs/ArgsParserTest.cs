using System;
using System.Collections.Generic;
using Duckie2Client.Libs;
using Duckie2Client.Libs.Enums;
using JetBrains.Annotations;
using Xunit;

namespace Duckie2Client.Tests.Libs;

[TestSubject(typeof(ArgsParser))]
public class ArgsParserTest
{
    [Fact]
    public void CheckArgumentValidity()
    {
        // Коллекция корректных опций и их значений.
        var argumentCollection = new List<StartupOption> { new("mode", ["console", "kassa"], false) };
        // Аргументы, переданные исполняемому файлу Приложения.
        string[] currentArguments;

        ArgsParser argsParser;
        Exception exception;

        // Проверка выброса исключения при отсутствии имени опции в первом аргументе.

        currentArguments = ["option", "value"];
        argsParser = new ArgsParser(currentArguments, ref argumentCollection);
        exception = Assert.Throws<DuckieException>(() => argsParser.CheckArgumentValidity());
        Assert.Equal((int)ErrorCodes.ArgumentsHaveNoOption, ((DuckieException)exception).ErrorNumber);

        // Проверка корректности указания имени опции.

        currentArguments = ["--invalid_name", "value"];
        argsParser = new ArgsParser(currentArguments, ref argumentCollection);
        exception = Assert.Throws<DuckieException>(() => argsParser.CheckArgumentValidity());
        Assert.Equal((int)ErrorCodes.OptionInvalidName, ((DuckieException)exception).ErrorNumber);

        // Проверяет каждую опцию в коллекции переданных аргументов на корректность указанного значения.

        currentArguments = ["--mode", "invalid_value"];
        argsParser = new ArgsParser(currentArguments, ref argumentCollection);
        exception = Assert.Throws<DuckieException>(() => argsParser.CheckArgumentValidity());
        Assert.Equal((int)ErrorCodes.InvalidOptionValue, ((DuckieException)exception).ErrorNumber);

        // Проверка корректности числа значений у опций. 
        // У опции, не представляющей список, не должно быть значений более одного.

        currentArguments = ["--mode", "value1", "value2", "--mode", "value3", "--mode", "value4"];
        argsParser = new ArgsParser(currentArguments, ref argumentCollection);
        exception = Assert.Throws<DuckieException>(() => argsParser.CheckArgumentValidity());
        Assert.Equal((int)ErrorCodes.ArgumentInvalidNumber, ((DuckieException)exception).ErrorNumber);
    }
}