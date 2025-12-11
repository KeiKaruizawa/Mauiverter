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
    public class Length : INotifyPropertyChanged
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

        public Length()
        {
            Units = new ObservableCollection<string>
            {
              "Millimeter (mm)",
                "Centimeter (cm)",
                "Meter (m)",
                "Kilometer (km)",
                "Inch (in)",
                "Foot (ft)",
                "Yard (yd)",
                "Mile (mi)",
                "Nautical Mile (nmi)",
                "Micrometer (µm)",
                "Nanometer (nm)",
                "Mil (mil)"
            };
            FromUnit = "Meter (m)";
            ToUnit = "Foot (ft)";
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

            double meters = ConvertToMeters(input, FromUnit);
            double result = ConvertFromBits(meters, ToUnit);

            Result = $"{result:N4} {ToUnit}";
        }

        private double ConvertToMeters(double value, string unit)
        {
            return unit switch
            {
                "Millimeter (mm)" => value / 1000,
                "Centimeter (cm)" => value / 100,
                "Meter (m)" => value,
                "Kilometer (km)" => value * 1000,
                "Inch (in)" => value * 0.0254,
                "Foot (ft)" => value * 0.3048,
                "Yard (yd)" => value * 0.9144,
                "Mile (mi)" => value * 1609.344,
                "Nautical Mile (nmi)" => value * 1852,
                "Micrometer (µm)" => value / 1000000,
                "Nanometer (nm)" => value / 1000000000,
                "Mil (mil)" => value * 0.0000254,
                _ => 0
            };
        }

        private double ConvertFromBits(double meters, string unit)
        {
            return unit switch
            {
                "Millimeter (mm)" => meters * 1000,
                "Centimeter (cm)" => meters * 100,
                "Meter (m)" => meters,
                "Kilometer (km)" => meters / 1000,
                "Inch (in)" => meters / 0.0254,
                "Foot (ft)" => meters / 0.3048,
                "Yard (yd)" => meters / 0.9144,
                "Mile (mi)" => meters / 1609.344,
                "Nautical Mile (nmi)" => meters / 1852,
                "Micrometer (µm)" => meters * 1000000,
                "Nanometer (nm)" => meters * 1000000000,
                "Mil (mil)" => meters / 0.0000254,
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
