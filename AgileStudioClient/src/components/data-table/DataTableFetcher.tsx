import './DataTable.css'
import api from "../../services/api/Api.tsx";
import type {FilterValue} from "./filters/Filters.tsx";
import type {PaginatedResultsDto} from "../../services/api/dtos/PaginatedResultsDto.tsx";

export interface IDataTableFetcher<T> {
  fetchData (searchQuery: string, filters: Record<string, unknown>, sort: string[], page: number): Promise<T[]>;
}

/**
 * Default data fetcher implementation that
 * retrieves data from a specified API endpoint.
 */
export class DataTableFetcher<T> implements IDataTableFetcher<T> {

  private readonly apiEndpoint: string;

  constructor (apiEndpoint: string) {
    this.apiEndpoint = apiEndpoint;
  }

  private getQueryParams(searchQuery: string, filters: Record<string, FilterValue>, sort: string[], page: number = 1) {
    const queryParams: Record<string, string | number | boolean | object | null | undefined> = {
      page
    };

    if(filters){
      const filtersQueryParams: Record<string, string | number | boolean | object | null | undefined> = {};

      for(const k in filters){
        const v = (filters[k] as FilterValue).value;

        if(Array.isArray(v)){
          filtersQueryParams[k] = v.join(',');
        }
        else if (v === null || v === undefined){
          // don't include in queryParams
        }
        else if (typeof v === 'object'){
          try{
            filtersQueryParams[k] = JSON.stringify(v as object);
          }
          catch{
            filtersQueryParams[k] = String(v);
          }
        }
        else{
          filtersQueryParams[k] = v as string | number | boolean;
        }
      }

      if(Object.keys(filtersQueryParams).length > 0){
        queryParams.filters = filtersQueryParams;
      }
    }

    if(searchQuery.length > 0){
      queryParams.searchQuery = searchQuery;
    }

    if(sort.length > 0){
      queryParams.sort = sort.join(',');
    }

    return queryParams;
  }

  async fetchPaginatedData(searchQuery: string, filters: Record<string, FilterValue>, sort: string[], page: number = 1): Promise<PaginatedResultsDto<T>>{
    try{
      const queryParams = this.getQueryParams(searchQuery, filters, sort, page);

      const response = await api.get(this.apiEndpoint, {
        params: queryParams
      });
      return response.data as PaginatedResultsDto<T>;
    }
    catch(error){
      console.error("Error fetching data:", error);
      throw error;
    }
  }

  async fetchData (searchQuery: string, filters: Record<string, FilterValue>, sort: string[], page: number = 1): Promise<T[]>{
    const response = await this.fetchPaginatedData(searchQuery, filters, sort, page);
    return response.items as T[];
  }
}