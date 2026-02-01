import './LoginPage.css'
import BlankLayout from "../layouts/BlankLayout.tsx";
import Constants from "../Constants.tsx";
import {useAuth0} from "@auth0/auth0-react";
import {setApiAuthBearerToken} from "../services/api/Api.tsx";
import {goToPage} from "../PageRouterUtils.tsx";
import ENV from "../config/ENV.tsx";

function LoginPage() {
  const auth0 = useAuth0();

  const doLogin = async () => {
    await auth0.loginWithPopup({
      authorizationParams: {
        audience: ENV.AUTH0_AUDIENCE,
        scope: "openid profile email"
      }
    });
    const accessToken = await auth0.getAccessTokenSilently();
    setApiAuthBearerToken(accessToken);
    goToPage('/');
  };

  return (
    <div className={"LoginPage d-flex"}>
      <div className="card">
        <div className="card-header">
          {Constants.PRODUCT_NAME}
        </div>
        <div className="card-body">
          <div className={"d-flex justify-content-center mb-3"}>
            <a className="btn btn-primary mx-2" onClick={doLogin}>Log In</a>
          </div>
        </div>
      </div>
    </div>
  )
}

export default LoginPage
