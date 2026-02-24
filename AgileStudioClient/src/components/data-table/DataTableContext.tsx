import React from "react";
import type {DataTableItem} from "./DataTable.tsx";

type DataTableContextValue = {
  data: DataTableItem[];
  filters: Record<string, unknown>;
  sort: string[];
  paginationDetails: object|null;
  setData: (data: DataTableItem[]) => void;
  setFilters: (filters: Record<string, unknown>) => void;
  setSort: (sort: string[]) => void;
  setPaginationDetails: (paginationDetails: object) => void;
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