import Api from "../../Api.tsx";
import type {AxiosResponse} from "axios";
import type {BacklogItemTypeSchemaDto} from "../../dtos/BacklogItemTypeSchemaDtos.tsx";

const baseUrl = '/Accounts/BacklogItemTypeSchemas';

export const getBacklogItemTypeSchemas = async (): Promise<AxiosResponse<BacklogItemTypeSchemaDto[]>> => {
  return await Api.get(baseUrl);
};

export const getBacklogItemTypeSchema = async (id:number): Promise<AxiosResponse<BacklogItemTypeSchemaDto>> => {
  return await Api.get(`${baseUrl}/${id}`);
};