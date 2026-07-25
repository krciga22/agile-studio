import Api from "../../Api.tsx";
import type {AxiosResponse} from "axios";
import type {
  ProjectDto,
  ProjectPatchDto,
  ProjectPostDto
} from "../../dtos/projects/ProjectDtos.tsx";
import type {PaginatedResultsDto} from "../../dtos/PaginatedResultsDto.tsx";
import type {GetCollectionQueryParams} from "../../api-utils.tsx";

export const baseUrl = '/Projects/Projects';

export const getProjects = async (params?: GetCollectionQueryParams): Promise<AxiosResponse<PaginatedResultsDto<ProjectDto>>> => {
  return await Api.get(baseUrl, {params});
};

export const createProject = async (project:ProjectPostDto): Promise<AxiosResponse<ProjectDto>> => {
  return await Api.post(baseUrl, project);
};

export const updateProject = async (id:number, project:ProjectPatchDto): Promise<AxiosResponse<ProjectDto>> => {
  return await Api.patch(`${baseUrl}/${id}`, project);
};

export const getProject = async (id:number): Promise<AxiosResponse<ProjectDto>> => {
  return await Api.get(`${baseUrl}/${id}`);
};

export const deleteProject = async (id:number): Promise<AxiosResponse> => {
  return await Api.delete(`${baseUrl}/${id}`);
};