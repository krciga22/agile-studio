using AgileStudioServer.Core.Services;
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

        /// <exception cref="ResourceServiceNotFoundException"></exception>
        public static IModelService GetModelService(
            IEnumerable<IModelService> _ModelServices,
            IResourceMap resourceMap)
        {
            IModelService? modelService = _ModelServices.FirstOrDefault(
                repo => repo.GetType() == resourceMap.GetResourceModelServiceType());

            if (modelService == null){
                throw new ResourceServiceNotFoundException(resourceMap.GetResourceType());
            }

            return modelService;
        }
    }
}
