import React from "react";
import type {DataTableItem} from "./DataTable.tsx";
import type {PaginationDetails} from "./Pagination.tsx";
import type {FilterValue} from "./filters/Filters.tsx";

type DataTableContextValue = {
  data: DataTableItem[];
  searchQuery: string;
  filters: Record<string, FilterValue>;
  sort: string[];
  paginationDetails: PaginationDetails|null;
}

export const DataTableContext = React.createContext<DataTableContextValue>({
  data: [],
  searchQuery: '',
  filters: {},
  sort: [],
  paginationDetails: null
});