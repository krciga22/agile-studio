import Api from "../../Api.tsx";
import type {AxiosResponse} from "axios";
import type {WorkflowDto, WorkflowPatchDto} from "../../dtos/accounts/WorkflowDtos.tsx";

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