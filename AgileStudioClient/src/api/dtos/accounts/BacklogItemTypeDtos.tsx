import type {UserSummaryDto} from "../UserDtos.tsx";
import type {AccountSummaryDto} from "./AccountDtos.tsx";
import type {WorkflowSummaryDto} from "./WorkflowDtos.tsx";

export type BacklogItemTypeDto = {
  id: number,
  title?: string,
  description?: string,
  createdOn: string,
  createdBy: UserSummaryDto,
  account: AccountSummaryDto,
  workflow: WorkflowSummaryDto
};

export type BacklogItemTypeSummaryDto = {
  id: number,
  title?: string,
  description?: string,
  createdOn: string,
};

export type BacklogItemTypePostDto = {
  title: string,
  accountID: number,
  workflowID: number,
  description?: string
};

export type BacklogItemTypePatchDto = {
  id: number,
  title: string,
  description?: string
};