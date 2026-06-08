import Api from "../../Api.tsx";
import type {AxiosResponse} from "axios";
import type {AccountDto} from "../../dtos/AccountDtos.tsx";
import type {PaginatedResultsDto} from "../../dtos/PaginatedResultsDto.tsx";

export const baseUrl = '/Accounts/Accounts';

export const getAccounts = async (): Promise<AxiosResponse<PaginatedResultsDto<AccountDto>>> => {
  return await Api.get(baseUrl);
};

export const getAccount = async (id:number): Promise<AxiosResponse<AccountDto>> => {
  return await Api.get(`${baseUrl}/${id}`);
};