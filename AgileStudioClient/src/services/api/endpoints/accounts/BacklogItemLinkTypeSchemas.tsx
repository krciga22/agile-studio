import Api from "../../Api.tsx";
import type {AxiosResponse} from "axios";
import type {BacklogItemLinkTypeSchemaDto} from "../../dtos/BacklogItemLinkTypeSchemaDtos.tsx";

const baseUrl = '/Accounts/BacklogItemLinkTypeSchemas';

export const getBacklogItemLinkTypeSchemas = async (): Promise<AxiosResponse<BacklogItemLinkTypeSchemaDto[]>> => {
  return await Api.get(baseUrl);
};

export const getBacklogItemLinkTypeSchema = async (id:number): Promise<AxiosResponse<BacklogItemLinkTypeSchemaDto>> => {
  return await Api.get(`${baseUrl}/${id}`);
};