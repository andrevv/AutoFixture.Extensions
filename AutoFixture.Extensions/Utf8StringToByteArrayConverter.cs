using System.Text;

namespace AutoFixture.Extensions
{
    public class Utf8StringToByteArrayConverter : ValueConverter<string, byte[]>
    {
        protected override byte[] Convert(string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }
    }
}