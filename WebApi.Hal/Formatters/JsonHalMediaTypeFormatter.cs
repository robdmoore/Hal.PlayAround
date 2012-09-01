using System;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using WebApi.Hal.Converters;
using WebApi.Hal.Dtos;

namespace WebApi.Hal.Formatters
{
    public class JsonHalMediaTypeFormatter : JsonMediaTypeFormatter
    {
        public JsonHalMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/hal+json"));
            SerializerSettings.Converters.Add(new HypermediaContentConverter());
            SerializerSettings.Converters.Add(new LinksConverter());
        }

        public override bool CanReadType(Type type)
        {
            return typeof(HypermediaContent).IsAssignableFrom(type);
        }

        public override bool CanWriteType(Type type)
        {
            return typeof(HypermediaContent).IsAssignableFrom(type);
        }
    }
}