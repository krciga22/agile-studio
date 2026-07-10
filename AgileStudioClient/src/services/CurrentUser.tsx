import {createContext} from "react";
import type {CurrentUserDto} from "./api/dtos/auth/CurrentUserDto.tsx";
import type {AxiosError} from "axios";

export type CurrentUserContextValue = {
  user: CurrentUserDto|null,
  error: AxiosError|null,
  isLoading: boolean,
};

const CurrentUserContext = createContext<CurrentUserContextValue>({
  user: null,
  error: null,
  isLoading: true
});
export default CurrentUserContext;