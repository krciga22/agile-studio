import axios from "axios";
import ENV from "../../config/ENV.tsx";

const Api = axios.create({
  baseURL: ENV.API_URL ?? "",
  timeout: 1000,
  headers: {'X-Custom-Header': 'foobar'}
});

export default Api;