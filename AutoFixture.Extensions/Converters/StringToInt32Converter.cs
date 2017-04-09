namespace AutoFixture.Extensions.Converters
{
    public class StringToInt32Converter : ValueConverter<string, int>
    {
        protected override int Convert(string value)
        {
            return int.Parse(value);
        }
    }
}