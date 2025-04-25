namespace LandonWalkerCOMP4300Project1.Controllers
{
    public class VolumeConversionService
    {
        public double ConvertToLiters(double value, string unit)
        {
            return unit switch
            {
                "Liters" => value,
                "Milliliters" => value / 1000,
                "Gallons" => value * 3.78541,
                "Quarts" => value * 0.946353,
                "Cups" => value * 0.24,
                _ => throw new ArgumentException($"Unsupported unit: {unit}")
            };
        }

        public double ConvertFromLiters(double liters, string unit)
        {
            return unit switch
            {
                "Liters" => liters,
                "Milliliters" => liters * 1000,
                "Gallons" => liters / 3.78541,
                "Quarts" => liters / 0.946353,
                "Cups" => liters / 0.24,
                _ => throw new ArgumentException($"Unsupported unit: {unit}")
            };
        }

        public double Convert(double input, string fromUnit, string toUnit)
        {
            var liters = ConvertToLiters(input, fromUnit);
            return ConvertFromLiters(liters, toUnit);
        }
    }
}
