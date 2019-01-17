using System.Collections.Generic;

namespace Weather.UnitTests.TestData
{
    public class WeatherTestData
    {
        public static IEnumerable<object[]> ValidSearchParameters => new List<string[]>
        {
            new[] { "London", "imperial" },
            new[] { "Norwich", "metric" },
            new[] { "Oxford", string.Empty }
        };
    }
}
