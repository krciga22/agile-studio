import React, {useCallback, useContext, useEffect, useState} from "react";
import type {BacklogItemTypeSchemaNodeDto} from "../../api/dtos/accounts/BacklogItemTypeSchemaNodeDtos.tsx";
import DataTable, {type DataTableColumn} from "../../components/data-table/DataTable.tsx";
import {DataTableContext} from "../../components/data-table/DataTableContext.tsx";
import CurrentUserContext from "../../services/CurrentUser.tsx";
import {debounce} from "../../Utils.tsx";
import Constants from "../../Constants.tsx";
import Pagination, {type PaginationDetails} from "../../components/data-table/Pagination.tsx";
import Search from "../../components/data-table/Search.tsx";
import Sort from "../../components/data-table/Sort.tsx";
import DateTimeText from "../../components/date/DateTimeText.tsx";
import {getBacklogItemTypeSchemaNodes} from "../../api/endpoints/accounts/BacklogItemTypeSchemas.tsx";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faPlus} from "@fortawesome/free-solid-svg-icons";
import CreateBacklogItemTypeSchemaNodeModal from "../../modals/account/CreateBacklogItemTypeSchemaNodeModal.tsx";
import type {UserSummaryDto} from "../../api/dtos/UserDtos.tsx";

type BacklogItemTypeSchemaNodesDataTableProps = {
  backlogItemTypeSchemaId: number;
}

function BacklogItemTypeSchemaNodesDataTable(props: BacklogItemTypeSchemaNodesDataTableProps) {
  const {backlogItemTypeSchemaId} = props;
  const [initializationStatus, setInitializationStatus] = useState('not_initialized');
  const [isLoading, setIsLoading] = useState<boolean|undefined>();
  const [data, setData] = useState<BacklogItemTypeSchemaNodeDto[]>([]);
  const [isCreateModalOpen, setIsCreateModalOpen] = useState(false);
  const [searchQuery, setSearchQuery] = useState<string>('');
  const [sort, setSort] = useState<string[]>([]);
  const [page, setPage] = useState<number>(1);
  const [paginationDetails, setPaginationDetails] = useState<PaginationDetails|null>(null);
  const currentUser = useContext(CurrentUserContext);
  const fetchDataTimeoutIdRef = React.useRef<ReturnType<typeof setTimeout> | null>(null);

  const _fetchData = useCallback(async (pageToFetch: number = 1) => {
    const fetchedData = await getBacklogItemTypeSchemaNodes(backlogItemTypeSchemaId, pageToFetch, searchQuery, sort);
    setData(fetchedData.data.items);

    setPaginationDetails({
      pageSize: fetchedData.data.items.length,
      currentPage: fetchedData.data.page,
      totalPages: fetchedData.data.totalPages
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

  const renderUser = (createdBy: UserSummaryDto) => {
    return createdBy ? `${createdBy.firstName ?? ''} ${createdBy.lastName ?? ''}`.trim() : '--';
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
      width: '42%',
      render: (item: BacklogItemTypeSchemaNodeDto) => item.backlogItemType.title ?? `#${item.backlogItemType.id}`
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
