using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypes
{
    public class BacklogItemLinkTypeModelHydrator : AbstractModelHydrator
    {
        public BacklogItemLinkTypeModelHydrator(DBContext dbContext) : base(dbContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(BacklogItemLinkType) ||
                from == typeof(BacklogItemLinkTypePostDto) ||
                from == typeof(BacklogItemLinkTypePatchDto)
            ) && to == typeof(BacklogItemLinkTypeModel);
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
                var backlogItemLinkType = _DBContext.BacklogItemLinkType.Find(from);
                if (backlogItemLinkType != null)
                {
                    from = backlogItemLinkType;
                }
            }

            if (from is BacklogItemLinkType)
            {
                var entity = (BacklogItemLinkType)from;
                model = new BacklogItemLinkTypeModel(entity.Title, entity.TitleOpposite);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is BacklogItemLinkTypePostDto)
            {
                var dto = (BacklogItemLinkTypePostDto)from;
                model = new BacklogItemLinkTypeModel(dto.Title, dto.TitleOpposite);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is BacklogItemLinkTypePatchDto)
            {
                var dto = (BacklogItemLinkTypePatchDto)from;
                var entity = _DBContext.BacklogItemLinkType.Find(dto.ID);
                if (entity != null)
                {
                    model = Hydrate(entity, typeof(BacklogItemLinkTypeModel), maxDepth, depth, referenceHydrator);
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

            var model = (BacklogItemLinkTypeModel)to;

            if (from is BacklogItemLinkType)
            {
                var entity = (BacklogItemLinkType)from;

                model.ID = entity.ID;
                model.Title = entity.Title;
                model.TitleOpposite = entity.TitleOpposite;
                model.Description = entity.Description;
                model.CreatedOn = entity.CreatedOn;
                model.CreatedByID = entity.CreatedByID;
            }
            else if (from is BacklogItemLinkTypePostDto)
            {
                var dto = (BacklogItemLinkTypePostDto)from;
                model.Title = dto.Title;
                model.TitleOpposite = dto.TitleOpposite;
                model.Description = dto.Description;
            }
            else if (from is BacklogItemLinkTypePatchDto)
            {
                var dto = (BacklogItemLinkTypePatchDto)from;
                model.Title = dto.Title;
                model.TitleOpposite = dto.TitleOpposite;
                model.Description = dto.Description;
            }
        }
    }
}
