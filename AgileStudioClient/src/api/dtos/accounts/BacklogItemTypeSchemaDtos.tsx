import type {UserSummaryDto} from "../UserDtos.tsx";
import type {AccountSummaryDto} from "./AccountDtos.tsx";

export type BacklogItemTypeSchemaDto = {
  id: number,
  title?: string,
  description?: string,
  createdOn: string,
  createdBy: UserSummaryDto,
  account: AccountSummaryDto
};

export type BacklogItemTypeSchemaSummaryDto = {
  id: number,
  title?: string
};

export type BacklogItemTypeSchemaPostDto = {
  title: string,
  description?: string,
  accountID: number
};

export type BacklogItemTypeSchemaPatchDto = {
  id: number,
  title: string,
  description?: string
};