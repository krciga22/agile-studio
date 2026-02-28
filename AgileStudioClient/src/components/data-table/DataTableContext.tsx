import React from "react";
import type {DataTableItem} from "./DataTable.tsx";
import type {PaginationDetails} from "./Pagination.tsx";

type DataTableContextValue = {
  data: DataTableItem[];
  searchQuery: string;
  filters: Record<string, unknown>;
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