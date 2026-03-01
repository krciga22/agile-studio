import './DataTable.css'
import api from "../../services/api/Api.tsx";
import type {FilterValue} from "./filters/Filters.tsx";

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

  async fetchData (searchQuery: string, filters: Record<string, FilterValue>, sort: string[], page: number = 1): Promise<T[]>{
    try{
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

      const response = await api.get(this.apiEndpoint, {
        params: queryParams
      });
      return response.data as T[];
    }
    catch(error){
      console.error("Error fetching data:", error);
      throw error;
    }
  }
}