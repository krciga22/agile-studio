using AgileStudioServer.Core.Command;
using AgileStudioServer.Core.Events;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Core.Resources;
using AgileStudioServer.Features.Resources.Resource.Events;

namespace AgileStudioServer.Features.Resources.Resource.Commands
{
    public class ResourceCreateCommandHandler : AbstractCommandHandler
    {
        private readonly EventDispatcher _EventDispatcher;

        private readonly IEnumerable<IResourceMap> _ResourceMaps;

        private readonly IEnumerable<Repository> _Repositories;

        public ResourceCreateCommandHandler(
            EventDispatcher eventDispatcher,
            IEnumerable<IResourceMap> resourceMaps,
            IEnumerable<Repository> repositories)
        {
            _EventDispatcher = eventDispatcher;
            _ResourceMaps = resourceMaps;
            _Repositories = repositories;
        }

        public override Type[] GetCommands()
        {
            return [typeof(ResourceCreateCommand)];
        }

        public override void Handle(ICommand command, ICommandResult result)
        {
            ResourceCreateCommand cmd = (ResourceCreateCommand)command;
            IResourceMap resourceMap = ResourceUtil.GetResourceMap(_ResourceMaps, cmd.Type);
            Type? repositoryType = resourceMap.GetResourceModelRepositoryType();
            if(repositoryType != null)
            {
                Repository repository = ResourceUtil.GetModelRepository(_Repositories, resourceMap);

                var createMethod = repository.GetType().GetMethod("Create") ??
                    throw new NotImplementedException(
                        $"Method \"Create\" is not yet implemented for repository " +
                        $"\"{nameof(repository)}\"."
                    );

                object? createdModel = createMethod.Invoke(repository, [cmd.Model]) ??
                    throw new Exception("No value returned after creating a new resource.");

                result.SetValue(createdModel);

                _EventDispatcher.Dispatch(new ResourceCreatedEvent(
                    ResourceTypes.ProjectsProject, createdModel));
            }
            else
            {
                throw new NotImplementedException("The resourceMap for " +
                    $"resource type {cmd.Type} did not specify a resource model respository type.");
            }
        }
    }
}
