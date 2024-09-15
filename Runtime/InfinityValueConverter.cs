using Newtonsoft.Json;
using System;

namespace Achieve.InfinityValue
{
    public class InfinityValueConverter : JsonConverter<InfinityValue>
    {
        public override void WriteJson(JsonWriter writer, InfinityValue value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override InfinityValue ReadJson(JsonReader reader, Type objectType, InfinityValue existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                return new InfinityValue((string)reader.Value);
            }
            throw new JsonSerializationException($"Unexpected token type: {reader.TokenType}");
        }

        public override bool CanRead => true;
        public override bool CanWrite => true;
    }
}