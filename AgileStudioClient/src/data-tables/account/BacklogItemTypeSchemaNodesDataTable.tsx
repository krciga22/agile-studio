import React, {useCallback, useContext, useEffect, useState} from "react";
import type {BacklogItemTypeSchemaNodeDto} from "../../api/dtos/accounts/BacklogItemTypeSchemaNodeDtos.tsx";
import type {BacklogItemTypeSchemaEdgeDto} from "../../api/dtos/accounts/BacklogItemTypeSchemaEdgeDtos.tsx";
import DataTable, {type DataTableColumn} from "../../components/data-table/DataTable.tsx";
import {DataTableContext} from "../../components/data-table/DataTableContext.tsx";
import CurrentUserContext from "../../services/CurrentUser.tsx";
import {debounce} from "../../Utils.tsx";
import Constants from "../../Constants.tsx";
import Pagination, {type PaginationDetails} from "../../components/data-table/Pagination.tsx";
import Search from "../../components/data-table/Search.tsx";
import Sort from "../../components/data-table/Sort.tsx";
import DateTimeText from "../../components/date/DateTimeText.tsx";
import {getBacklogItemTypeSchemaEdges, getBacklogItemTypeSchemaNodes} from "../../api/endpoints/accounts/BacklogItemTypeSchemas.tsx";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faEllipsisVertical, faPlus} from "@fortawesome/free-solid-svg-icons";
import CreateBacklogItemTypeSchemaNodeModal from "../../modals/account/CreateBacklogItemTypeSchemaNodeModal.tsx";
import CreateBacklogItemTypeSchemaEdgeModal from "../../modals/account/CreateBacklogItemTypeSchemaEdgeModal.tsx";
import ConfirmDeleteModal from "../../modals/ConfirmDeleteModal.tsx";
import type {UserSummaryDto} from "../../api/dtos/UserDtos.tsx";
import {deleteBacklogItemTypeSchemaNode} from "../../api/endpoints/accounts/BacklogItemTypeSchemaNodes.tsx";
import {deleteBacklogItemTypeSchemaEdge} from "../../api/endpoints/accounts/BacklogItemTypeSchemaEdges.tsx";
import {paginatedResultsToArray, toSortString} from "../../api/api-utils.tsx";

type BacklogItemTypeSchemaNodesDataTableProps = {
  backlogItemTypeSchemaId: number;
}

function BacklogItemTypeSchemaNodesDataTable(props: BacklogItemTypeSchemaNodesDataTableProps) {
  const {backlogItemTypeSchemaId} = props;
  const [initializationStatus, setInitializationStatus] = useState('not_initialized');
  const [isLoading, setIsLoading] = useState<boolean|undefined>();
  const [data, setData] = useState<BacklogItemTypeSchemaNodeDto[]>([]);
  const [edges, setEdges] = useState<BacklogItemTypeSchemaEdgeDto[]>([]);
  const [isCreateModalOpen, setIsCreateModalOpen] = useState(false);
  const [isEdgeModalOpen, setIsEdgeModalOpen] = useState(false);
  const [edgeModalMode, setEdgeModalMode] = useState<'from'|'to'>('from');
  const [edgeModalTargetTypeID, setEdgeModalTargetTypeID] = useState<number|undefined>();
  const [deletingNode, setDeletingNode] = useState<BacklogItemTypeSchemaNodeDto|undefined>();
  const [deletingEdge, setDeletingEdge] = useState<BacklogItemTypeSchemaEdgeDto|undefined>();
  const [openActionsMenuId, setOpenActionsMenuId] = useState<number|undefined>();
  const [searchQuery, setSearchQuery] = useState<string>('');
  const [sort, setSort] = useState<string[]>([]);
  const [page, setPage] = useState<number>(1);
  const [paginationDetails, setPaginationDetails] = useState<PaginationDetails|null>(null);
  const currentUser = useContext(CurrentUserContext);
  const fetchDataTimeoutIdRef = React.useRef<ReturnType<typeof setTimeout> | null>(null);

  const _fetchData = useCallback(async (pageToFetch: number = 1) => {
    const [nodesResponse, allEdges] = await Promise.all([
      getBacklogItemTypeSchemaNodes(backlogItemTypeSchemaId,
        {page: pageToFetch, searchQuery, sort: toSortString(sort)}),
      paginatedResultsToArray(page =>
        getBacklogItemTypeSchemaEdges(backlogItemTypeSchemaId, {page}))
    ]);

    setData(nodesResponse.data.items);
    setEdges(allEdges);

    setPaginationDetails({
      pageSize: nodesResponse.data.items.length,
      currentPage: nodesResponse.data.page,
      totalPages: nodesResponse.data.totalPages
    });
  }, [backlogItemTypeSchemaId, searchQuery, sort]);

  const fetchData = useCallback((pageToFetch: number = 1) => {
    return new Promise((resolve, reject) => {
      setIsLoading(true);

      debounce(
        async () => {
          try {
            const fetchedData = await _fetchData(pageToFetch);
            resolve(fetchedData);
          }
          catch(error){
            reject(error);
          }
          finally {
            setIsLoading(false);
          }
        },
        Constants.EXTRA_WAIT_TIME_MS,
        fetchDataTimeoutIdRef
      );
    });
  }, [_fetchData]);

  const doSearch = (newSearchQuery: string) => {
    setSearchQuery(newSearchQuery);
    setPage(1);
  };

  const doSort = (newSort: string[]) => {
    setSort(newSort);
    setPage(1);
  };

  useEffect(() => {
    if(initializationStatus === 'initialized'){
      fetchData(page);
    }
  }, [fetchData, initializationStatus, page]);

  useEffect(() => {
    setPage(1);
  }, [backlogItemTypeSchemaId]);

  useEffect(() => {
    const closeActionsMenu = () => {
      setOpenActionsMenuId(undefined);
    };

    window.addEventListener('mousedown', closeActionsMenu);

    return () => {
      window.removeEventListener('mousedown', closeActionsMenu);
    };
  }, []);

  const addEdge = (edgeType:React.SetStateAction<"from"|"to">, targetTypeID:number) => {
    setEdgeModalMode(edgeType);
    setEdgeModalTargetTypeID(targetTypeID);
    setIsEdgeModalOpen(true);
  };

  const renderUser = (createdBy: UserSummaryDto) => {
    return createdBy ? `${createdBy.firstName ?? ''} ${createdBy.lastName ?? ''}`.trim() : '--';
  };

  const renderFromTypes = (item: BacklogItemTypeSchemaNodeDto) => {
    const fromEdges = edges.filter(edge => edge.toType.id === item.backlogItemType.id);

    return (
      <div>
        {fromEdges.map(edge => (
          <span key={edge.id} className="badge bg-light text-dark me-1">
            {(edge.fromType ? (edge.fromType.title ?? `#${edge.fromType.id}`) : '[start]')}
            <button
              type="button"
              className="btn btn-sm text-dark p-0 ms-2"
              aria-label="Remove from type"
              onClick={(ev) => { ev.stopPropagation(); setDeletingEdge(edge); }}
            >
              ×
            </button>
          </span>
        ))}
        <button
          type="button"
          className="btn btn-sm btn-outline-secondary"
          aria-label="Add from type"
          onClick={() => { addEdge('from', item.backlogItemType.id); }}
        >
          <FontAwesomeIcon icon={faPlus} />
        </button>
      </div>
    );
  };

  const renderActionsMenu = (item: BacklogItemTypeSchemaNodeDto) => {
    const isOpen = openActionsMenuId === item.id;
    return (
      <div className="dropstart">
        <button
          type="button"
          className="btn btn-sm btn-outline-secondary"
          data-bs-toggle="dropdown"
          aria-label="Backlog item type schema node actions"
          aria-expanded={isOpen}
          onClick={(e) => {
            e.stopPropagation();
            setOpenActionsMenuId(isOpen ? undefined : item.id);
          }}
        >
          <FontAwesomeIcon icon={faEllipsisVertical} />
        </button>
        <div className="dropdown" onMouseDown={e => e.stopPropagation()}>
          <ul className={`dropdown-menu dropdown-menu-end ${isOpen ? 'show' : ''}`}>
            <li>
              <button
                type="button"
                className="dropdown-item"
                onClick={() => {
                  setOpenActionsMenuId(undefined);
                  setDeletingNode(item);
                }}
              >
                Delete
              </button>
            </li>
          </ul>
        </div>
      </div>
    );
  };

  const columns: DataTableColumn<BacklogItemTypeSchemaNodeDto>[] = [
    {
      key: 'id',
      field: 'id',
      header: 'ID',
      width: '8%',
      sortable: true
    },
    {
      key: 'backlogItemType',
      header: 'Type',
      render: (item: BacklogItemTypeSchemaNodeDto) => item.backlogItemType.title ?? `#${item.backlogItemType.id}`
    },
    {
      key: 'fromTypes',
      header: 'From Types',
      render: renderFromTypes
    },
    {
      key: 'createdBy',
      header: 'Created By',
      width: '20%',
      render: (item: BacklogItemTypeSchemaNodeDto) => renderUser(item.createdBy)
    },
    {
      key: 'createdOn',
      field: 'createdOn',
      header: 'Created On',
      width: '20%',
      sortable: true,
      render: (item: BacklogItemTypeSchemaNodeDto) => <DateTimeText date={item.createdOn} />
    },
    {
      key: 'actions',
      header: '',
      width: '54px',
      render: renderActionsMenu
    }
  ];

  const sortableFields = columns.filter(c => c?.sortable)
    .map(c => ({
      fieldKey: c.key,
      label: typeof c.header === 'string' ? c.header : String(c.key)
    }));

  if(initializationStatus === 'not_initialized' &&
    !currentUser.isLoading && currentUser.user){
    setInitializationStatus('initialized');
  }

  return (
    <div className={"DataTable BacklogItemTypeSchemaNodesDataTable"}>
      <DataTableContext.Provider value={{
        data: data,
        searchQuery: searchQuery,
        filters: {},
        sort: sort,
        paginationDetails: paginationDetails
      }}>
        <div className={"mb-3 d-flex justify-content-between align-items-center"}>
          <div className={"d-flex align-items-start gap-2"}>
            <Search setSearchQuery={doSearch}></Search>
            <Sort sortableFields={sortableFields} setSort={doSort}></Sort>
          </div>
          <div className={"d-flex align-items-start gap-2"}>
            <button
              type="button"
              className="btn btn-primary"
              onClick={() => setIsCreateModalOpen(true)}
            >
              <FontAwesomeIcon icon={faPlus} />
            </button>
          </div>
        </div>

        <CreateBacklogItemTypeSchemaNodeModal
          isOpen={isCreateModalOpen}
          backlogItemTypeSchemaID={backlogItemTypeSchemaId}
          onClose={() => setIsCreateModalOpen(false)}
          onCreated={() => {
            setIsCreateModalOpen(false);
            fetchData(page);
          }}
        />

        <CreateBacklogItemTypeSchemaEdgeModal
          isOpen={isEdgeModalOpen}
          mode={edgeModalMode}
          backlogItemTypeSchemaID={backlogItemTypeSchemaId}
          targetBacklogItemTypeID={edgeModalTargetTypeID}
          onClose={() => setIsEdgeModalOpen(false)}
          onCreated={() => {
            setIsEdgeModalOpen(false);
            fetchData(page);
          }}
        />

        <ConfirmDeleteModal
          resourceType={"Type"}
          resourceTitle={
            deletingNode
              ? deletingNode.backlogItemType.title ?? `#${deletingNode.backlogItemType.id}`
              : undefined
          }
          resourceID={deletingNode?.id}
          deleteEndpoint={deleteBacklogItemTypeSchemaNode}
          onCancel={() => {
            setDeletingNode(undefined);
          }}
          onDeleteSuccess={async () => {
            setDeletingNode(undefined);
            await fetchData(page);
          }}
        />

        <ConfirmDeleteModal
          resourceType={"Type Edge"}
          resourceTitle={
            deletingEdge
              ? `${deletingEdge.fromType ? (deletingEdge.fromType.title ?? `#${deletingEdge.fromType.id}`) : '[start]'} → ${deletingEdge.toType.title ?? `#${deletingEdge.toType.id}`}`
              : undefined
          }
          resourceID={deletingEdge?.id}
          deleteEndpoint={deleteBacklogItemTypeSchemaEdge}
          onCancel={() => {
            setDeletingEdge(undefined);
          }}
          onDeleteSuccess={async () => {
            setDeletingEdge(undefined);
            await fetchData(page);
          }}
        />

        <div className={"mb-3"}>
          <DataTable<BacklogItemTypeSchemaNodeDto>
            columns={columns}
            isLoading={isLoading}>
          </DataTable>
        </div>

        <div className={"d-flex justify-content-left"}>
          <Pagination setPage={async (nextPage) => setPage(nextPage) } />
        </div>
      </DataTableContext.Provider>
    </div>
  )
}

export default BacklogItemTypeSchemaNodesDataTable
