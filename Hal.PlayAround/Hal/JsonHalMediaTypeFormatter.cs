using System;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using Hal.PlayAround.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hal.PlayAround.Hal
{
    public class HalConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteStartObject();

            var links = new JObject {{"self", "currenturl"}};

            writer.WritePropertyName("_links");
            serializer.Serialize(writer, links);

            foreach (var property in value.GetType().GetProperties())
            {
                writer.WritePropertyName(property.Name.ToLower());
                serializer.Serialize(writer, property.GetValue(value, null));
            }

            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            serializer.Converters.Remove(this);
            var result = serializer.Deserialize(reader, objectType);
            serializer.Converters.Add(this);
            return result;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (Person);
        }
    }

    public class JsonHalMediaTypeFormatter : JsonMediaTypeFormatter
    {
        public JsonHalMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/hal+json"));
            SerializerSettings.Converters.Add(new HalConverter());
        }

        public override bool CanReadType(Type type)
        {
            return true;
        }

        public override bool CanWriteType(Type type)
        {
            return true;
        }
    }
}