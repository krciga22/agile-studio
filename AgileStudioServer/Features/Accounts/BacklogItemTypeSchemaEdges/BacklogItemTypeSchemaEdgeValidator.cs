using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Features.Accounts.BacklogItemTypes;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaEdges
{
    public class BacklogItemTypeSchemaEdgeValidator : AbstractService
    {
        private readonly BacklogItemTypeSchemaEdgeRepository _BacklogItemTypeSchemaEdgeRepository;

        private readonly BacklogItemTypeSchemaRepository _BacklogItemTypeSchemaRepository;

        private readonly BacklogItemTypeRepository _BacklogItemTypeRepository;

        public BacklogItemTypeSchemaEdgeValidator(
            BacklogItemTypeSchemaEdgeRepository backlogItemTypeSchemaEdgeRepository,
            BacklogItemTypeRepository backlogItemTypeRepository,
            BacklogItemTypeSchemaRepository backlogItemTypeSchemaRepository)
        {
            _BacklogItemTypeSchemaEdgeRepository = backlogItemTypeSchemaEdgeRepository;
            _BacklogItemTypeRepository = backlogItemTypeRepository;
            _BacklogItemTypeSchemaRepository = backlogItemTypeSchemaRepository;
        }

        /// <summary>
        /// Validates the FromType, ToType, and Schema of an edge 
        /// belong to the same account.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ModelNotFoundException"></exception>
        public void ValidateSameAccount(int? fromTypeId, int toTypeId, int schemaId)
        {
            BacklogItemTypeModel? fromType = null;
            if (fromTypeId != null){
                int fromTypeIdInt = (int)fromTypeId;
                fromType = _BacklogItemTypeRepository.Get(fromTypeIdInt) ??
                    throw new ModelNotFoundException(
                        nameof(BacklogItemTypeModel), fromTypeIdInt.ToString());
            }

            BacklogItemTypeModel toType = _BacklogItemTypeRepository.Get(toTypeId) ??
                throw new ModelNotFoundException(
                    nameof(BacklogItemTypeModel), toTypeId.ToString());

            BacklogItemTypeSchemaModel schema = _BacklogItemTypeSchemaRepository.Get(schemaId) ??
                throw new ModelNotFoundException(
                    nameof(BacklogItemTypeSchemaModel), schemaId.ToString());

            if ((fromType != null && fromType.AccountID != toType.AccountID) ||
                toType.AccountID != schema.AccountID)
            {
                throw new InvalidOperationException("Account mismatch between FromType, ToType, and Schema.");
            }
        }

        /// <summary>
        /// Validates that adding the given edge to the graph 
        /// does not create a cycle.
        /// </summary>
        /// <param name="edge"></param>
        /// <exception cref="Exception"></exception>
        public void ValidateNoCycles(BacklogItemTypeSchemaEdgeModel edge)
        {
            if (edge is null) throw new ArgumentNullException(nameof(edge));

            // reject self-cycle immediately
            if (edge.FromTypeID == edge.ToTypeID) {
                throw new InvalidOperationException("Self-cycle detected: FromTypeID equals ToTypeID.");
            }

            // Load all edges for the schema in one DB call to avoid N+1 queries
            var dbEdges = _BacklogItemTypeSchemaEdgeRepository.GetAllBySchemaId(edge.SchemaID)
                .Select(e => new { e.FromTypeID, e.ToTypeID })
                .ToList();

            // Include the new edge in the in-memory graph
            dbEdges.Add(new { edge.FromTypeID, edge.ToTypeID });

            // Build adjacency list
            var adjacency = new Dictionary<int, List<int>>();
            var nodes = new HashSet<int>();
            foreach (var e in dbEdges)
            {
                if(e.FromTypeID != null) {
                    int fromTypeId = (int) e.FromTypeID;
                    nodes.Add(fromTypeId);
                    if (!adjacency.TryGetValue(fromTypeId, out var list))
                    {
                        list = new List<int>();
                        adjacency[fromTypeId] = list;
                        list.Add(e.ToTypeID);
                    }
                }

                nodes.Add(e.ToTypeID);
            }

            // Colors: 0 = unvisited, 1 = visiting (on stack), 2 = visited
            var color = new Dictionary<int, int>();
            foreach (var n in nodes) color[n] = 0;

            bool HasCycle(int node)
            {
                color[node] = 1;
                if (adjacency.TryGetValue(node, out var neighbors))
                {
                    foreach (var nb in neighbors)
                    {
                        if (color[nb] == 1) return true;   // back edge -> cycle
                        if (color[nb] == 0 && HasCycle(nb)) return true;
                    }
                }
                color[node] = 2;
                return false;
            }

            foreach (var n in nodes)
            {
                if (color[n] == 0 && HasCycle(n))
                {
                    throw new InvalidOperationException("Cycle detected in backlog item type schema graph.");
                }
            }
        }
    }
}
