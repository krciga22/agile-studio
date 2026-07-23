import type {UserSummaryDto} from "../UserDtos.tsx";
import type {BacklogItemTypeSchemaDto} from "./BacklogItemTypeSchemaDtos.tsx";
import type {BacklogItemTypeDto} from "./BacklogItemTypeDtos.tsx";

export type BacklogItemTypeSchemaNodeDto = {
  id: number,
  backlogItemTypeSchema: BacklogItemTypeSchemaDto,
  backlogItemType: BacklogItemTypeDto,
  createdOn: string,
  createdBy: UserSummaryDto,
};

export type BacklogItemTypeSchemaNodePostDto = {
  backlogItemTypeSchemaID: number,
  backlogItemTypeID: number,
};