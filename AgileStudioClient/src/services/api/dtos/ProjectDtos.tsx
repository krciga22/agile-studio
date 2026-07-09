import type {UserSummaryDto} from "./UserDtos.tsx";
import type {BacklogItemTypeSchemaSummaryDto} from "./BacklogItemTypeSchemaDtos.tsx";
import type {BacklogItemLinkTypeSchemaSummaryDto} from "./BacklogItemLinkTypeSchemaDtos.tsx";
import type {AccountSummaryDto} from "./AccountDtos.tsx";

export type ProjectDto = {
  id: number,
  title?: string,
  description?: string,
  createdOn: string,
  createdBy: UserSummaryDto,
  account: AccountSummaryDto,
  backlogItemTypeSchema: BacklogItemTypeSchemaSummaryDto,
  backlogItemLinkTypeSchema: BacklogItemLinkTypeSchemaSummaryDto,
};

export type ProjectPostDto = {
  title: string,
  description?: string,
  accountId: number,
  backlogItemTypeSchemaId: number,
  backlogItemLinkTypeSchemaId: number,
};

export type ProjectPatchDto = {
  id: number,
  title: string,
  description?: string,
};