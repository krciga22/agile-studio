import './DataTable.css'
import api from "../../services/api/Api.tsx";

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

  async fetchData (searchQuery: string, filters: Record<string, unknown>, sort: string[], page: number = 1): Promise<T[]>{
    try{
      const queryParams: Record<string, any> = {
        ...filters,
        page
      };

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