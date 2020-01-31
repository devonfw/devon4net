using System;
using System.Globalization;
using MvvmCross.Converters;

namespace Excalibur.Cross.Converters
{
    /// <summary>
    /// MvvmCross Inverse boolean Converter
    /// </summary>
    public class InverseBooleanConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a boolean");

            return !(bool)value;
        }
    }
}
