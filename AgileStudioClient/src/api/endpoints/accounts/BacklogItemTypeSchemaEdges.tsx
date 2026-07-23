import Api from "../../Api.tsx";
import type {AxiosResponse} from "axios";
import type {BacklogItemTypeSchemaEdgeDto} from "../../dtos/accounts/BacklogItemTypeSchemaEdgeDtos.tsx";

export const baseUrl = '/Accounts/BacklogItemTypeSchemaEdges';

export const getBacklogItemTypeSchemaEdge = async (id:number): Promise<AxiosResponse<BacklogItemTypeSchemaEdgeDto>> => {
  return await Api.get(`${baseUrl}/${id}`);
};

export const deleteBacklogItemTypeSchemaEdge = async (id:number): Promise<AxiosResponse> => {
  return await Api.delete(`${baseUrl}/${id}`);
};