import React, {useContext, useEffect, useState} from "react";
import type {ProjectDto} from "../services/api/dtos/ProjectDtos.tsx";
import DataTable, {type DataTableColumn} from "../components/data-table/DataTable";
import {DataTableContext} from "../components/data-table/DataTableContext.tsx";
import {DataTableFetcher} from "../components/data-table/DataTableFetcher.tsx";
import CurrentUserContext from "../services/CurrentUser.tsx";
import {debounce} from "../Utils.tsx";
import Constants from "../Constants.tsx";

const INIT_STATUS_NOT_INITIALIZED = 'not_initialized';
const INIT_STATUS_INITIALIZING = 'initializing';
const INIT_STATUS_INITIALIZED = 'initialized';

function ProjectsDataTable() {
  const [initializationStatus, setInitializationStatus] = useState(INIT_STATUS_NOT_INITIALIZED);
  const [isLoading, setIsLoading] = useState<boolean|undefined>();
  const [data, setData] = useState<ProjectDto[]>([]);
  const [filters, setFilters] = useState<Record<string, unknown>>({});
  const [sort, setSort] = useState<string[]>([]);
  const [paginationDetails, setPaginationDetails] = useState<object|null>(null);
  const [page] = useState<number>(1);
  const [fetcher] = useState(new DataTableFetcher<ProjectDto>('/Project'));
  const currentUser = useContext(CurrentUserContext);
  const fetchDataTimeoutIdRef = React.useRef<ReturnType<typeof setTimeout> | null>(null);

  useEffect(() => {
    if(initializationStatus === INIT_STATUS_INITIALIZED){
      fetchData();
    }
  }, []);

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
      width: '90%'
    }
  ];

  const fetchData = () => {
    return new Promise((resolve, reject) => {
      setIsLoading(true);

      debounce(
        async () => {
          try {
            const fetchedData = await _fetchData();
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
  }

  const _fetchData = async () => {
    const fetchedData = await fetcher.fetchData(filters, sort, page);
    setData(fetchedData);
  }

  if(initializationStatus === INIT_STATUS_NOT_INITIALIZED &&
    !currentUser.isLoading && currentUser.user){
    setInitializationStatus(INIT_STATUS_INITIALIZING);

    fetchData()
      .finally(() => {
        setInitializationStatus(INIT_STATUS_INITIALIZED);
      });
  }

  return (
    <div className={"DataTable ProjectsDataTable"}>
      <DataTableContext value={{
        data: data,
        filters: filters,
        sort: sort,
        paginationDetails: paginationDetails,
        setData: (data) => {
          setData(data as ProjectDto[]);
        },
        setFilters: setFilters,
        setSort: setSort,
        setPaginationDetails: setPaginationDetails
      }}>
        <DataTable<ProjectDto> columns={columns} isLoading={isLoading}></DataTable>
      </DataTableContext>
    </div>
  )
}

export default ProjectsDataTable
