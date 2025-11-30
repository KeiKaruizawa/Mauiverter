using Microsoft.WindowsAppSDK.Runtime.Packages;
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
                "Millisecond (ms)",
                "Second (s)",
                "Minute (min)",
                "Hour (h)",
                "Day (d)",
                "Week (wk)",
                "Month (mo)",
                "Year (yr)"
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
                "Millisecond (ms)" => value / 1000,
                "Second (s)" => value,
                "Minute (min)" => value * 60,
                "Hour (h)" => value * 3600,
                "Day (d)" => value * 86400,
                "Week (wk)" => value * 604800,
                "Month (mo)" => value * 2628000,  // Approximate: 30.4167 days
                "Year (yr)" => value * 31536000,  // 365 days
                _ => 0
            };
        }

        private double ConvertFromSeconds(double seconds, string unit)
        {
            return unit switch
            {
                "Millisecond (ms)" => seconds * 1000,
                "Second (s)" => seconds,
                "Minute (min)" => seconds / 60,
                "Hour (h)" => seconds / 3600,
                "Day (d)" => seconds / 86400,
                "Week (wk)" => seconds / 604800,
                "Month (mo)" => seconds / 2628000,
                "Year (yr)" => seconds / 31536000,
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
