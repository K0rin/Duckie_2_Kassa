using Duckie2Client.Libs;
using JetBrains.Annotations;
using Xunit;

namespace Duckie2Client.Tests.Libs;

[TestSubject(typeof(Strings))]
public class StringsTest
{
    [Fact]
    public void GetFirstTitleCaseTest()
    {
        const string INITIAL_STRING = "lower_case_string";
        const string EXPECTED = "Lower_case_string";
        var result = Strings.GetFirstTitleCase(INITIAL_STRING);
        Assert.Equal(EXPECTED, result);
    }
}