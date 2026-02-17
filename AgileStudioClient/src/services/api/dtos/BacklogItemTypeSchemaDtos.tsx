import type {UserSummaryDto} from "./UserDtos.tsx";

export type BacklogItemTypeSchemaDto = {
  id: number,
  title?: string,
  description?: string,
  createdOn: string,
  createdBy: UserSummaryDto
};

export type BacklogItemTypeSchemaSummaryDto = {
  id: number,
  title?: string
};