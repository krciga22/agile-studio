import Api from "../Api.tsx";
import type {AxiosResponse} from "axios";
import type {PaginatedResultsDto} from "../dtos/PaginatedResultsDto.tsx";

export const getResources = async <TDto,>(type: string): Promise<AxiosResponse<PaginatedResultsDto<TDto>>> => {
  return await Api.get(`/Resource/${type}`);
};

export const getResource = async <TDto,>(type: string, id:number): Promise<AxiosResponse<TDto>> => {
  return await Api.get(`/Resource/${type}/${id}`);
};

export const createResource = async <TDto,>(type: string, data: object): Promise<AxiosResponse<TDto>> => {
  return await Api.post(`/Resource/${type}`, data);
};

export const updateResource = async <TDto,>(type: string, id: number, data: object): Promise<AxiosResponse<TDto>> => {
  return await Api.patch(`/Resource/${type}/${id}`, data);
};

export const deleteResource = async (type: string, id: number): Promise<AxiosResponse> => {
  return await Api.delete(`/Resource/${type}/${id}`);
};