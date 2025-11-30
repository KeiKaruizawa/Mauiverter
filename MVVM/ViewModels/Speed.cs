using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Mauiverter.MVVM.ViewModels
{
    public class Speed : INotifyPropertyChanged
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

        public Speed()
        {
            Units = new ObservableCollection<string>
            {
                "Meters per Second (m/s)",
                "Kilometers per Hour (km/h)",
                "Miles per Hour (mph)",
                "Feet per Second (ft/s)",
                "Knot (kn)"
            };

            FromUnit = "Meters per Second (m/s)";
            ToUnit = "Kilometers per Hour (km/h)";
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

            double mps = ConvertToMetersPerSecond(input, FromUnit);
            double result = ConvertFromMetersPerSecond(mps, ToUnit);

            Result = $"{result:N4} {ToUnit}";
        }

        private double ConvertToMetersPerSecond(double value, string unit)
        {
            return unit switch
            {
                "Meters per Second (m/s)" => value,
                "Kilometers per Hour (km/h)" => value / 3.6,
                "Miles per Hour (mph)" => value * 0.44704,
                "Feet per Second (ft/s)" => value * 0.3048,
                "Knot (kn)" => value * 0.514444,
                _ => 0
            };
        }

        private double ConvertFromMetersPerSecond(double mps, string unit)
        {
            return unit switch
            {
                "Meters per Second (m/s)" => mps,
                "Kilometers per Hour (km/h)" => mps * 3.6,
                "Miles per Hour (mph)" => mps / 0.44704,
                "Feet per Second (ft/s)" => mps / 0.3048,
                "Knot (kn)" => mps / 0.514444,
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
