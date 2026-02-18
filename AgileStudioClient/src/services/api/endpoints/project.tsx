import Api from "../Api.tsx";
import type {AxiosResponse} from "axios";
import type {
  ProjectDto,
  ProjectPatchDto,
  ProjectPostDto
} from "../dtos/ProjectDtos.tsx";

export const getProjects = async (): Promise<AxiosResponse<ProjectDto[]>> => {
  return await Api.get('/Project');
};

export const createProject = async (project:ProjectPostDto): Promise<AxiosResponse<ProjectDto>> => {
  return await Api.post('/Project', project);
};

export const updateProject = async (id:number, project:ProjectPatchDto): Promise<AxiosResponse<ProjectDto>> => {
  return await Api.patch(`/Project/${id}`, project);
};

export const getProject = async (id:number): Promise<AxiosResponse<ProjectDto>> => {
  return await Api.get(`/Project/${id}`);
};

export const deleteProject = async (id:number): Promise<AxiosResponse> => {
  return await Api.delete(`/Project/${id}`);
};