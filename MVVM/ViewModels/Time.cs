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
    public class Time : INotifyPropertyChanged
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

        public Time()
        {
            Units = new ObservableCollection<string>
            {
                "Nanosecond (ns)",
                "Microsecond (µs)",
                "Millisecond (ms)",
                "Second (s)",
                "Minute (min)",
                "Hour (h)",
                "Day (d)",
                "Week (wk)",
                "Month (mo)",
                "Year (yr)",
                "Decade (dec)",
                "Century (c)"
            };
            FromUnit = "Second (s)";
            ToUnit = "Minute (min)";
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

            double seconds = ConvertToSeconds(input, FromUnit);
            double result = ConvertFromSeconds(seconds, ToUnit);

            Result = $"{result:N4} {ToUnit}";
        }

        private double ConvertToSeconds(double value, string unit)
        {
            return unit switch
            {
                "Nanosecond (ns)" => value / 1000000000,
                "Microsecond (µs)" => value / 1000000,
                "Millisecond (ms)" => value / 1000,
                "Second (s)" => value,
                "Minute (min)" => value * 60,
                "Hour (h)" => value * 3600,
                "Day (d)" => value * 86400,
                "Week (wk)" => value * 604800,
                "Month (mo)" => value * 2629800,
                "Year (yr)" => value * 31557600,
                "Decade (dec)" => value * 315576000,
                "Century (c)" => value * 3155760000,
                _ => 0
            };
        }

        private double ConvertFromSeconds(double seconds, string unit)
        {
            return unit switch
            {
                "Nanosecond (ns)" => seconds * 1000000000,
                "Microsecond (µs)" => seconds * 1000000,
                "Millisecond (ms)" => seconds * 1000,
                "Second (s)" => seconds,
                "Minute (min)" => seconds / 60,
                "Hour (h)" => seconds / 3600,
                "Day (d)" => seconds / 86400,
                "Week (wk)" => seconds / 604800,
                "Month (mo)" => seconds / 2629800,
                "Year (yr)" => seconds / 31557600,
                "Decade (dec)" => seconds / 315576000,
                "Century (c)" => seconds / 3155760000,
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
