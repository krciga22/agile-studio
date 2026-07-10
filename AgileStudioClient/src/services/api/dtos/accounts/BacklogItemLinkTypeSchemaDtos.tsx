import type {UserSummaryDto} from "../UserDtos.tsx";

export type BacklogItemLinkTypeSchemaDto = {
  id: number,
  title?: string,
  description?: string,
  createdOn: string,
  createdBy: UserSummaryDto
};

export type BacklogItemLinkTypeSchemaSummaryDto = {
  id: number,
  title?: string,
  description?: string
};