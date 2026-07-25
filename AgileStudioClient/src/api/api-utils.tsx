import type {AxiosResponse} from "axios";
import type {PaginatedResultsDto} from "./dtos/PaginatedResultsDto.tsx";

/**
 * Fetch all pages from an API that returns PaginatedResultsDto<T>.
 * The fetchPage callback should accept a page number and return an AxiosResponse<PaginatedResultsDto<T>>.
 * Returns an array of results from all pages.
 */
export const paginatedResultsToArray = async <T,> (fetchPage: (page: number) => Promise<AxiosResponse<PaginatedResultsDto<T>>>): Promise<T[]> => {
  const firstResp = await fetchPage(1);
  const firstData = firstResp.data;
  const items: T[] = [...(firstData.items ?? [])];
  const totalPages = firstData.totalPages ?? 1;

  // Fetch remaining pages (if any)
  for(let page = 2; page <= totalPages; page++){
    const resp = await fetchPage(page);
    if(resp && resp.data && resp.data.items){
      items.push(...resp.data.items);
    }
  }

  return items;
};

export type GetCollectionQueryParams = {
  page?: number;
  itemsPerPage?: number;
  sort?: string;
  searchQuery?: string;
};

export const toSortString = (sort:string[]) => {
  return sort.join(",");
};