using System;

namespace AutoFixture.Extensions.Converters
{
    public abstract class ValueConverter<TSource, TTarget> : IValueConverter
    {
        public virtual object Convert(object value)
        {
            var sourceType = typeof(TSource);
            var targetType = typeof(TTarget);

            if (sourceType == targetType)
            {
                return value;
            }

            if (value.GetType() != sourceType)
            {
                throw new ArgumentException(
                    $"Argument of type {typeof(TSource).FullName} was expected, but {value.GetType().FullName} was received.");
            }

            return Convert((TSource) value);
        }

        protected abstract TTarget Convert(TSource value);
    }
}