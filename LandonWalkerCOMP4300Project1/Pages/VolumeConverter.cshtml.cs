using LandonWalkerCOMP4300Project1.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LandonWalkerCOMP4300Project1.Pages
{
    public class VolumeConverterModel(VolumeConversionService conversionService) : PageModel
    {
        [BindProperty]
        [FromForm]
        public string? InputValueRaw { get; set; }

        public double InputValue
        {
            get => double.TryParse(InputValueRaw, out var value) ? value : 0.0;
            set => InputValueRaw = value.ToString();
        }

        [BindProperty]
        public string? FromUnit { get; set; }

        [BindProperty]
        public string? ToUnit { get; set; }

        public double? ConvertedValue { get; set; }
        public string? ErrorMessage { get; set; }

        public void OnPostReset()
        {
            //Clear all bound properties
            InputValue = 0.0;
            FromUnit = null;
            ToUnit = null;
            ConvertedValue = null;
            ErrorMessage = null;
        }

        public List<string> Units { get; } = ["Liters", "Milliliters", "Gallons", "Quarts", "Cups"];

        public void OnGet() { }

        public void OnPost()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(InputValueRaw))
                    throw new ArgumentException("Please enter a value.");

                if (!double.TryParse(InputValueRaw, out double inputValue))
                    throw new ArgumentException("Only numeric values are allowed.");

                if (InputValue < 0)
                {
                    throw new ArgumentException("Input value must be a non-negative number.");
                }

                if (ToUnit != null && FromUnit != null && (!Units.Contains(FromUnit) || !Units.Contains(ToUnit)))
                {
                    throw new ArgumentException("Unsupported unit selected.");
                }

                if (FromUnit != null)
                    if (ToUnit != null)
                        ConvertedValue = conversionService.Convert(InputValue, FromUnit, ToUnit);
            }
            catch (ArgumentException ex)
            {
                ErrorMessage = $"Input Error: {ex.Message}";
            }
            catch (Exception ex)
            {
                ErrorMessage = "Oops! Something went wrong during conversion. Please try again later.";

#if DEBUG
                ErrorMessage += $" (Debug Info: {ex.Message})";
#endif
            }
        }
    }
}
