import Api from "../../Api.tsx";
import type {AxiosResponse} from "axios";
import type {BacklogItemTypeDto, BacklogItemTypePatchDto} from "../../dtos/accounts/BacklogItemTypeDtos.tsx";

export const baseUrl = '/Accounts/BacklogItemTypes';

export const getBacklogItemType = async (id:number): Promise<AxiosResponse<BacklogItemTypeDto>> => {
  return await Api.get(`${baseUrl}/${id}`);
};

export const updateBacklogItemType = async (dto:BacklogItemTypePatchDto): Promise<AxiosResponse<BacklogItemTypeDto>> => {
  return await Api.patch(`${baseUrl}/${dto.id}`, dto);
};

export const deleteBacklogItemType = async (id:number): Promise<AxiosResponse> => {
  return await Api.delete(`${baseUrl}/${id}`);
};