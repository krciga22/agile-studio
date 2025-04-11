using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.CoreFeatures.Projects.Projects
{
    public class ProjectModelHydrator : AbstractModelHydrator
    {
        public ProjectModelHydrator(DBContext dbContext) : base(dbContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(Project) ||
                from == typeof(ProjectPostDto) ||
                from == typeof(ProjectPatchDto)
            ) && to == typeof(ProjectModel);
        }

        public override object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupportedException(from.GetType(), to);
            }

            object? model = null;

            if (from is int)
            {
                var project = _DBContext.Project.Find(from);
                if (project != null)
                {
                    from = project;
                }
            }

            if (from is Project)
            {
                var entity = (Project)from;
                model = new ProjectModel(entity.Title, entity.BacklogItemTypeSchemaID, entity.BacklogItemLinkTypeSchemaID);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is ProjectPostDto)
            {
                var dto = (ProjectPostDto)from;
                model = new ProjectModel(dto.Title, dto.BacklogItemTypeSchemaId, dto.BacklogItemLinkTypeSchemaId);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is ProjectPatchDto)
            {
                var dto = (ProjectPatchDto)from;
                var entity = _DBContext.Project.Find(dto.ID);
                if (entity != null)
                {
                    model = Hydrate(entity, typeof(ProjectModel), maxDepth, depth, referenceHydrator);
                    Hydrate(dto, model, maxDepth, depth, referenceHydrator);
                }
            }

            if (model == null)
            {
                throw new HydrationFailedException(from.GetType(), to);
            }

            return model;
        }

        public override void Hydrate(object from, object to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to.GetType()))
            {
                throw new HydrationNotSupportedException(from.GetType(), to.GetType());
            }

            var model = (ProjectModel)to;

            if (from is Project)
            {
                var entity = (Project)from;

                model.ID = entity.ID;
                model.Title = entity.Title;
                model.Description = entity.Description;
                model.CreatedOn = entity.CreatedOn;
                model.BacklogItemTypeSchemaID = entity.BacklogItemTypeSchemaID;
                model.CreatedByID = entity.CreatedByID;
            }
            else if (from is ProjectPostDto)
            {
                var dto = (ProjectPostDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;
                model.BacklogItemTypeSchemaID = dto.BacklogItemTypeSchemaId;
            }
            else if (from is ProjectPatchDto)
            {
                var dto = (ProjectPatchDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;
            }
        }
    }
}
