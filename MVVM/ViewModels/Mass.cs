using Microsoft.Maui.Controls;
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
    public class Mass : INotifyPropertyChanged
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

        public Mass()
        {
            Units = new ObservableCollection<string>
            {
                "Milligram (mg)",
                "Gram (g)",
                "Kilogram (kg)",
                "Metric Ton (t)",
                "Ounce (oz)",
                "Pound (lb)",
                "Stone (st)",
                "Short Ton (ton)",
                "Long Ton (LT)",
                "Carat (ct)",
                "Microgram (µg)"
            };
            FromUnit = "Kilogram (kg)";
            ToUnit = "Pound (lb)";
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

            double kg = ConvertToKilograms(input, FromUnit);
            double result = ConvertFromKilograms(kg, ToUnit);

            Result = $"{result:N4} {ToUnit}";
        }

        private double ConvertToKilograms(double value, string unit)
        {
            return unit switch
            {
                "Milligram (mg)" => value / 1000000,
                "Gram (g)" => value / 1000,
                "Kilogram (kg)" => value,
                "Metric Ton (t)" => value * 1000,
                "Ounce (oz)" => value * 0.028349523125,
                "Pound (lb)" => value * 0.45359237,
                "Stone (st)" => value * 6.35029318,
                "Short Ton (ton)" => value * 907.18474,
                "Long Ton (LT)" => value * 1016.0469088,
                "Carat (ct)" => value * 0.0002,
                "Microgram (µg)" => value / 1000000000,
                _ => 0
            };
        }

        private double ConvertFromKilograms(double kg, string unit)
        {
            return unit switch
            {
                "Milligram (mg)" => kg * 1000000,
                "Gram (g)" => kg * 1000,
                "Kilogram (kg)" => kg,
                "Metric Ton (t)" => kg / 1000,
                "Ounce (oz)" => kg / 0.028349523125,
                "Pound (lb)" => kg / 0.45359237,
                "Stone (st)" => kg / 6.35029318,
                "Short Ton (ton)" => kg / 907.18474,
                "Long Ton (LT)" => kg / 1016.0469088,
                "Carat (ct)" => kg / 0.0002,
                "Microgram (µg)" => kg * 1000000000,
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
