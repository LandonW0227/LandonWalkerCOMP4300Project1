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
            Assert.True(Math.Abs(result - expected) < 0.01);
        }

        [Fact]
        public void Convert_ThrowsOnInvalidUnit()
        {
            Assert.Throws<ArgumentException>(() => _service.Convert(1, "BadUnit", "Liters"));
            Assert.Throws<ArgumentException>(() => _service.Convert(1, "Liters", "BadUnit"));
        }
    }
}
