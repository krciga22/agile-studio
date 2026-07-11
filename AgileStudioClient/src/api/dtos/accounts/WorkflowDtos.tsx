import type {UserSummaryDto} from "../UserDtos.tsx";
import type {AccountSummaryDto} from "./AccountDtos.tsx";

export type WorkflowDto = {
  id: number,
  title?: string,
  description?: string,
  createdOn: string,
  createdBy: UserSummaryDto,
  account: AccountSummaryDto
};

export type WorkflowSummaryDto = {
  id: number,
  title?: string
};

export type WorkflowPostDto = {
  title: string,
  description?: string,
  accountId: number
};

export type WorkflowPatchDto = {
  id: number,
  title: string,
  description?: string
};