using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace Mauiverter.MVVM.ViewModels
{
    public class Volume : INotifyPropertyChanged
    {
        private string _inputValue = string.Empty;
        private string _fromUnit = string.Empty;
        private string _toUnit = string.Empty;
        private string _result = string.Empty;

        public ObservableCollection<string> Units { get; set; }

        public string InputValue
        {
            get => _inputValue;
            set
            {
                _inputValue = value ?? string.Empty;
                OnPropertyChanged();
                Convert();
            }
        }

        public string FromUnit
        {
            get => _fromUnit;
            set
            {
                _fromUnit = value ?? string.Empty;
                OnPropertyChanged();
                Convert();
            }
        }

        public string ToUnit
        {
            get => _toUnit;
            set
            {
                _toUnit = value ?? string.Empty;
                OnPropertyChanged();
                Convert();
            }
        }

        public string Result
        {
            get => _result;
            set
            {
                _result = value ?? string.Empty;
                OnPropertyChanged();
            }
        }

        public Volume()
        {
            Units = new ObservableCollection<string>
            {
                "Milliliter (mL)",
                "Liter (L)",
                "Cubic Meter (m³)",
                "Cubic Centimeter (cm³)",
                "Cubic Inch (in³)",
                "Cubic Foot (ft³)",
                "Fluid Ounce US (fl oz)",
                "Cup US (cup)",
                "Pint US (pt)",
                "Quart US (qt)",
                "Gallon US (gal)",
                "Gallon UK (gal UK)",
                "Barrel (bbl)",
                "Tablespoon (tbsp)",
                "Teaspoon (tsp)"
            };
            FromUnit = "Liter (L)";
            ToUnit = "Gallon US (gal)";
            InputValue = "0";
        }

        private void Convert()
        {
            if (string.IsNullOrWhiteSpace(InputValue) || string.IsNullOrWhiteSpace(FromUnit) || string.IsNullOrWhiteSpace(ToUnit))
            {
                Result = "0";
                return;
            }

            if (!double.TryParse(InputValue, out double input))
            {
                Result = "Invalid input";
                return;
            }

            double liters = ConvertToLiters(input, FromUnit);
            double result = ConvertFromLiters(liters, ToUnit);

            Result = $"{result:N4} {ToUnit}";
        }

        private double ConvertToLiters(double value, string unit)
        {
            return unit switch
            {
                "Milliliter (mL)" => value / 1000,
                "Liter (L)" => value,
                "Cubic Meter (m³)" => value * 1000,
                "Cubic Centimeter (cm³)" => value / 1000,
                "Cubic Inch (in³)" => value * 0.0163871,
                "Cubic Foot (ft³)" => value * 28.316846592,
                "Fluid Ounce US (fl oz)" => value * 0.0295735,
                "Cup US (cup)" => value * 0.236588,
                "Pint US (pt)" => value * 0.473176,
                "Quart US (qt)" => value * 0.946353,
                "Gallon US (gal)" => value * 3.78541,
                "Gallon UK (gal UK)" => value * 4.54609,
                "Barrel (bbl)" => value * 158.987,
                "Tablespoon (tbsp)" => value * 0.0147868,
                "Teaspoon (tsp)" => value * 0.00492892,
                _ => 0
            };
        }

        private double ConvertFromLiters(double liters, string unit)
        {
            return unit switch
            {
                "Milliliter (mL)" => liters * 1000,
                "Liter (L)" => liters,
                "Cubic Meter (m³)" => liters / 1000,
                "Cubic Centimeter (cm³)" => liters * 1000,
                "Cubic Inch (in³)" => liters / 0.0163871,
                "Cubic Foot (ft³)" => liters / 28.316846592,
                "Fluid Ounce US (fl oz)" => liters / 0.0295735,
                "Cup US (cup)" => liters / 0.236588,
                "Pint US (pt)" => liters / 0.473176,
                "Quart US (qt)" => liters / 0.946353,
                "Gallon US (gal)" => liters / 3.78541,
                "Gallon UK (gal UK)" => liters / 4.54609,
                "Barrel (bbl)" => liters / 158.987,
                "Tablespoon (tbsp)" => liters / 0.0147868,
                "Teaspoon (tsp)" => liters / 0.00492892,
                _ => 0
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
