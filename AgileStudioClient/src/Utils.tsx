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