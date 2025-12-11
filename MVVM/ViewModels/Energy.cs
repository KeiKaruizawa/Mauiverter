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
    public class Energy : INotifyPropertyChanged
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

        public Energy()
        {
            Units = new ObservableCollection<string>
            {
                "Joule (J)",
                "Kilojoule (kJ)",
                "Calorie (cal)",
                "Kilocalorie (kcal)",
                "Watt-hour (Wh)",
                "Kilowatt-hour (kWh)",
                "Electronvolt (eV)",
                "British Thermal Unit (BTU)",
                "Foot-pound (ft·lb)",
                "Erg (erg)"
            };
            FromUnit = "Joule (J)";
            ToUnit = "Kilocalorie (kcal)";
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

            double joules = ConvertToJoules(input, FromUnit);
            double result = ConvertFromJoules(joules, ToUnit);

            Result = $"{result:N4} {ToUnit}";
        }

        private double ConvertToJoules(double value, string unit)
        {
            return unit switch
            {
                "Joule (J)" => value,
                "Kilojoule (kJ)" => value * 1000,
                "Calorie (cal)" => value * 4.184,
                "Kilocalorie (kcal)" => value * 4184,
                "Watt-hour (Wh)" => value * 3600,
                "Kilowatt-hour (kWh)" => value * 3600000,
                "Electronvolt (eV)" => value * 1.602176634e-19,
                "British Thermal Unit (BTU)" => value * 1055.06,
                "Foot-pound (ft·lb)" => value * 1.3558179483314004,
                "Erg (erg)" => value * 1e-7,
                _ => 0
            };
        }

        private double ConvertFromJoules(double joules, string unit)
        {
            return unit switch
            {
                "Joule (J)" => joules,
                "Kilojoule (kJ)" => joules / 1000,
                "Calorie (cal)" => joules / 4.184,
                "Kilocalorie (kcal)" => joules / 4184,
                "Watt-hour (Wh)" => joules / 3600,
                "Kilowatt-hour (kWh)" => joules / 3600000,
                "Electronvolt (eV)" => joules / 1.602176634e-19,
                "British Thermal Unit (BTU)" => joules / 1055.06,
                "Foot-pound (ft·lb)" => joules / 1.3558179483314004,
                "Erg (erg)" => joules / 1e-7,
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
