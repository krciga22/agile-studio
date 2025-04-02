using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.CoreFeatures.Releases.Releases
{
    public class ReleaseModelHydrator : AbstractModelHydrator
    {
        public ReleaseModelHydrator(DBContext dbContext) : base(dbContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(Release) ||
                from == typeof(ReleasePostDto) ||
                from == typeof(ReleasePatchDto)
            ) && to == typeof(ReleaseModel);
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
                var release = _DBContext.Release.Find(from);
                if (release != null)
                {
                    from = release;
                }
            }

            if (from is Release)
            {
                var entity = (Release)from;
                model = new ReleaseModel(entity.Title, entity.ProjectID);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is ReleasePostDto)
            {
                var dto = (ReleasePostDto)from;
                model = new ReleaseModel(dto.Title, dto.ProjectId);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is ReleasePatchDto)
            {
                var dto = (ReleasePatchDto)from;
                var entity = _DBContext.Release.Find(dto.ID);
                if (entity != null)
                {
                    model = Hydrate(entity, typeof(ReleaseModel), maxDepth, depth, referenceHydrator);
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

            var model = (ReleaseModel)to;

            if (from is Release)
            {
                var entity = (Release)from;

                model.ID = entity.ID;
                model.Title = entity.Title;
                model.Description = entity.Description;
                model.CreatedOn = entity.CreatedOn;
                model.StartDate = entity.StartDate;
                model.EndDate = entity.EndDate;
                model.ProjectID = entity.ProjectID;
                model.CreatedByID = entity.CreatedByID;
            }
            else if (from is ReleasePostDto)
            {
                var dto = (ReleasePostDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;
                model.StartDate = dto.StartDate;
                model.EndDate = dto.EndDate;
                model.ProjectID = dto.ProjectId;
            }
            else if (from is ReleasePatchDto)
            {
                var dto = (ReleasePatchDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;
                model.StartDate = dto.StartDate;
                model.EndDate = dto.EndDate;
            }
        }
    }
}
