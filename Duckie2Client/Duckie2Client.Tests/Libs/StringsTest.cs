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

        const string INITIAL_STRING_2 = "Upper case string";
        const string EXPECTED_2 = "Upper case string";
        var result2 = Strings.GetFirstTitleCase(INITIAL_STRING_2);
        Assert.Equal(EXPECTED_2, result2);
    }
}