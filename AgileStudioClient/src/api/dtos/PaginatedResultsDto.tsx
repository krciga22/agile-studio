
export type PaginatedResultsDto<T> = {
  items: T[],
  total: number,
  page: number,
  totalPages: number,
  prevPage: number,
  nextPage: number,
};