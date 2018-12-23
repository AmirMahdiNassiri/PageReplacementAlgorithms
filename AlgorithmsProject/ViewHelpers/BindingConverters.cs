using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AlgorithmsProject.ViewHelpers
{
    [ValueConversion(typeof(ObservableCollection<int>), typeof(string))]
    public class IntCollectionToDelimitedStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var castedValue = (ObservableCollection<int>)value;

            if (castedValue == null)
                return Binding.DoNothing;

            string result = string.Empty;

            foreach (var number in castedValue)
            {
                result += $"{number}, ";
            }

            if (result.Length >= 3)
                return result.Substring(0, result.Length - 2);
            else
                return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var castedValue = (string)value;

            if (castedValue == null)
                return Binding.DoNothing;
            
            var result = new ObservableCollection<int>();

            if (string.IsNullOrWhiteSpace(castedValue))
                return result;
            
            var splittedValues = castedValue.Split(',');

            foreach (var item in splittedValues)
            {
                int.TryParse(item, out int castedInt);

                result.Add(castedInt);
            }

            return result;
        }
    }

    [ValueConversion(typeof(ObservableCollection<Tuple<string, Func<int, int>>>), typeof(string))]
    public class FuncCollectionToDelimitedStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var castedValue = (ObservableCollection<Tuple<string, Func<int, int>>>)value;

            if (castedValue == null)
                return Binding.DoNothing;

            string result = string.Empty;

            foreach (var item in castedValue)
            {
                result += $"{item.Item1}, ";
            }

            if (result.Length >= 3)
                return result.Substring(0, result.Length - 2);
            else
                return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var castedValue = (string)value;

            if (castedValue == null)
                return Binding.DoNothing;

            var result = new ObservableCollection<Tuple<string, Func<int, int>>>();

            if (string.IsNullOrWhiteSpace(castedValue))
                return result;
            
            var splittedValues = castedValue.Split(',');

            foreach (var item in splittedValues)
            {
                var indexOfMulti = item.IndexOf('*');
                var indexOfPlus = item.IndexOf('+');

                if (indexOfMulti != -1)
                {
                    result.Add(new Tuple<string, Func<int, int>>(
                        item, new Func<int, int>(x => (int) (x * double.Parse(item.Substring(indexOfMulti + 1, item.Length - 1 - indexOfMulti))))
                        ));
                }
                else if (indexOfPlus != -1)
                {
                    result.Add(new Tuple<string, Func<int, int>>(
                        item, new Func<int, int>(x => (int) (x + double.Parse(item.Substring(indexOfPlus + 1, item.Length - 1 - indexOfPlus))))
                        ));
                }
            }

            return result;
        }
    }
}
