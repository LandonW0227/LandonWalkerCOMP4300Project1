using LandonWalkerCOMP4300Project1.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LandonWalkerCOMP4300Project1.Pages
{
    public class VolumeConverterModel(
        VolumeConversionService conversionService,
        string fromUnit,
        string toUnit,
        string errorMessage)
        : PageModel
    {
        [BindProperty]
        public double InputValue { get; set; }

        [BindProperty]
        public string FromUnit { get; set; } = fromUnit;

        [BindProperty]
        public string ToUnit { get; set; } = toUnit;

        public double? ConvertedValue { get; set; }
        public string ErrorMessage { get; set; } = errorMessage;

        public List<string> Units { get; } = ["Liters", "Milliliters", "Gallons", "Quarts", "Cups"];

        public void OnGet() { }

        public void OnPost()
        {
            try
            {
                if (InputValue < 0)
                {
                    throw new ArgumentException("Input value must be a non-negative number.");
                }

                if (!Units.Contains(FromUnit) || !Units.Contains(ToUnit))
                {
                    throw new ArgumentException("Unsupported unit selected.");
                }

                ConvertedValue = conversionService.Convert(InputValue, FromUnit, ToUnit);
            }
            catch (ArgumentException ex)
            {
                ErrorMessage = $"Input Error: {ex.Message}";
            }
            catch (Exception ex)
            {
                //Generic message
                ErrorMessage = "Oops! Something went wrong during conversion. Please try again later.";

#if DEBUG
                //For debugging purposes only
                ErrorMessage += $" (Debug Info: {ex.Message})";
#endif
            }
        }
    }
}
