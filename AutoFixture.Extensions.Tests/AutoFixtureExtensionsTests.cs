using System;
using System.Globalization;
using System.Text;
using AutoFixture.Extensions.Converters;
using Ploeh.AutoFixture;
using Xunit;

namespace AutoFixture.Extensions.Tests
{
    public class AutoFixtureExtensionsTests
    {
        private readonly IFixture _fixture;

        public AutoFixtureExtensionsTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void DoubleToStringConversionTest()
        {
            // arrange
            var num = _fixture.Create<double>();

            // act
            var obj = _fixture.Build<Target>()
                .With(x => x.String, num)
                .Create();

            // assert
            Assert.Equal(num.ToString(CultureInfo.InvariantCulture), obj.String);
        }

        [Fact]
        public void FloatToStringConversionTest()
        {
            // arrange
            var num = _fixture.Create<float>();

            // act
            var obj = _fixture.Build<Target>()
                .With(x => x.String, num)
                .Create();

            // assert
            Assert.Equal(num.ToString(CultureInfo.InvariantCulture), obj.String);
        }

        [Fact]
        public void Int32ToStringConversionTest()
        {
            // arrange
            var num = _fixture.Create<int>();

            // act
            var obj = _fixture.Build<Target>()
                .With(x => x.String, num)
                .Create();

            // assert
            Assert.Equal(num.ToString(), obj.String);
        }

        [Fact]
        public void Int64ToStringConversionTest()
        {
            // arrange
            var num = _fixture.Create<long>();

            // act
            var obj = _fixture.Build<Target>()
                .With(x => x.String, num)
                .Create();

            // assert
            Assert.Equal(num.ToString(), obj.String);
        }

        [Fact]
        public void Utf8StringToByteArrayConversionTest()
        {
            // arrange
            var str = _fixture.Create<string>();

            // act
            var obj = _fixture.Build<Target>()
                .With(x => x.Bytes, str)
                .Create();

            // assert
            Assert.Equal(Encoding.UTF8.GetBytes(str), obj.Bytes);
        }

        [Fact]
        public void UndefinedConversionTest()
        {
            // arrange
            var bar = _fixture.Create<Random>();

            // act
            var ex = Assert.Throws<InvalidOperationException>(() => _fixture.Build<Target>().With(x => x.String, bar));

            // assert
            Assert.Equal($"Conversion from {typeof(Random).FullName} to {typeof(string).FullName} is not defined.",
                ex.Message);
        }

        [Fact]
        public void ExtendConversionTest()
        {
            // arrange
            var src = _fixture.Create<Complex>();

            // act
            var obj = _fixture.Extend(typeof(Complex), typeof(string), new ComplexToStringConverter())
                .Build<Target>()
                .With(x => x.String, src)
                .Create();

            // assert
            Assert.Equal(src.Number.ToString(), obj.String);
        }

        [Fact]
        public void StringToInt32ConversionTest()
        {
            // arrange
            var str = _fixture.Create<int>().ToString();

            // act
            var obj = _fixture.Build<Target>()
                .With(x => x.Int32, str)
                .Create();

            // assert
            Assert.Equal(int.Parse(str), obj.Int32);
        }

        private class Complex
        {
            public int Number { get; set; }
        }

        private class Target
        {
            public int Int32 { get; set; }
            public string String { get; set; }
            public byte[] Bytes { get; set; }
        }

        private class ComplexToStringConverter : ValueConverter<Complex, string>
        {
            protected override string Convert(Complex value)
            {
                return value.Number.ToString();
            }
        }
    }
}