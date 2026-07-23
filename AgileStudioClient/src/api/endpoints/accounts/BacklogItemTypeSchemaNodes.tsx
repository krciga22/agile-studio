import Api from "../../Api.tsx";
import type {AxiosResponse} from "axios";
import type {BacklogItemTypeSchemaNodeDto} from "../../dtos/accounts/BacklogItemTypeSchemaNodeDtos.tsx";

export const baseUrl = '/Accounts/BacklogItemTypeSchemaNodes';

export const getBacklogItemTypeSchemaNode = async (id:number): Promise<AxiosResponse<BacklogItemTypeSchemaNodeDto>> => {
  return await Api.get(`${baseUrl}/${id}`);
};

export const deleteBacklogItemTypeSchemaNode = async (id:number): Promise<AxiosResponse> => {
  return await Api.delete(`${baseUrl}/${id}`);
};