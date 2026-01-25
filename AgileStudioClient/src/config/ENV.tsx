import type {ENV as ENV_TYPE} from "./../models/ENV.tsx";

const getEnvValue = <T = string | null>(key: string, fallback?: T): T => {
  const __ENV__ = window.__ENV__ ?? {};
  const val = __ENV__?.[key] ?? fallback;

  // return provided fallback (or null if no fallback)
  if (val === undefined || val === null) {
    return (fallback === undefined ? (null as unknown as T) : fallback) as T;
  }

  // convert booleans when a boolean fallback is provided
  if (typeof fallback === "boolean") {
    return ((val === true || val === "true") as unknown) as T;
  }

  // convert numbers when a number fallback is provided
  if (typeof fallback === "number") {
    const n = Number(val);
    return ((Number.isFinite(n) ? n : fallback) as unknown) as T;
  }

  return val as T;
};

const ENV:ENV_TYPE = Object.freeze({
  API_URL: getEnvValue<string>("API_URL"),
  AUTH0_DOMAIN: getEnvValue<string>("AUTH0_DOMAIN"),
  AUTH0_CLIENT_ID: getEnvValue<string>("AUTH0_CLIENT_ID"),
});

export default ENV
