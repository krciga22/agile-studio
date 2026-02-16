import './FormError.css'
import {
  isProblemDetailsError,
  isProblemDetailsErrorMap,
  type ProblemDetailsError,
  type ProblemDetailsErrorMap
} from "../../services/api/dtos/ProblemDetailsDtos.tsx";

type FormErrorProps = {
  error: ProblemDetailsErrorMap|ProblemDetailsError|string|null;
  id?: string;
};

export default function FormError({error, id}: FormErrorProps) {
  if (!error) return null;

  let messages: string[] = [];
  if(typeof error === 'string'){
    messages = [error];
  }
  else if(isProblemDetailsError(error)){
    messages = error.errors;
  }
  else if(isProblemDetailsErrorMap(error) && id && error?.[id]){
    messages = error[id].errors;
  }

  if (!messages.length) return null;

  return (
    <div id={id} role="alert" className={"FormError"}>
      <ul>
        {messages.map((msg, i) => {
          const key = `error-${id}-${i}`;
          return <li key={key}>{msg}</li>;
        })}
      </ul>
    </div>
  );
}