import './LoginPage.css'
import Constants from "../Constants.tsx";
import {useAuth0} from "@auth0/auth0-react";
import {goToPage} from "../PageRouterUtils.tsx";
import AuthService from "../services/AuthService.tsx";

function LoginPage() {
  const auth0 = useAuth0();

  const doLogin = async () => {
    const accessToken = await AuthService.loginWithPopup(auth0);
    if(accessToken){
      goToPage('/');
    }
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
