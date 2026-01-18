import type {UserSummaryDto} from "./UserDtos.tsx";

export type ProjectDto = {
  id: number,
  title?: string,
  description?: string,
  createdOn: string,
  createdBy: UserSummaryDto,
  backlogItemTypeSchema: null,
  backlogItemLinkTypeSchema: null,
};

export type ProjectPostDto = {
  title: string,
  description?: string,
  backlogItemTypeSchemaId: number,
  backlogItemLinkTypeSchemaId: number,
};