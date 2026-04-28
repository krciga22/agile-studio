using AgileStudioServer.Features.Resources.Resource.Exceptions;

namespace AgileStudioServer.Core.Resources
{
    public class ResourceUtil
    {
        /// <exception cref="UnsupportedResourceTypeException"></exception>
        public static IResourceMap GetResourceMap(IEnumerable<IResourceMap> _ResourceMaps, string type)
        {
            var resourceMap = _ResourceMaps.FirstOrDefault(r =>
                    r.GetResourceType() == type);
            if (resourceMap == null){
                throw new UnsupportedResourceTypeException(type);
            }

            return resourceMap;
        }
    }
}
