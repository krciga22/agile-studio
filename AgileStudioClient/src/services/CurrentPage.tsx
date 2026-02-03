import {createContext} from "react";

export type CurrentPageContextValue = {
  pathname: string|null,
  state: object|null,
};

const CurrentPageContext = createContext<CurrentPageContextValue>({
  pathname: null,
  state: null,
});
export default CurrentPageContext;