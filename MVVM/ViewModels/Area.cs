using Microsoft.Maui.ApplicationModel.DataTransfer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Mauiverter.MVVM.ViewModels
{
    public class Area : INotifyPropertyChanged
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

        public Area()
        {
            Units = new ObservableCollection<string>
            {
               "Square Millimeter (mm²)",
                "Square Centimeter (cm²)",
                "Square Meter (m²)",
                "Square Kilometer (km²)",
                "Square Inch (in²)",
                "Square Foot (ft²)",
                "Square Yard (yd²)",
                "Square Mile (mi²)",
                "Acre (ac)",
                "Hectare (ha)"
            };
            FromUnit = "Square Meter (m²)";
            ToUnit = "Square Foot (ft²)";
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

            double sqMeters = ConvertToSquareMeters(input, FromUnit);
            double result = ConvertFromSquareMeters(sqMeters, ToUnit);

            Result = $"{result:N4} {ToUnit}";
        }

        private double ConvertToSquareMeters(double value, string unit)
        {
            return unit switch
            {
                "Square Millimeter (mm²)" => value / 1000000,
                "Square Centimeter (cm²)" => value / 10000,
                "Square Meter (m²)" => value,
                "Square Kilometer (km²)" => value * 1000000,
                "Square Inch (in²)" => value * 0.00064516,
                "Square Foot (ft²)" => value * 0.09290304,
                "Square Yard (yd²)" => value * 0.83612736,
                "Square Mile (mi²)" => value * 2589988.110336,
                "Acre (ac)" => value * 4046.8564224,
                "Hectare (ha)" => value * 10000,
                _ => 0
            };
        }

        private double ConvertFromSquareMeters(double sqMeters, string unit)
        {
            return unit switch
            {
                "Square Millimeter (mm²)" => sqMeters * 1000000,
                "Square Centimeter (cm²)" => sqMeters * 10000,
                "Square Meter (m²)" => sqMeters,
                "Square Kilometer (km²)" => sqMeters / 1000000,
                "Square Inch (in²)" => sqMeters / 0.00064516,
                "Square Foot (ft²)" => sqMeters / 0.09290304,
                "Square Yard (yd²)" => sqMeters / 0.83612736,
                "Square Mile (mi²)" => sqMeters / 2589988.110336,
                "Acre (ac)" => sqMeters / 4046.8564224,
                "Hectare (ha)" => sqMeters / 10000,
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
