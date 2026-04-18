using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.CoreFeatures.Projects.Sprints
{
    public class SprintModelHydrator : AbstractModelHydrator
    {
        public SprintModelHydrator(DBContext dbContext) : base(dbContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(Sprint) ||
                from == typeof(SprintPostDto) ||
                from == typeof(SprintPatchDto)
            ) && to == typeof(SprintModel);
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
                var sprint = _DBContext.Sprint.Find(from);
                if (sprint != null)
                {
                    from = sprint;
                }
            }

            if (from is Sprint)
            {
                var entity = (Sprint)from;
                model = new SprintModel(entity.SprintNumber, entity.ProjectID);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is SprintPostDto)
            {
                var dto = (SprintPostDto)from;
                model = new SprintModel(0, dto.ProjectId); // todo fix hard coded sprint number
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is SprintPatchDto)
            {
                var dto = (SprintPatchDto)from;
                var entity = _DBContext.Sprint.Find(dto.ID);
                if (entity != null)
                {
                    model = Hydrate(entity, typeof(SprintModel), maxDepth, depth, referenceHydrator);
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

            var model = (SprintModel)to;

            if (from is Sprint)
            {
                var entity = (Sprint)from;
                model.ID = entity.ID;
                model.SprintNumber = entity.SprintNumber;
                model.Description = entity.Description;
                model.CreatedOn = entity.CreatedOn;
                model.StartDate = entity.StartDate;
                model.EndDate = entity.EndDate;
                model.ProjectID = entity.ProjectID;
                model.CreatedByID = entity.CreatedByID;
            }
            else if (from is SprintPostDto)
            {
                var dto = (SprintPostDto)from;
                model.Description = dto.Description;
                model.StartDate = dto.StartDate;
                model.EndDate = dto.EndDate;
                model.ProjectID = dto.ProjectId;
            }
            else if (from is SprintPatchDto)
            {
                var dto = (SprintPatchDto)from;
                model.Description = dto.Description;
                model.StartDate = dto.StartDate;
                model.EndDate = dto.EndDate;
            }
        }
    }
}
