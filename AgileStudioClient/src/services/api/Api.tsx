import axios from "axios";
import ENV from "../../config/ENV.tsx";
import type {Auth0ContextInterface} from "@auth0/auth0-react";

const Api = axios.create({
  baseURL: ENV.API_URL ?? "",
  timeout: 10000,
  withCredentials: true
});
export default Api;

export const initApiAuthBearerToken = async (auth0:Auth0ContextInterface) => {
  console.info("Initializing API auth bearer token");

  return new Promise<string>((resolve, reject) => {
    auth0.getAccessTokenSilently()
      .then(accessToken => {
        setApiAuthBearerToken(accessToken);
        resolve(accessToken);
      })
      .catch(error => {
        console.error('Error getting access token', error);
        reject(error);
      });
  });
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