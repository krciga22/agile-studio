import type {AxiosError} from "axios";

export const ERROR_MESSAGE_DEFAULT = "An error has occurred";

export const ERROR_MESSAGE_INVALID_FORM_FIELDS = "One or more fields is invalid";

export const ERROR_MESSAGE_UNAUTHENTICATED = "You must be authenticated to perform this action";

export const ERROR_MESSAGE_UNAUTHORIZED = "You are not authorized to perform this action";

export const ERROR_CONTEXT = {
  FORM_SUBMISSION: "form_submission",
  DATA_FETCHING: "data_fetching",
  OTHER: "other"
}

export const getErrorMessageForAxiosError = (err: AxiosError, errorContext: string = ERROR_CONTEXT.OTHER): string => {
  let message: string = ERROR_MESSAGE_DEFAULT;

  if(err.response){
    if(err.response.status === 400){
      if(errorContext === ERROR_CONTEXT.FORM_SUBMISSION){
        message = ERROR_MESSAGE_INVALID_FORM_FIELDS;
      }
    }
    else if(err.response.status === 401){
      message = ERROR_MESSAGE_UNAUTHENTICATED;
    }
    else if(err.response.status === 403){
      message = ERROR_MESSAGE_UNAUTHORIZED;
    }
  }
  else if(err.code === 'ERR_NETWORK'){
    message = "A network error has occurred.";
  }
  else if(err.code === 'ECONNABORTED'){
    message = "The request timed out. Please try again later.";
  }

  return message;
}