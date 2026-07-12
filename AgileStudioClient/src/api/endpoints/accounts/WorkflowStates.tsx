import Api from "../../Api.tsx";
import type {AxiosResponse} from "axios";
import type {WorkflowStateDto, WorkflowStatePatchDto} from "../../dtos/accounts/WorkflowStateDtos.tsx";

export const baseUrl = '/Accounts/WorkflowStates';

export const getWorkflowState = async (id:number): Promise<AxiosResponse<WorkflowStateDto>> => {
  return await Api.get(`${baseUrl}/${id}`);
};

export const updateWorkflowState = async (dto:WorkflowStatePatchDto): Promise<AxiosResponse<WorkflowStateDto>> => {
  return await Api.patch(`${baseUrl}/${dto.id}`, dto);
};

export const deleteWorkflowState = async (id:number): Promise<AxiosResponse> => {
  return await Api.delete(`${baseUrl}/${id}`);
};