using System;

namespace AutoFixture.Extensions
{
    public abstract class ValueConverter<TSource, TTarget> : IValueConverter
        where TSource : class
        where TTarget : class
    {
        public object Convert(object value)
        {
            var sourceValue = value as TSource;
            if (sourceValue == null)
            {
                throw new ArgumentException(
                    $"Argument of type {typeof(TSource).FullName} was expected, but {value.GetType().FullName} was received.");
            }

            return Convert(sourceValue);
        }

        protected abstract TTarget Convert(TSource value);
    }
}