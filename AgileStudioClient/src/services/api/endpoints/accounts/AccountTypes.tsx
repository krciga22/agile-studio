import Api from "../../Api.tsx";
import type {AxiosResponse} from "axios";
import type {PaginatedResultsDto} from "../../dtos/PaginatedResultsDto.tsx";
import type {AccountTypeDto} from "../../dtos/accounts/AccountTypeDtos.tsx";

export const baseUrl = '/Accounts/AccountTypes';

export const getAccountTypes = async (): Promise<AxiosResponse<PaginatedResultsDto<AccountTypeDto>>> => {
  return await Api.get(baseUrl);
};

export const getAccountType = async (id:number): Promise<AxiosResponse<AccountTypeDto>> => {
  return await Api.get(`${baseUrl}/${id}`);
};