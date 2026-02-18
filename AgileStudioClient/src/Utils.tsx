import Constants from "./Constants.tsx";

type SetDocumentTitleOptions = {
  prepend: boolean
};

const Utils = {
  setDocumentTitle: (title: string, options?: SetDocumentTitleOptions) => {
    const prepend = options?.prepend ?? true;
    if(prepend){
      title = `${title} - ${Constants.PRODUCT_NAME}`;
    }

    document.title = title;
  }
};

export default Utils;

/**
 * Debounce a given callback function, using a
 * timeout ID stored in a React ref.
 */
export const debounce = (callback: () => void, delay: number, timeoutIdRef: React.RefObject<number|null>) => {
  if(timeoutIdRef.current){
    clearTimeout(timeoutIdRef.current);
  }

  timeoutIdRef.current = setTimeout(() => {
    callback();
    timeoutIdRef.current = null;
  }, delay);
}

export const numberToString = (value: number): string => {
  return value === Constants.DEFAULT_VALUE_NUMBER ?
    Constants.DEFAULT_VALUE_STRING : value.toString();
}

export const stringToNumber = (value: string): number => {
  return value === Constants.DEFAULT_VALUE_STRING ?
    Constants.DEFAULT_VALUE_NUMBER : parseInt(value);
}