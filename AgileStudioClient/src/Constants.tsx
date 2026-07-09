import type {ToastOptions} from "react-toastify";

const Constants = {
  PRODUCT_NAME: "Agile Studio",
  PRODUCT_VERSION: "0.0.0",
  /**
   * Extra wait time in milliseconds to ease
   * transitions between loading and loaded
   * states.
   */
  EXTRA_WAIT_TIME_MS: 400,
  DEFAULT_VALUE_STRING: '',
  DEFAULT_VALUE_NUMBER: 0,
  DEFAULT_TOAST_PROPS: {
    position: "bottom-left"
  } as ToastOptions,
  DATE_FORMAT: 'yyyy-MM-dd',
  DATE_TIME_FORMAT: 'yyyy-MM-dd HH:mm:ss',
};

export default Constants;