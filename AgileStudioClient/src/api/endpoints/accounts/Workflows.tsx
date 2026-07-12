import Api from "../../Api.tsx";
import type {AxiosResponse} from "axios";
import type {WorkflowDto, WorkflowPatchDto} from "../../dtos/accounts/WorkflowDtos.tsx";
import type {PaginatedResultsDto} from "../../dtos/PaginatedResultsDto.tsx";
import type {WorkflowStateDto, WorkflowStatePostDto} from "../../dtos/accounts/WorkflowStateDtos.tsx";

export const baseUrl = '/Accounts/Workflows';

export const getWorkflow = async (id:number): Promise<AxiosResponse<WorkflowDto>> => {
  return await Api.get(`${baseUrl}/${id}`);
};

export const updateWorkflow = async (dto:WorkflowPatchDto): Promise<AxiosResponse<WorkflowDto>> => {
  return await Api.patch(`${baseUrl}/${dto.id}`, dto);
};

export const deleteWorkflow = async (id:number): Promise<AxiosResponse> => {
  return await Api.delete(`${baseUrl}/${id}`);
};

export const getWorkflowStates = async (workflowId:number):
  Promise<AxiosResponse<PaginatedResultsDto<WorkflowStateDto>>> => {
  return await Api.get(`${baseUrl}/${workflowId}/WorkflowStates`);
};

export const createWorkflowState = async (dto:WorkflowStatePostDto):
  Promise<AxiosResponse<WorkflowStateDto>> => {
  return await Api.post(`${baseUrl}/${dto.workflowId}/WorkflowStates`, dto);
};