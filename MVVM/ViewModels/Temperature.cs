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
    public class Temperature : INotifyPropertyChanged
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

        public Temperature()
        {
            Units = new ObservableCollection<string>
            {
                "Celsius (°C)",
                "Fahrenheit (°F)",
                "Kelvin (K)"
            };

            FromUnit = "Celsius (°C)";
            ToUnit = "Fahrenheit (°F)";
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

            double result = ConvertTemperature(input, FromUnit, ToUnit);
            Result = $"{result:N2} {ToUnit}";
        }

        private double ConvertTemperature(double value, string from, string to)
        {
            // First convert to Celsius
            double celsius = from switch
            {
                "Celsius (°C)" => value,
                "Fahrenheit (°F)" => (value - 32) * 5 / 9,
                "Kelvin (K)" => value - 273.15,
                _ => 0
            };

            // Then convert from Celsius to target unit
            return to switch
            {
                "Celsius (°C)" => celsius,
                "Fahrenheit (°F)" => (celsius * 9 / 5) + 32,
                "Kelvin (K)" => celsius + 273.15,
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
