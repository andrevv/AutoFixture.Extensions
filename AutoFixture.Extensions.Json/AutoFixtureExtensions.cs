using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using Newtonsoft.Json;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Dsl;

namespace AutoFixture.Extensions.Json
{
    public static class AutoFixtureExtensions
    {
        private const string Default = "default";

        private static readonly Func<Type, string> GetSerializerKey =
            (Type type) => type.FullName;

        private static readonly IDictionary<string, JsonSerializer> Serializers =
            new Dictionary<string, JsonSerializer>
            {
                {Default, new JsonSerializer()}
            };

        public static IPostprocessComposer<T> Json<T, TProperty, TValue>(
            this IPostprocessComposer<T> composer,
            Expression<Func<T, TProperty>> propertyPicker,
            TValue value)
            where TProperty : class
        {
            return composer.With(propertyPicker, Serialize(value));
        }

        public static void Extend(this IFixture fixture, Type type, JsonSerializer serializer)
        {
            Serializers[GetSerializerKey(type)] = serializer;
        }

        private static string Serialize<T>(T value)
        {
            using (var textWriter = new StringWriter())
            {
                var key = GetSerializerKey(typeof(T));
                var serializer = Serializers[Serializers.ContainsKey(key) ? key : Default];
                serializer.Serialize(textWriter, value);
                return textWriter.GetStringBuilder().ToString();
            }
        }
    }
}