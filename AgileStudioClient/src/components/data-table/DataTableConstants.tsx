
export const DATA_TABLE_SORT_ASC = 'asc' as const;
export const DATA_TABLE_SORT_DESC = 'desc' as const;

export type DataTableSortDirection = typeof DATA_TABLE_SORT_ASC | typeof DATA_TABLE_SORT_DESC;
