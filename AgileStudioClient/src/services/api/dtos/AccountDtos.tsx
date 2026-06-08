import type {UserSummaryDto} from "./UserDtos.tsx";

export type AccountDto = {
  id: number,
  accountType: AccountTypeDto,
  createdOn: string,
  createdBy: UserSummaryDto,
};

export type AccountTypeDto = {
  id: number,
  title?: string,
  description?: string,
  createdOn: string,
  createdBy: UserSummaryDto,
};