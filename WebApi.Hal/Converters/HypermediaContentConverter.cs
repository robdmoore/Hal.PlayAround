using System;
using System.Collections;
using Newtonsoft.Json;
using WebApi.Hal.Dtos;

namespace WebApi.Hal.Converters
{
    public class HypermediaContentConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var content = (HypermediaContent)value;

            writer.WriteStartObject();
            writer.WritePropertyName("_links");
            serializer.Serialize(writer, content.Links);
            
            if (content.Content is IEnumerable)
            {
                writer.WritePropertyName("values");
                serializer.Serialize(writer, content.Content);
            }
            else
            {
                foreach (var property in content.Content.GetType().GetProperties())
                {
                    writer.WritePropertyName(property.Name.ToLower());
                    serializer.Serialize(writer, property.GetValue(content.Content, null));
                }
            }

            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return reader.Value;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(HypermediaContent).IsAssignableFrom(objectType);
        }
    }
}