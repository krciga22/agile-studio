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

        public static IModelService GetResourceService(
            IEnumerable<IResourceMap> _ResourceMaps, 
            IEnumerable<IModelService> _ModelServices, 
            string type)
        {
            var resourceMap = GetResourceMap(_ResourceMaps, type);

            IModelService? modelService = _ModelServices.FirstOrDefault(
                repo => repo.GetType() == resourceMap.GetResourceServiceType());

            if (modelService == null){
                throw new ResourceServiceNotFound(type);
            }

            return modelService;
        }
    }
}
