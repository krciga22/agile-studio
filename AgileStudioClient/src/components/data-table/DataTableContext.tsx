import React from "react";
import type {DataTableItem} from "./DataTable.tsx";
import type {PaginationDetails} from "./Pagination.tsx";

type DataTableContextValue = {
  data: DataTableItem[];
  filters: Record<string, unknown>;
  sort: string[];
  paginationDetails: PaginationDetails|null;
  setData: (data: DataTableItem[]) => void;
  setFilters: (filters: Record<string, unknown>) => void;
  setSort: (sort: string[]) => void;
  setPaginationDetails: (paginationDetails: PaginationDetails) => void;
}

export const DataTableContext = React.createContext<DataTableContextValue>({
  data: [],
  filters: {},
  sort: [],
  paginationDetails: null,
  setData: () => {},
  setFilters: () => {},
  setSort: () => {},
  setPaginationDetails: () => {}
});