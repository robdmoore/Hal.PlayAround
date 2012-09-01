using System.Collections.Generic;

namespace WebApi.Hal.Dtos
{
    public class HypermediaContent
    {
        public object Content { get; private set; }
        public List<Link> Links { get; private set; }

        public HypermediaContent(object content)
        {
            Content = content;
            Links = new List<Link>();
        }
    }
}
