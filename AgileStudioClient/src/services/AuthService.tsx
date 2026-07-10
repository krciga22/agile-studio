import ENV from "../config/ENV.tsx";
import {type Auth0ContextInterface} from "@auth0/auth0-react/src/auth0-context.tsx";
import {User} from "@auth0/auth0-spa-js/src/global.ts";
import {setApiAuthBearerToken} from "../api/Api.tsx";

const AuthService = {
  loginWithPopup: async (auth0: Auth0ContextInterface<User>): Promise<string|null> => {
    await auth0.loginWithPopup({
      authorizationParams: {
        audience: ENV.AUTH0_AUDIENCE,
        scope: "openid profile email"
      }
    });

    if(!auth0.isAuthenticated){
      return null;
    }

    const accessToken = await auth0.getAccessTokenSilently({
      authorizationParams: {
        audience: ENV.AUTH0_AUDIENCE,
        scope: "openid profile email"
      }
    });

    setApiAuthBearerToken(accessToken);

    return accessToken;
  },
};

export default AuthService;