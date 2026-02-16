
export type ProblemDetailsDto = {
  type?: string;
  title?: string;
  status?: number;
  detail?: string;
  errors?: Record<string, string[]>;
};

export const isProblemDetailsDto = (obj: any): obj is ProblemDetailsDto => {
  return obj && typeof obj === 'object' && (typeof obj.type === 'string' || typeof obj.title === 'string' || typeof obj.status === 'number' || typeof obj.detail === 'string' || (obj.errors && typeof obj.errors === 'object'));
}

export type ProblemDetailsError = {
  title: string;
  errors: string[];
};

export const isProblemDetailsError = (obj: any): obj is ProblemDetailsError => {
  return obj && typeof obj === 'object' && typeof obj.title === 'string' && Array.isArray(obj.errors);
};

export type ProblemDetailsErrorMap = Record<string, ProblemDetailsError>;

export const isProblemDetailsErrorMap = (obj: any): obj is ProblemDetailsErrorMap => {
  if (!obj || typeof obj !== 'object') return false;

  for (const key in obj) {
    if (!isProblemDetailsError(obj[key])) {
      return false;
    }
  }

  return true;
};