using WebApi.Hal.Dtos;

namespace WebApi.Hal.Interfaces
{
    public interface IHalAwareController
    {
        Link GetLinkForResource(string resourceId, object o);
    }
}
