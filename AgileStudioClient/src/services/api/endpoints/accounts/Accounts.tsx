import Api from "../../Api.tsx";
import type {AxiosResponse} from "axios";
import type {AccountDto} from "../../dtos/AccountDtos.tsx";
import type {PaginatedResultsDto} from "../../dtos/PaginatedResultsDto.tsx";
import type {BacklogItemTypeSchemaDto, BacklogItemTypeSchemaPostDto} from "../../dtos/BacklogItemTypeSchemaDtos.tsx";
import type {BacklogItemTypeDto, BacklogItemTypePostDto} from "../../dtos/BacklogItemTypeDtos.tsx";

export const baseUrl = '/Accounts/Accounts';

export const getAccounts = async (): Promise<AxiosResponse<PaginatedResultsDto<AccountDto>>> => {
  return await Api.get(baseUrl);
};

export const getAccount = async (id:number): Promise<AxiosResponse<AccountDto>> => {
  return await Api.get(`${baseUrl}/${id}`);
};

export const getBacklogItemTypes = async (accountId:number):
  Promise<AxiosResponse<PaginatedResultsDto<BacklogItemTypeDto>>> => {
  return await Api.get(`${baseUrl}/${accountId}/BacklogItemTypes`);
};

export const createBacklogItemType = async (dto:BacklogItemTypePostDto):
  Promise<AxiosResponse<BacklogItemTypeSchemaDto>> => {
  return await Api.post(`${baseUrl}/${dto.accountID}/BacklogItemTypes`, dto);
};

export const getBacklogItemTypeSchemas = async (accountId:number):
  Promise<AxiosResponse<PaginatedResultsDto<BacklogItemTypeSchemaDto>>> => {
  return await Api.get(`${baseUrl}/${accountId}/BacklogItemTypeSchemas`);
};

export const createBacklogItemTypeSchema = async (dto:BacklogItemTypeSchemaPostDto):
  Promise<AxiosResponse<BacklogItemTypeSchemaDto>> => {
  return await Api.post(`${baseUrl}/${dto.accountID}/BacklogItemTypeSchemas`, dto);
};
