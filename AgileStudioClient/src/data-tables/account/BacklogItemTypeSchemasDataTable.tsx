import React, {useCallback, useContext, useEffect, useMemo, useState} from "react";
import type {BacklogItemTypeSchemaDto} from "../../api/dtos/accounts/BacklogItemTypeSchemaDtos.tsx";
import DataTable, {type DataTableColumn} from "../../components/data-table/DataTable.tsx";
import {DataTableContext} from "../../components/data-table/DataTableContext.tsx";
import {DataTableFetcher} from "../../components/data-table/DataTableFetcher.tsx";
import CurrentUserContext from "../../services/CurrentUser.tsx";
import {debounce} from "../../Utils.tsx";
import Constants from "../../Constants.tsx";
import Pagination, {type PaginationDetails} from "../../components/data-table/Pagination.tsx";
import Search from "../../components/data-table/Search.tsx";
import Sort from "../../components/data-table/Sort.tsx";
import DateTimeText from "../../components/date/DateTimeText.tsx";
import {baseUrl as accountsEndpoint} from "../../api/endpoints/accounts/Accounts.tsx";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faEllipsisVertical, faPlus} from "@fortawesome/free-solid-svg-icons";
import CreateBacklogItemTypeSchemaModal from "../../modals/account/CreateBacklogItemTypeSchemaModal.tsx";
import ConfirmDeleteModal from "../../modals/ConfirmDeleteModal.tsx";
import {deleteBacklogItemTypeSchema} from "../../api/endpoints/accounts/BacklogItemTypeSchemas.tsx";
import {getAccountBacklogItemTypeSchemaPagePath} from "../../PageRoutes.tsx";
import {goToPage, linkToPage} from "../../PageRouterUtils.tsx";

const INIT_STATUS_NOT_INITIALIZED = 'not_initialized';
const INIT_STATUS_INITIALIZED = 'initialized';

type BacklogItemTypeSchemasDataTableProps = {
  accountId: number;
}

function BacklogItemTypeSchemasDataTable(props: BacklogItemTypeSchemasDataTableProps) {
  const {accountId} = props;
  const [initializationStatus, setInitializationStatus] = useState(INIT_STATUS_NOT_INITIALIZED);
  const [isLoading, setIsLoading] = useState<boolean|undefined>();
  const [data, setData] = useState<BacklogItemTypeSchemaDto[]>([]);
  const [isCreateModalOpen, setIsCreateModalOpen] = useState(false);
  const [deletingSchema, setDeletingSchema] = useState<BacklogItemTypeSchemaDto|undefined>();
  const [openActionsMenuId, setOpenActionsMenuId] = useState<number|undefined>();
  const [searchQuery, setSearchQuery] = useState<string>('');
  const [sort, setSort] = useState<string[]>([]);
  const [page, setPage] = useState<number>(1);
  const [paginationDetails, setPaginationDetails] = useState<PaginationDetails|null>(null);
  const endpoint = useMemo(() => {
    return `${accountsEndpoint}/${accountId}/BacklogItemTypeSchemas`;
  }, [accountId]);
  const fetcher = useMemo(() => new DataTableFetcher<BacklogItemTypeSchemaDto>(endpoint), [endpoint]);
  const currentUser = useContext(CurrentUserContext);
  const fetchDataTimeoutIdRef = React.useRef<ReturnType<typeof setTimeout> | null>(null);

  const _fetchData = useCallback(async (pageToFetch: number = 1) => {
    const fetchedData = await fetcher.fetchPaginatedData(searchQuery, {}, sort, pageToFetch);
    setData(fetchedData.items);

    setPaginationDetails({
      pageSize: fetchedData.items.length,
      currentPage: fetchedData.page,
      totalPages: fetchedData.totalPages
    });
  }, [fetcher, searchQuery, sort]);

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
    if(initializationStatus === INIT_STATUS_INITIALIZED){
      fetchData(page);
    }
  }, [fetchData, initializationStatus, page]);

  useEffect(() => {
    setPage(1);
  }, [accountId]);

  useEffect(() => {
    const closeActionsMenu = () => {
      setOpenActionsMenuId(undefined);
    };

    window.addEventListener('mousedown', closeActionsMenu);

    return () => {
      window.removeEventListener('mousedown', closeActionsMenu);
    };
  }, []);

  const renderActionsMenu = (item: BacklogItemTypeSchemaDto) => {
    const isOpen = openActionsMenuId === item.id;
    return (
      <div className="dropstart">
        <button
          type="button"
          className="btn btn-sm btn-outline-secondary"
          data-bs-toggle="dropdown"
          aria-label="Backlog item type schema actions"
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
                  goToPage(getAccountBacklogItemTypeSchemaPagePath(accountId, item.id));
                }}
              >
                Edit
              </button>
            </li>
            <li>
              <button
                type="button"
                className="dropdown-item"
                onClick={() => {
                  setOpenActionsMenuId(undefined);
                  setDeletingSchema(item);
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

  const columns: DataTableColumn<BacklogItemTypeSchemaDto>[] = [
    {
      key: 'id',
      field: 'id',
      header: 'ID',
      width: '10%',
      sortable: true
    },
    {
      key: 'title',
      field: 'title',
      header: 'Title',
      width: '65%',
      sortable: true,
      render: (schema: BacklogItemTypeSchemaDto) => {
        const pagePath = getAccountBacklogItemTypeSchemaPagePath(accountId, schema.id);
        return (
          <a href={pagePath} onClick={linkToPage}>
            {schema.title}
          </a>
        );
      }
    },
    {
      key: 'createdOn',
      field: 'createdOn',
      header: 'Created On',
      width: '25%',
      sortable: true,
      render: (item: BacklogItemTypeSchemaDto) => {
        return <DateTimeText date={item.createdOn} />
      }
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

  if(initializationStatus === INIT_STATUS_NOT_INITIALIZED &&
    !currentUser.isLoading && currentUser.user){
    setInitializationStatus(INIT_STATUS_INITIALIZED);
  }

  return (
    <div className={"DataTable BacklogItemTypeSchemasDataTable"}>
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

        <CreateBacklogItemTypeSchemaModal
          isOpen={isCreateModalOpen}
          accountID={accountId}
          onClose={() => setIsCreateModalOpen(false)}
          onCreated={() => {
            setIsCreateModalOpen(false);
            fetchData(page);
          }}
        />

        <ConfirmDeleteModal
          resourceType={"Backlog Item Type Schema"}
          resourceTitle={deletingSchema?.title}
          resourceID={deletingSchema?.id}
          deleteEndpoint={deleteBacklogItemTypeSchema}
          onCancel={() => {
            setDeletingSchema(undefined);
          }}
          onDeleteSuccess={async () => {
            setDeletingSchema(undefined);
            await fetchData(page);
          }}
        />

        <div className={"mb-3"}>
          <DataTable<BacklogItemTypeSchemaDto>
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

export default BacklogItemTypeSchemasDataTable
