import Api from "../Api.tsx";
import type {AxiosResponse} from "axios";
import type {
  ProjectDto
} from "../dtos/ProjectDtos.tsx";

export const getProject = async (id:number): Promise<AxiosResponse<ProjectDto>> => {
  return await Api.get(`/Project/${id}`);
};