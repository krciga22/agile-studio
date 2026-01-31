declare global {
  interface Window {
    __ENV__?: { [key: string]: string | boolean | number };
  }
}
export {};