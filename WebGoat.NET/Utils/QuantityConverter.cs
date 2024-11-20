using Newtonsoft.Json;
using System;
using WebGoatCore.Models.OrderDetailDomainPrimitives;

namespace WebGoat.NET.Utils
{
    public class QuantityConverter : JsonConverter<Quantity>
    {
        public override void WriteJson(JsonWriter writer, Quantity? value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteValue(value.Value);
            }
        }

        public override Quantity ReadJson(JsonReader reader, Type objectType, Quantity? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null!;
            }

            if (reader.TokenType == JsonToken.Integer)
            {
                var value = Convert.ToInt16(reader.Value);
                return new Quantity(value);
            }

            throw new JsonSerializationException("Unexpected token type when deserializing Quantity");
        }
    }
}
