using LandonWalkerCOMP4300Project1.Controllers;
using Xunit;

namespace LandonWalkerCOMP4300Project1.Tests
{
    public class VolumeConversionServiceTests
    {
        private readonly VolumeConversionService _service = new();

        [Theory]
        [InlineData(1, "Liters", "Milliliters", 1000)]
        [InlineData(1, "Gallons", "Liters", 3.78541)]
        [InlineData(2, "Cups", "Milliliters", 480)]
        public void Convert_ReturnsExpectedResults(double input, string from, string to, double expected)
        {
            var result = _service.Convert(input, from, to);
            Assert.Equal(expected, result, precision: 2); // Compares up to 2 decimal places
        }

        [Fact]
        public void Convert_ThrowsOnInvalidUnit()
        {
            Assert.Throws<ArgumentException>(() => _service.Convert(1, "BadUnit", "Liters"));
            Assert.Throws<ArgumentException>(() => _service.Convert(1, "Liters", "BadUnit"));
        }

        [Theory]
        [InlineData(0, "Liters", "Gallons", 0)]
        [InlineData(1000000, "Milliliters", "Liters", 1000)]
        public void Convert_HandlesZeroAndLargeValues(double input, string from, string to, double expected)
        {
            var result = _service.Convert(input, from, to);
            Assert.True(Math.Abs(result - expected) < 0.01);
        }

        [Theory]
        [InlineData(10, "Liters", "Liters")]
        [InlineData(250, "Milliliters", "Milliliters")]
        public void Convert_SameUnit_ReturnsSameValue(double input, string unit, string unitAgain)
        {
            var result = _service.Convert(input, unit, unitAgain);
            Assert.Equal(input, result);
        }

        [Theory]
        [InlineData(-10, "Liters", "Milliliters")]
        [InlineData(-1, "Gallons", "Quarts")]
        public void Convert_NegativeValue_ReturnsExpectedResult(double input, string from, string to)
        {
            var result = _service.Convert(input, from, to);
            Assert.True(result < 0);
        }

        [Fact]
        public void Convert_ExtremeValue_DoesNotThrow()
        {
            double extreme = double.MaxValue / 1e200;
            var result = _service.Convert(extreme, "Liters", "Milliliters");
            Assert.True(double.IsFinite(result));
        }

        [Fact]
        public void Convert_HighPrecisionFloats_PreservesPrecision()
        {
            var result = _service.Convert(0.123456789, "Liters", "Milliliters");
            Assert.InRange(result, 123.456, 123.457);
        }
    }
}
