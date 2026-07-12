import type {UserSummaryDto} from "../UserDtos.tsx";
import type {WorkflowSummaryDto} from "./WorkflowDtos.tsx";

export type WorkflowStateDto = {
  id: number,
  title?: string,
  description?: string,
  createdOn: string,
  createdBy: UserSummaryDto,
  workflow: WorkflowSummaryDto
};

export type WorkflowStateSummaryDto = {
  id: number,
  title?: string
};

export type WorkflowStatePostDto = {
  title: string,
  description?: string,
  workflowId: number
};

export type WorkflowStatePatchDto = {
  id: number,
  title: string,
  description?: string
};