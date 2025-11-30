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
                "Gallon (US)",
                "Quart (US)",
                "Pint (US)",
                "Cup (US)",
                "Fluid Ounce (US)",
                "Tablespoon (US)",
                "Teaspoon (US)"
            };

            FromUnit = "Milliliter (mL)";
            ToUnit = "Liter (L)";
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
                "Gallon (US)" => value * 3.78541,
                "Quart (US)" => value * 0.946353,
                "Pint (US)" => value * 0.473176,
                "Cup (US)" => value * 0.236588,
                "Fluid Ounce (US)" => value * 0.0295735,
                "Tablespoon (US)" => value * 0.0147868,
                "Teaspoon (US)" => value * 0.00492892,
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
                "Gallon (US)" => liters / 3.78541,
                "Quart (US)" => liters / 0.946353,
                "Pint (US)" => liters / 0.473176,
                "Cup (US)" => liters / 0.236588,
                "Fluid Ounce (US)" => liters / 0.0295735,
                "Tablespoon (US)" => liters / 0.0147868,
                "Teaspoon (US)" => liters / 0.00492892,
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
