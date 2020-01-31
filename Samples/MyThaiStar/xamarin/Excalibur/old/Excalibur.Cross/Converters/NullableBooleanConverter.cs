using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace Excalibur.Cross.Converters
{
    /// <summary>
    /// MvvmCross nullable boolean Converter
    /// </summary>
    public class NullableBooleanConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(bool?))
                throw new InvalidOperationException("The target must be a nullable boolean");

            return (bool?)value;
        }
    }
}
