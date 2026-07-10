
export type PaginatedResultsDto<T> = {
  items: T[],
  total: number,
  page: number,
  totalPages: number,
  prefPage: number,
  nextPage: number,
};