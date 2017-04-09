using System.IO;
using Newtonsoft.Json;
using Ploeh.AutoFixture;
using Xunit;

namespace AutoFixture.Extensions.Json.Tests
{
    public class AutoFixtureExtensionsTests
    {
        private readonly IFixture _fixture;

        public AutoFixtureExtensionsTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void StringToJsonConversion()
        {
            // arrange
            var source = _fixture.Create<Source>();

            // act
            var target = _fixture.Build<Target>()
                .Json(x => x.Json, source)
                .Create();

            // assert
            Assert.Equal(SerializeToJson(source), target.Json);
        }

        private string SerializeToJson<T>(T data)
        {
            using (var textWriter = new StringWriter())
            {
                new JsonSerializer().Serialize(textWriter, data);
                return textWriter.GetStringBuilder().ToString();
            }
        }

        private class Source
        {
            public string String { get; set; }
            public int Int32 { get; set; }
        }

        private class Target
        {
            public string Json { get; set; }
        }
    }
}