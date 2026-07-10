import React, {useCallback, useContext, useEffect, useState} from "react";
import type {AccountDto} from "../api/dtos/accounts/AccountDtos.tsx";
import DataTable, {type DataTableColumn} from "../components/data-table/DataTable";
import {DataTableContext} from "../components/data-table/DataTableContext.tsx";
import {DataTableFetcher} from "../components/data-table/DataTableFetcher.tsx";
import CurrentUserContext from "../services/CurrentUser.tsx";
import {debounce} from "../Utils.tsx";
import Constants from "../Constants.tsx";
import Pagination, {type PaginationDetails} from "../components/data-table/Pagination";
import Search from "../components/data-table/Search";
import Sort from "../components/data-table/Sort";
import {baseUrl as accountsEndpoint} from "../api/endpoints/accounts/Accounts.tsx";
import {getAccountTitle} from "../util/account-utils.tsx";
import DateTimeText from "../components/date/DateTimeText.tsx";
import {getAccountHomePagePath} from "../PageRoutes.tsx";
import {linkToPage} from "../PageRouterUtils.tsx";

const INIT_STATUS_NOT_INITIALIZED = 'not_initialized';
const INIT_STATUS_INITIALIZED = 'initialized';

function AccountsDataTable() {
  const [initializationStatus, setInitializationStatus] = useState(INIT_STATUS_NOT_INITIALIZED);
  const [isLoading, setIsLoading] = useState<boolean|undefined>();
  const [data, setData] = useState<AccountDto[]>([]);
  const [searchQuery, setSearchQuery] = useState<string>('');
  const [sort, setSort] = useState<string[]>([]);
  const [page, setPage] = useState<number>(1);
  const [paginationDetails, setPaginationDetails] = useState<PaginationDetails|null>(null);
  const [fetcher] = useState(new DataTableFetcher<AccountDto>(accountsEndpoint));
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

  const columns: DataTableColumn<AccountDto>[] = [
    {
      key: 'id',
      field: 'id',
      header: 'ID',
      width: '10%',
      sortable: true
    },
    {
      key: 'accountType',
      header: 'Account',
      width: '60%',
      render: (account: AccountDto) => {
        const accountHomePath = getAccountHomePagePath(account.id);
        return (
          <a href={accountHomePath} onClick={linkToPage}>
            {getAccountTitle(account)}
          </a>
        );
      }
    },
    {
      key: 'createdOn',
      field: 'createdOn',
      header: 'Created On',
      width: '30%',
      sortable: true,
      render: (account: AccountDto) => {
        return <DateTimeText date={account.createdOn} />
      }
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
    <div className={"DataTable AccountsDataTable"}>
      <DataTableContext.Provider value={{
        data: data,
        searchQuery: searchQuery,
        filters: {},
        sort: sort,
        paginationDetails: paginationDetails
      }}>
        <div className={"mb-3 d-flex align-items-start gap-2"}>
          <Search setSearchQuery={doSearch}></Search>
          <Sort sortableFields={sortableFields} setSort={doSort}></Sort>
        </div>

        <div className={"mb-3"}>
          <DataTable<AccountDto>
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

export default AccountsDataTable
