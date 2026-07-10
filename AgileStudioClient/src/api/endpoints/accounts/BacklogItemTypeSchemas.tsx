import Api from "../../Api.tsx";
import type {AxiosResponse} from "axios";
import type {BacklogItemTypeSchemaDto, BacklogItemTypeSchemaPatchDto} from "../../dtos/accounts/BacklogItemTypeSchemaDtos.tsx";

export const baseUrl = '/Accounts/BacklogItemTypeSchemas';

export const getBacklogItemTypeSchema = async (id:number): Promise<AxiosResponse<BacklogItemTypeSchemaDto>> => {
  return await Api.get(`${baseUrl}/${id}`);
};

export const updateBacklogItemTypeSchema = async (dto:BacklogItemTypeSchemaPatchDto): Promise<AxiosResponse<BacklogItemTypeSchemaDto>> => {
  return await Api.patch(`${baseUrl}/${dto.id}`, dto);
};

export const deleteBacklogItemTypeSchema = async (id:number): Promise<AxiosResponse> => {
  return await Api.delete(`${baseUrl}/${id}`);
};