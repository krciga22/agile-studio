import Api from "../Api.tsx";
import type {AxiosResponse} from "axios";
import type {PaginatedResultsDto} from "../dtos/PaginatedResultsDto.tsx";

export const getResources = async <TDto,>(type: string): Promise<AxiosResponse<PaginatedResultsDto<TDto>>> => {
  return await Api.get('/Resource/' + type);
};