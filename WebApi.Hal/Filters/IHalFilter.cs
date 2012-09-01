using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Filters;
using WebApi.Hal.Attributes;
using WebApi.Hal.Dtos;
using WebApi.Hal.Interfaces;

namespace WebApi.Hal.Filters
{
    public class HalFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (!actionExecutedContext.Request.Headers.Accept.Any(h => h.MediaType.ToLower() == "application/hal+json"))
                return;

            var controller = actionExecutedContext.ActionContext.ControllerContext.Controller as IHalAwareController;
            if (controller == null)
                return;

            var objectContent = actionExecutedContext.Response.Content as ObjectContent;
            if (objectContent == null)
                return;

            var attrs = actionExecutedContext.ActionContext.ActionDescriptor.GetCustomAttributes<LinkedResourceAttribute>()
                ?? new BindingList<LinkedResourceAttribute>();

            HypermediaContent newContent;
            var objectList = objectContent.Value as IEnumerable;
            if (objectList != null)
            {
                // todo: this is wrong because they should be embedded
                var newList = new ArrayList();
                newContent = new HypermediaContent(newList);
                foreach (var o in objectList)
                {
                    var newNewContent = new HypermediaContent(o);
                    var o1 = o;
                    attrs.Select(attr => controller.GetLinkForResource(attr.Resource, o1)).ToList()
                        .ForEach(a => newNewContent.Links.Add(a));
                    newList.Add(newNewContent);
                }
            }
            else
            {
                newContent = new HypermediaContent(objectContent.Value);
                var o = objectContent.Value;
                attrs.Select(attr => controller.GetLinkForResource(attr.Resource, o)).ToList()
                    .ForEach(a => newContent.Links.Add(a));

                if (actionExecutedContext.Request.Method != HttpMethod.Get && attrs.All(a => a.Resource != Hal.Resource.Self))
                    newContent.Links.Add(controller.GetLinkForResource(Hal.Resource.Self, o));
            }
            if (newContent.Links.All(l => l.Rel != Hal.Resource.Self))
                newContent.Links.Add(new Link(Hal.Resource.Self, actionExecutedContext.Request.RequestUri.AbsolutePath));
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(actionExecutedContext.Response.StatusCode, newContent);
        }
    }
}