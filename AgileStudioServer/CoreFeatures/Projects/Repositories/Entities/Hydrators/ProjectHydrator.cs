using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Core.Repositories.Exceptions;
using AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemTypeSchemas;
using AgileStudioServer.CoreFeatures.Projects.Repositories.Entities;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServer.Data;

namespace AgileStudioServer.CoreFeatures.Projects.Repositories.Entities.Hydrators;

public class ProjectHydrator : AbstractEntityHydrator
{
    public ProjectHydrator(DBContext dBContext) : base(dBContext)
    {

    }

    public override bool Supports(Type from, Type to)
    {
        return (
            from == typeof(int) ||
            from == typeof(Services.Models.Project)
        ) && to == typeof(Project);
    }

    public override object Hydrate(object from, Type to, int maxDepth = 0, int depth = 0, IHydrator? referenceHydrator = null)
    {
        if (!Supports(from.GetType(), to))
        {
            throw new HydrationNotSupportedException(from.GetType(), to);
        }

        object? entity = null;

        if (from is Services.Models.Project)
        {
            var model = (Services.Models.Project)from;
            if (model.ID > 0)
            {
                entity = _DBContext.Project.Find(model.ID);
                if (entity == null)
                {
                    throw new EntityNotFoundException(
                        nameof(Project), model.ID.ToString());
                }
            }
            else
            {
                entity = new Project(model.Title, model.BacklogItemTypeSchemaID);
            }

            if (entity != null)
            {
                Hydrate(model, entity, maxDepth, depth, referenceHydrator);
            }
        }
        else if (from is int)
        {
            entity = _DBContext.Project.Find(from);
        }

        if (entity == null)
        {
            throw new HydrationFailedException(from.GetType(), to);
        }

        return entity;
    }

    public override void Hydrate(object from, object to, int maxDepth = 0, int depth = 0, IHydrator? referenceHydrator = null)
    {
        if (!Supports(from.GetType(), to.GetType()))
        {
            throw new HydrationNotSupportedException(from.GetType(), to.GetType());
        }

        var entity = (Project)to;
        int nextDepth = depth + 1;

        if (from is Services.Models.Project)
        {
            var model = (Services.Models.Project)from;

            entity.ID = model.ID;
            entity.Title = model.Title;
            entity.Description = model.Description;
            entity.CreatedOn = model.CreatedOn;
            entity.BacklogItemTypeSchemaID = model.BacklogItemTypeSchemaID;
            entity.CreatedByID = model.CreatedByID;

            if (referenceHydrator != null && nextDepth <= maxDepth)
            {
                entity.BacklogItemTypeSchema = (BacklogItemTypeSchema)referenceHydrator.Hydrate(
                    model.BacklogItemTypeSchemaID, typeof(BacklogItemTypeSchema), maxDepth, nextDepth
                );

                if (model.CreatedByID != null)
                {
                    entity.CreatedBy = (User)referenceHydrator.Hydrate(
                        model.CreatedByID, typeof(User), maxDepth, nextDepth
                    );
                }
            }
        }
    }
}
