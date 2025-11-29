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
    public class Information : INotifyPropertyChanged
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

        public Information()
        {
            Units = new ObservableCollection<string>
            {
                "Bit (b)",
                "Byte (B)",
                "Kilobyte (KB)",
                "Megabyte (MB)",
                "Gigabyte (GB)",
                "Terabyte (TB)",
                "Petabyte (PB)",
                "Exabyte (EB)",
                "Zettabyte (ZB)",
                "Yottabyte (YB)"
            };

            FromUnit = "Byte (B)";
            ToUnit = "Kilobyte (KB)";
            InputValue = "0";
        }

        private void Convert()
        {
            if(string.IsNullOrWhiteSpace(InputValue) || string.IsNullOrWhiteSpace(FromUnit) || string.IsNullOrWhiteSpace(ToUnit))
            {
                Result = "0";
                return;
            }

            if (!double.TryParse(InputValue, out double input))
            {
                Result = "Invalid input";
                return;
            }

            double bits = ConvertToBits(input, FromUnit);
            double result = ConvertFromBits(bits, ToUnit);

            Result = $"{result:N4} {ToUnit}";
        }

        private double ConvertToBits(double value, string unit)
        {
            return unit switch
            {
                "Bit (b)" => value,
                "Byte (B)" => value * 8,
                "Kilobyte (KB)" => value * 8 * 1024,
                "Megabyte (MB)" => value * 8 * 1024 * 1024,
                "Gigabyte (GB)" => value * 8 * 1024 * 1024 * 1024,
                "Terabyte (TB)" => value * 8 * Math.Pow(1024, 4),
                "Petabyte (PB)" => value * 8 * Math.Pow(1024, 5),
                "Exabyte (EB)" => value * 8 * Math.Pow(1024, 6),
                "Zettabyte (ZB)" => value * 8 * Math.Pow(1024, 7),
                "Yottabyte (YB)" => value * 8 * Math.Pow(1024, 8),
                _ => 0
            };
        }

        private double ConvertFromBits (double bits, string unit)
        {
            return unit switch
            {
                "Bit (b)" => bits,
                "Byte (B)" => bits / 8,
                "Kilobyte (KB)" => bits / (8 * 1024),
                "Megabyte (MB)" => bits / (8 * 1024 * 1024),
                "Gigabyte (GB)" => bits / (8 * Math.Pow(1024, 3)),
                "Terabyte (TB)" => bits / (8 * Math.Pow(1024, 4)),
                "Petabyte (PB)" => bits / (8 * Math.Pow(1024, 5)),
                "Exabyte (EB)" => bits / (8 * Math.Pow(1024, 6)),
                "Zettabyte (ZB)" => bits / (8 * Math.Pow(1024, 7)),
                "Yottabyte (YB)" => bits / (8 * Math.Pow(1024, 8)),
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
