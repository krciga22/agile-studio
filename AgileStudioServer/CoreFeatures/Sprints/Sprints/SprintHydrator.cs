using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Core.Repositories.Exceptions;
using AgileStudioServer.CoreFeatures.Projects.Repositories.Entities;
using AgileStudioServer.CoreFeatures.Users.Repositories.Entities;
using AgileStudioServer.Data;

namespace AgileStudioServer.CoreFeatures.Sprints.Sprints;

public class SprintHydrator : AbstractEntityHydrator
{
    public SprintHydrator(DBContext dBContext) : base(dBContext)
    {

    }

    public override bool Supports(Type from, Type to)
    {
        return (
            from == typeof(int) ||
            from == typeof(SprintModel)
        ) && to == typeof(Sprint);
    }

    public override object Hydrate(object from, Type to, int maxDepth = 0, int depth = 0, IHydrator? referenceHydrator = null)
    {
        if (!Supports(from.GetType(), to))
        {
            throw new HydrationNotSupportedException(from.GetType(), to);
        }

        object? entity = null;

        if (from is SprintModel)
        {
            var model = (SprintModel)from;
            if (model.ID > 0)
            {
                entity = _DBContext.Sprint.Find(model.ID);
                if (entity == null)
                {
                    throw new EntityNotFoundException(
                        nameof(Sprint), model.ID.ToString());
                }
            }
            else
            {
                entity = new Sprint(model.SprintNumber, model.ProjectID);
            }

            if (entity != null)
            {
                Hydrate(model, entity, maxDepth, depth, referenceHydrator);
            }
        }
        else if (from is int)
        {
            entity = _DBContext.Sprint.Find(from);
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

        var entity = (Sprint)to;
        int nextDepth = depth + 1;

        if (from is SprintModel)
        {
            var model = (SprintModel)from;

            entity.ID = model.ID;
            entity.SprintNumber = model.SprintNumber;
            entity.Description = model.Description;
            entity.CreatedOn = model.CreatedOn;
            entity.StartDate = model.StartDate;
            entity.EndDate = model.EndDate;
            entity.ProjectID = model.ProjectID;
            entity.CreatedByID = model.CreatedByID;

            if (referenceHydrator != null && nextDepth <= maxDepth)
            {
                entity.Project = (Project)referenceHydrator.Hydrate(
                    model.ProjectID, typeof(Project), maxDepth, nextDepth
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
