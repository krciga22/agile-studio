using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Data;
using AgileStudioServer.Features.Resources.Resource;
using Microsoft.EntityFrameworkCore.Storage;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaEdges
{
    public class BacklogItemTypeSchemaEdgeService : AbstractModelService<BacklogItemTypeSchemaEdgeModel, int>
    {
        private readonly BacklogItemTypeSchemaEdgeRepository _BacklogItemTypeSchemaEdgeRepository;

        private readonly BacklogItemTypeSchemaEdgeValidator _BacklogItemTypeSchemaEdgeValidator;

        private readonly DBContext _DBContext;

        public BacklogItemTypeSchemaEdgeService(
            BacklogItemTypeSchemaEdgeRepository backlogItemTypeSchemaEdgeRepository,
            BacklogItemTypeSchemaEdgeValidator backlogItemTypeSchemaEdgeValidator,
            DBContext dBContext)
        {
            _BacklogItemTypeSchemaEdgeRepository = backlogItemTypeSchemaEdgeRepository;
            _BacklogItemTypeSchemaEdgeValidator = backlogItemTypeSchemaEdgeValidator;
            _DBContext = dBContext;
        }

        public override PaginationResults<BacklogItemTypeSchemaEdgeModel> GetCollection()
        {
            throw new NotImplementedException();
        }

        public override PaginationResults<BacklogItemTypeSchemaEdgeModel> GetSubCollection(string parentResourceType, object[] id)
        {
            switch (parentResourceType)
            {
                case ResourceTypes.AccountsBacklogItemTypeSchema:
                    return GetBySchemaId(int.Parse(id[0].ToString()!));
                default:
                    throw new ArgumentException($"Unsupported parent resource type: {parentResourceType}");
            }
        }

        public virtual List<BacklogItemTypeSchemaEdgeModel> GetByFromTypeId(int? fromTypeId, int schemaId = 0)
        {
            return _BacklogItemTypeSchemaEdgeRepository.GetByFromTypeId(fromTypeId, schemaId);
        }

        public virtual List<BacklogItemTypeSchemaEdgeModel> GetByToTypeId(int toTypeId, int schemaId = 0)
        {
            return _BacklogItemTypeSchemaEdgeRepository.GetByToTypeId(toTypeId, schemaId);
        }

        public virtual PaginationResults<BacklogItemTypeSchemaEdgeModel> GetBySchemaId(int schemaId)
        {
            return _BacklogItemTypeSchemaEdgeRepository.GetBySchemaId(schemaId);
        }

        /// <exception cref="ModelNotFoundException"></exception>
        public override BacklogItemTypeSchemaEdgeModel Get(int id)
        {
            return _BacklogItemTypeSchemaEdgeRepository.Get(id) ??
                throw new ModelNotFoundException(
                    nameof(BacklogItemTypeSchemaEdgeModel), id.ToString());
        }

        /// <exception cref="ModelNotFoundException"></exception>
        public virtual BacklogItemTypeSchemaEdgeModel GetByFromTypeToTypeAndSchema(int? fromTypeId, int toTypeId, int schemaId)
        {
            return _BacklogItemTypeSchemaEdgeRepository.Get(fromTypeId, toTypeId, schemaId) ??
                throw ModelNotFoundException.FromCompositeKey(
                    nameof(BacklogItemTypeSchemaEdgeModel),
                    [fromTypeId ?? 0, toTypeId, schemaId]);
        }

        public override int GetIdentifier(BacklogItemTypeSchemaEdgeModel model)
        {
            return model.ID;
        }

        /// <exception cref="InvalidOperationException"></exception>
        public override BacklogItemTypeSchemaEdgeModel Create(BacklogItemTypeSchemaEdgeModel edge) 
        {
            ArgumentNullException.ThrowIfNull(edge);

            var currentTx = _DBContext.Database.CurrentTransaction;
            IDbContextTransaction tx = currentTx ?? _DBContext.Database.BeginTransaction();
            bool createdTransaction = (currentTx == null);

            try
            {
                var existingEdge = _BacklogItemTypeSchemaEdgeRepository.Get(
                    edge.FromTypeID, edge.ToTypeID, edge.SchemaID);
                if (existingEdge != null) {
                    throw new InvalidOperationException("Edge already exists.");
                }

                _BacklogItemTypeSchemaEdgeValidator.ValidateSameAccount(edge.FromTypeID, edge.ToTypeID, edge.SchemaID);

                _BacklogItemTypeSchemaEdgeValidator.ValidateNoCycles(edge);

                var newEdgeModel = _BacklogItemTypeSchemaEdgeRepository.Create(edge);

                if (createdTransaction) {
                    tx.Commit();
                }

                return newEdgeModel;
            }
            catch
            {
                if (createdTransaction) {
                    try { tx.Rollback(); } 
                    catch { 
                        // todo log exception once a logger is setup
                    }
                }

                throw;
            }
            finally
            {
                if (createdTransaction) {
                    tx.Dispose();
                }
            }
        }

        public override BacklogItemTypeSchemaEdgeModel Update(BacklogItemTypeSchemaEdgeModel backlogItemTypeSchemaEdge)
        {
            return _BacklogItemTypeSchemaEdgeRepository.Update(backlogItemTypeSchemaEdge);
        }

        public override void Delete(BacklogItemTypeSchemaEdgeModel backlogItemTypeSchemaEdge)
        {
            _BacklogItemTypeSchemaEdgeRepository.Delete(backlogItemTypeSchemaEdge);
        }
    }
}
