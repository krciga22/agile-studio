import Api from "../Api.tsx";
import type {AxiosResponse} from "axios";
import type {ProjectDto, ProjectPostDto} from "../dtos/ProjectDtos.tsx";

export const getProjects = async (): Promise<AxiosResponse<ProjectDto[]>> => {
  return await Api.get('/Project');
};

export const createProject = async (project:ProjectPostDto): Promise<AxiosResponse<ProjectDto>> => {
  return await Api.post('/Project', project);
};

export const getProject = async (id:number): Promise<AxiosResponse<ProjectDto>> => {
  return await Api.get(`/Project/${id}`);
};