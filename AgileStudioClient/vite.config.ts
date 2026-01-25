import { defineConfig, loadEnv } from 'vite'
import react from '@vitejs/plugin-react'
import * as path from "node:path";
import * as fs from "node:fs";

// https://vite.dev/config/
export default defineConfig(({mode}) => {
  const env = loadEnv(mode, process.cwd(), '');
  const host:string = env?.HOST ?? "localhost";

  let https = {};
  const sslCert:string = env?.SSL_CERT ?? "";
  const sslKey:string = env?.SSL_KEY ?? "";
  if(sslCert && sslKey){
    const certPath = path.resolve(process.cwd(), sslCert);
    const keyPath = path.resolve(process.cwd(), sslKey);

    try {
      https = {
        cert: fs.readFileSync(certPath),
        key: fs.readFileSync(keyPath),
      }
    }
    catch (error) {
      console.error(error);
      console.warn(`Failed to load ssl cert (${certPath}) and/or key (${keyPath}); starting without HTTPS.`);
      https = false
    }
  }

  return {
    server:{
      host: host,
      allowedHosts: [host],
      https
    },
    plugins: [react()],
  };
});
