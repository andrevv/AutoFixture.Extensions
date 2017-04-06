using System.Text;

namespace AutoFixture.Extensions.Converters
{
    public class StringToByteArrayConverter : ValueConverter<string, byte[]>
    {
        private readonly Encoding _encoding;

        public StringToByteArrayConverter(Encoding encoding)
        {
            _encoding = encoding;
        }

        protected override byte[] Convert(string value)
        {
            return _encoding.GetBytes(value);
        }
    }
}