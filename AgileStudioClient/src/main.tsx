import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import {Auth0Provider} from "@auth0/auth0-react";
import ENV from "./config/ENV.tsx";
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <Auth0Provider
      domain={ENV.AUTH0_DOMAIN}
      clientId={ENV.AUTH0_CLIENT_ID}
      authorizationParams={{
        redirect_uri: window.location.origin
      }}
    >
      <>
        <App />
        <ToastContainer position="bottom-right" autoClose={3500} pauseOnHover />
      </>
    </Auth0Provider>
  </StrictMode>,
)
