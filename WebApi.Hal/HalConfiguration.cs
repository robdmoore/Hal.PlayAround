using System.Web.Http;
using WebApi.Hal.Filters;
using WebApi.Hal.Formatters;

namespace WebApi.Hal
{
    public static class HalConfiguration
    {
        public static void Configure(HttpConfiguration configuration)
        {
            configuration.Formatters.Add(new JsonHalMediaTypeFormatter());
            configuration.Filters.Add(new HalFilter());
        }
    }
}
