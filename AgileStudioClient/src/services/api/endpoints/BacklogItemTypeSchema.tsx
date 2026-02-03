import Api from "../Api.tsx";
import type {AxiosResponse} from "axios";
import type {BacklogItemTypeSchemaDto} from "../dtos/BacklogItemTypeSchemaDtos.tsx";

export const getBacklogItemTypeSchemas = async (): Promise<AxiosResponse<BacklogItemTypeSchemaDto[]>> => {
  return await Api.get('/BacklogItemTypeSchema');
};

export const getBacklogItemTypeSchema = async (id:number): Promise<AxiosResponse<BacklogItemTypeSchemaDto>> => {
  return await Api.get(`/BacklogItemTypeSchema/${id}`);
};