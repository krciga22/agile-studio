import type {UserSummaryDto} from "../UserDtos.tsx";

export type AccountTypeDto = {
  id: number,
  title?: string,
  description?: string,
  createdOn: string,
  createdBy: UserSummaryDto,
};