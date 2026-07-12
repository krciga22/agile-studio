import Api from "../../Api.tsx";
import type {AxiosResponse} from "axios";
import type {AccountDto} from "../../dtos/accounts/AccountDtos.tsx";
import type {PaginatedResultsDto} from "../../dtos/PaginatedResultsDto.tsx";
import type {BacklogItemTypeSchemaDto, BacklogItemTypeSchemaPostDto} from "../../dtos/accounts/BacklogItemTypeSchemaDtos.tsx";
import type {BacklogItemTypeDto, BacklogItemTypePostDto} from "../../dtos/accounts/BacklogItemTypeDtos.tsx";
import type {WorkflowDto, WorkflowPostDto} from "../../dtos/accounts/WorkflowDtos.tsx";

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
  Promise<AxiosResponse<BacklogItemTypeDto>> => {
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

export const getWorkflows = async (accountId:number, sort: string[] = []):
  Promise<AxiosResponse<PaginatedResultsDto<WorkflowDto>>> => {
  const params: Record<string, string> = {};

  if(sort.length > 0){
    params.sort = sort.join(',');
  }

  return await Api.get(`${baseUrl}/${accountId}/Workflows`, {params});
};

export const createWorkflow = async (dto:WorkflowPostDto):
  Promise<AxiosResponse<WorkflowDto>> => {
  return await Api.post(`${baseUrl}/${dto.accountId}/Workflows`, dto);
};
