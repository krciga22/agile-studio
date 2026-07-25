import Api from "../../Api.tsx";
import type {AxiosResponse} from "axios";
import type {BacklogItemTypeSchemaDto, BacklogItemTypeSchemaPatchDto} from "../../dtos/accounts/BacklogItemTypeSchemaDtos.tsx";
import type {
  BacklogItemTypeSchemaNodeDto,
  BacklogItemTypeSchemaNodePostDto
} from "../../dtos/accounts/BacklogItemTypeSchemaNodeDtos.tsx";
import type {
  BacklogItemTypeSchemaEdgeDto,
  BacklogItemTypeSchemaEdgePostDto
} from "../../dtos/accounts/BacklogItemTypeSchemaEdgeDtos.tsx";
import type {PaginatedResultsDto} from "../../dtos/PaginatedResultsDto.tsx";
import type {GetCollectionQueryParams} from "../../api-utils.tsx";

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

export const getBacklogItemTypeSchemaNodes = async (id:number, params?: GetCollectionQueryParams):
  Promise<AxiosResponse<PaginatedResultsDto<BacklogItemTypeSchemaNodeDto>>> => {
  return await Api.get(`${baseUrl}/${id}/Nodes`, {params});
};

export const createBacklogItemTypeSchemaNode = async (dto:BacklogItemTypeSchemaNodePostDto):
  Promise<AxiosResponse<BacklogItemTypeSchemaNodeDto>> => {
  return await Api.post(`${baseUrl}/${dto.backlogItemTypeSchemaID}/Nodes`, dto);
};

export const getBacklogItemTypeSchemaEdges = async (id:number, params?: GetCollectionQueryParams):
  Promise<AxiosResponse<PaginatedResultsDto<BacklogItemTypeSchemaEdgeDto>>> => {
  return await Api.get(`${baseUrl}/${id}/Edges`, {params});
};

export const createBacklogItemTypeSchemaEdge = async (dto:BacklogItemTypeSchemaEdgePostDto):
  Promise<AxiosResponse<BacklogItemTypeSchemaEdgeDto>> => {
  return await Api.post(`${baseUrl}/${dto.backlogItemTypeSchemaID}/Edges`, dto);
};