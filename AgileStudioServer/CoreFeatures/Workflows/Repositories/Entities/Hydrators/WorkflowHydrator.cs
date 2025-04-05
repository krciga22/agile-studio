using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Core.Repositories.Exceptions;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServer.CoreFeatures.Workflows.Repositories.Entities;
using AgileStudioServer.Data;

namespace AgileStudioServer.CoreFeatures.Workflows.Repositories.Entities.Hydrators;

public class WorkflowHydrator : AbstractEntityHydrator
{
    public WorkflowHydrator(DBContext dBContext) : base(dBContext)
    {

    }

    public override bool Supports(Type from, Type to)
    {
        return (
            from == typeof(int) ||
            from == typeof(Services.Models.Workflow)
        ) && to == typeof(Workflow);
    }

    public override object Hydrate(object from, Type to, int maxDepth = 0, int depth = 0, IHydrator? referenceHydrator = null)
    {
        if (!Supports(from.GetType(), to))
        {
            throw new HydrationNotSupportedException(from.GetType(), to);
        }

        object? entity = null;

        if (from is Services.Models.Workflow)
        {
            var model = (Services.Models.Workflow)from;
            if (model.ID > 0)
            {
                entity = _DBContext.Workflow.Find(model.ID);
                if (entity == null)
                {
                    throw new EntityNotFoundException(
                        nameof(Workflow), model.ID.ToString());
                }
            }
            else
            {
                entity = new Workflow(model.Title);
            }

            if (entity != null)
            {
                Hydrate(model, entity, maxDepth, depth, referenceHydrator);
            }
        }
        else if (from is int)
        {
            entity = _DBContext.Workflow.Find(from);
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

        var entity = (Workflow)to;
        int nextDepth = depth + 1;

        if (from is Services.Models.Workflow)
        {
            var model = (Services.Models.Workflow)from;

            entity.ID = model.ID;
            entity.Title = model.Title;
            entity.Description = model.Description;
            entity.CreatedOn = model.CreatedOn;
            entity.CreatedByID = model.CreatedById;

            if (referenceHydrator != null && nextDepth <= maxDepth)
            {
                if (model.CreatedById != null)
                {
                    entity.CreatedBy = (User)referenceHydrator.Hydrate(
                        model.CreatedById, typeof(User), maxDepth, nextDepth
                    );
                }
            }
        }
    }
}
