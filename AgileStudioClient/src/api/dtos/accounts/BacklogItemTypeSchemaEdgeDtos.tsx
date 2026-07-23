import type {UserSummaryDto} from "../UserDtos.tsx";
import type {BacklogItemTypeSchemaDto} from "./BacklogItemTypeSchemaDtos.tsx";
import type {BacklogItemTypeDto} from "./BacklogItemTypeDtos.tsx";

export type BacklogItemTypeSchemaEdgeDto = {
  id: number,
  backlogItemTypeSchema: BacklogItemTypeSchemaDto,
  fromType: BacklogItemTypeDto,
  toType: BacklogItemTypeDto,
  createdOn: string,
  createdBy: UserSummaryDto,
};

export type BacklogItemTypeSchemaEdgePostDto = {
  backlogItemTypeSchemaID: number,
  fromTypeID: number,
  toTypeID: number,
};