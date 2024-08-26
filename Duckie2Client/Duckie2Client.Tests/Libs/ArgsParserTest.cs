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
    public void StartupOptionObjectCreationTest()
    {
        const string OPTION_NAME = "testoption";
        var optionValidValues = new List<string> { "value1", "value2" };
        var result = new StartupOption(OPTION_NAME, optionValidValues, false);

        Assert.Equal(OPTION_NAME, result.Name);
        Assert.Equal(optionValidValues, result.Values);
        Assert.False(result.IsList);
    }

    public static IEnumerable<object[]> TestData()
    {
        // Проверка выброса исключения при отсутствии имени опции в первом аргументе.
        yield return [new List<string> { "option", "value" }, ErrorCodes.ArgumentsHaveNoOption];
        // Проверка корректности указания имени опции.
        yield return [new List<string> { "--invalid_name", "value" }, ErrorCodes.OptionInvalidName];
        // Проверяет каждую опцию в коллекции переданных аргументов на корректность указанного значения.
        yield return [new List<string> { "--mode", "invalid_value" }, ErrorCodes.InvalidOptionValue];
        // Проверка корректности числа значений у опций. 
        // У опции, не представляющей список, не должно быть значений более одного.
        yield return
        [
            new List<string> { "--mode", "value1", "value2", "--mode", "value3", "--mode", "value4" },
            ErrorCodes.ArgumentInvalidNumber
        ];
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public void CheckArgumentValidity_Throw_Exception_On_Invalid_Arguments(string[] given, int expected)
    {
        // given - Аргументы, переданные исполняемому файлу Приложения.

        // Коллекция корректных опций и их значений.
        var argumentCollection = new List<StartupOption> { new("mode", ["console", "kassa"], false) };
        var argsParser = new ArgsParser(given, ref argumentCollection);
        var exception = Assert.Throws<DuckieException>(() => argsParser.CheckArgumentValidity());

        Assert.Equal(expected, exception.ErrorNumber);
    }

#pragma warning disable xUnit1004
    [Fact(Skip = "Not implemented")]
#pragma warning restore xUnit1004
    public void ArgsParser_Valid_Object_Creation()
    {
    }
}