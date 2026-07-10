import Api from "../Api.tsx";
import type {AxiosResponse} from "axios";
import type {CurrentUserDto} from "../dtos/auth/CurrentUserDto.tsx";

export const getCurrentUser = async (): Promise<AxiosResponse<CurrentUserDto>> => {
  return await Api.get('/Auth/CurrentUser');
};
