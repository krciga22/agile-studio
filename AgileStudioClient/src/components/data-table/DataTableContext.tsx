import React from "react";

type DataTableContextValue = {
  data: object[];
  filters: Record<string, unknown>;
  sort: string[];
  paginationDetails: object|null;
  setData: (data: object[]) => void;
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