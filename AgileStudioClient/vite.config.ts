import { defineConfig, loadEnv } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig(({mode}) => {
  const env = loadEnv(mode, process.cwd(), '');
  const host:string = env?.HOST ?? "localhost";

  return {
    server:{
      host: host,
      allowedHosts: [host],
    },
    plugins: [react()],
  };
});
