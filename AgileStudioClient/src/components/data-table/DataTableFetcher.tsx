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
      const queryParams: Record<string, string | number | boolean | object | null | undefined> = {
        page
      };

      if(filters){
        for(const k in filters){
          const v = filters[k];
          if(Array.isArray(v)){
            queryParams[k] = v.join(',');
          }
          else if (v === null || v === undefined){
            // don't include in queryParams
          }
          else if (typeof v === 'object'){
            try{
              queryParams[k] = JSON.stringify(v as object);
            }
            catch{
              queryParams[k] = String(v);
            }
          }
          else{
            queryParams[k] = v as string | number | boolean;
          }
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