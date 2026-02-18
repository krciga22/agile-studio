import type {UserSummaryDto} from "./UserDtos.tsx";
import type {BacklogItemTypeSchemaSummaryDto} from "./BacklogItemTypeSchemaDtos.tsx";
import type {BacklogItemLinkTypeSchemaSummaryDto} from "./BacklogItemLinkTypeSchemaDtos.tsx";

export type ProjectDto = {
  id: number,
  title?: string,
  description?: string,
  createdOn: string,
  createdBy: UserSummaryDto,
  backlogItemTypeSchema: BacklogItemTypeSchemaSummaryDto,
  backlogItemLinkTypeSchema: BacklogItemLinkTypeSchemaSummaryDto,
};

export type ProjectPostDto = {
  title: string,
  description?: string,
  backlogItemTypeSchemaId: number,
  backlogItemLinkTypeSchemaId: number,
};

export type ProjectPatchDto = {
  id: number,
  title: string,
  description?: string,
};