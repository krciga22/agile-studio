import type {UserSummaryDto} from "../UserDtos.tsx";
import type {AccountTypeDto} from "./AccountTypeDtos.tsx";

export type AccountDto = {
  id: number,
  accountType: AccountTypeDto,
  createdOn: string,
  createdBy: UserSummaryDto,
};

export type AccountSummaryDto = {
  id: number,
};