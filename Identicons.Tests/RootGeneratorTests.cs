using System.Collections;

namespace Identicons.Tests
{
    public class RootGeneratorTests
    {
        /// <summary>
        /// A basic test to validate that a bunch of different strings can run without breaking.
        /// </summary>
        [TestCaseSource(typeof(SmokeTestCases))]
        public async Task SmokeTest(string str)
        {
            using (var mem = new MemoryStream())
            {
                await RootGenerator.Generate(str, mem);
            }
        }

        public class SmokeTestCases : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                for (var i = 0; i < 1000; i++)
                {
                    yield return new object[] { i.ToString() };
                }
            }
        }
    }
}
