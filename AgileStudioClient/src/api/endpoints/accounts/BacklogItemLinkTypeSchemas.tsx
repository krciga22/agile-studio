import Api from "../../Api.tsx";
import type {AxiosResponse} from "axios";
import type {BacklogItemLinkTypeSchemaDto} from "../../dtos/accounts/BacklogItemLinkTypeSchemaDtos.tsx";
import type {GetCollectionQueryParams} from "../../api-utils.tsx";

export const baseUrl = '/Accounts/BacklogItemLinkTypeSchemas';

export const getBacklogItemLinkTypeSchemas = async (params?:GetCollectionQueryParams): Promise<AxiosResponse<BacklogItemLinkTypeSchemaDto[]>> => {
  return await Api.get(baseUrl, {params});
};

export const getBacklogItemLinkTypeSchema = async (id:number): Promise<AxiosResponse<BacklogItemLinkTypeSchemaDto>> => {
  return await Api.get(`${baseUrl}/${id}`);
};