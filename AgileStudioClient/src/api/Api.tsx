import axios, {type AxiosResponse} from "axios";
import ENV from "../config/ENV.tsx";
import type {Auth0ContextInterface} from "@auth0/auth0-react";
import {
  isProblemDetailsDto,
  type ProblemDetailsDto,
  type ProblemDetailsError,
  type ProblemDetailsErrorMap
} from "./dtos/ProblemDetailsDtos.tsx";

const Api = axios.create({
  baseURL: ENV.API_URL ?? "",
  timeout: 10000,
  withCredentials: true
});
export default Api;

export const initApiAuthBearerToken = async (auth0:Auth0ContextInterface) => {
  console.info("Initializing API auth bearer token");

  const accessToken: string|null = await auth0.getAccessTokenSilently({
    authorizationParams: {
      audience: ENV.AUTH0_AUDIENCE,
      scope: "openid profile email"
    }
  });

  if(accessToken){
    setApiAuthBearerToken(accessToken);
  }

  return accessToken;
}

export const setApiAuthBearerToken = (token: string|null) => {
  console.info("Setting API auth bearer token:",
    token ? token.substring(0, 7) + "..." : "null");

  if (token) {
    Api.defaults.headers['Authorization'] = `Bearer ${token}`;
  } else {
    delete Api.defaults.headers['Authorization'];
  }
}

/**
 * Get a map of problem details errors from an
 * axios response.
 */
export async function getProblemDetailsErrorMapFromResponse (response: AxiosResponse): Promise<ProblemDetailsErrorMap|null> {
  let errors = null;
  if(response && response.status === 400){
    const dto = await getProblemDetailsDto(response);
    if(dto){
      errors = getProblemDetailsErrorMapFromDto(dto);
    }
  }

  return errors;
}

/**
 * Get a map of problem details errors from a
 * ProblemDetailsDto.
 */
export function getProblemDetailsErrorMapFromDto(dto: ProblemDetailsDto): ProblemDetailsErrorMap {
  const errors: ProblemDetailsError[] = getProblemDetailsErrorsFromDto(dto);

  const mapped: ProblemDetailsErrorMap = {};
  for (const item of errors) {
    if (!item) continue;

    const rawKey = item.title || "";
    const lastSegment = rawKey.split(".").pop() || rawKey;
    const normalized = lastSegment.replace(/\[.*?\]/g, "").toLowerCase();

    const messages: string[] = Array.isArray(item.errors) ? item.errors : [];
    if (messages.length) {
      mapped[normalized] = {
        title: item.title || normalized,
        errors: messages
      };
    }
  }

  return mapped;
}

/**
 * Get an array of problem details errors from a
 * ProblemDetailsDto.
 */
export function getProblemDetailsErrorsFromDto(dto: ProblemDetailsDto): ProblemDetailsError[] {
  const errors: ProblemDetailsError[] = [];
  if(dto.errors){
    for(const key in dto.errors){
      const errorMessages:string[] = dto.errors[key];
      errors.push({
        title: key,
        errors: errorMessages
      });
    }
  }

  return errors;
}

/**
 * Get a ProblemDetailsDto from an axios response.
 */
export async function getProblemDetailsDto(resp: AxiosResponse): Promise<ProblemDetailsDto|null> {
  if(isProblemDetailsDto(resp.data)){
    return resp.data as ProblemDetailsDto;
  }

  return null;
}