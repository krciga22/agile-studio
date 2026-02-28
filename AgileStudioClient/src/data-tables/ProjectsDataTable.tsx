import React, {useContext, useEffect, useState, useCallback} from "react";
import type {ProjectDto} from "../services/api/dtos/ProjectDtos.tsx";
import DataTable, {type DataTableColumn} from "../components/data-table/DataTable";
import {DataTableContext} from "../components/data-table/DataTableContext.tsx";
import {DataTableFetcher} from "../components/data-table/DataTableFetcher.tsx";
import CurrentUserContext from "../services/CurrentUser.tsx";
import {debounce} from "../Utils.tsx";
import Constants from "../Constants.tsx";
import Pagination, {type PaginationDetails} from "../components/data-table/Pagination";
import Search from "../components/data-table/Search";
import Sort from "../components/data-table/Sort";

const INIT_STATUS_NOT_INITIALIZED = 'not_initialized';
const INIT_STATUS_INITIALIZED = 'initialized';

function ProjectsDataTable() {
  const [initializationStatus, setInitializationStatus] = useState(INIT_STATUS_NOT_INITIALIZED);
  const [isLoading, setIsLoading] = useState<boolean|undefined>();
  const [data, setData] = useState<ProjectDto[]>([]);
  const [searchQuery, setSearchQuery] = useState<string>('');
  const [filters] = useState<Record<string, unknown>>({});
  const [sort, setSort] = useState<string[]>([]);
  const [page, setPage] = useState<number>(1);
  const [paginationDetails, setPaginationDetails] = useState<PaginationDetails|null>(null);
  const [fetcher] = useState(new DataTableFetcher<ProjectDto>('/Project'));
  const currentUser = useContext(CurrentUserContext);
  const fetchDataTimeoutIdRef = React.useRef<ReturnType<typeof setTimeout> | null>(null);

  const _fetchData = useCallback(async (pageToFetch: number = 1) => {
    const fetchedData = await fetcher.fetchData(searchQuery, filters, sort, pageToFetch);
    setData(fetchedData);

    // TODO: this should come from the API, but for now we can just set it to a fixed value
    setPaginationDetails({
      pageSize: 10,
      currentPage: pageToFetch,
      totalPages: 5
    });
  }, [fetcher, searchQuery, filters, sort]);

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

  const doSearch = (searchQuery: string) => {
    setSearchQuery(searchQuery);
    setPage(1);
  };

  const doSort = (sort: string[]) => {
    setSort(sort);
    setPage(1);
  };

  useEffect(() => {
    if(initializationStatus === INIT_STATUS_INITIALIZED){
      fetchData(page);
    }
  }, [fetchData, initializationStatus, page]);

  const columns: DataTableColumn<ProjectDto>[] = [
    {
      key: 'id',
      field: 'id',
      header: 'ID',
      width: '10%'
    },
    {
      key: 'title',
      field: 'title',
      header: 'Title',
      width: '90%',
      sortable: true
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
    <div className={"DataTable ProjectsDataTable"}>
      <DataTableContext.Provider value={{
        data: data,
        searchQuery: searchQuery,
        filters: filters,
        sort: sort,
        paginationDetails: paginationDetails
      }}>
        <div className={"mb-3 d-flex align-items-start gap-2"}>
          <Search setSearchQuery={doSearch}></Search>
          <Sort sortableFields={sortableFields} setSort={doSort}></Sort>
        </div>

        <div className={"mb-3"}>
          <DataTable<ProjectDto>
            columns={columns}
            isLoading={isLoading}>
          </DataTable>
        </div>

        <div className={"d-flex justify-content-left"}>
          <Pagination setPage={async (page) => setPage(page) } />
        </div>
      </DataTableContext.Provider>
    </div>
  )
}

export default ProjectsDataTable
