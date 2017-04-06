using System.Globalization;

namespace AutoFixture.Extensions.Converters
{
    public class ObjectToStringConverter : IValueConverter
    {
        public object Convert(object value)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}", value);
        }
    }
}