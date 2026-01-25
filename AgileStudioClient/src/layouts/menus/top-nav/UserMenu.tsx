import './UserMenu.css'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faUser, faWarning, faSpinner } from '@fortawesome/free-solid-svg-icons'
import {useContext} from "react";
import CurrentUserContext from "../../../services/CurrentUser.tsx";
import {useAuth0} from "@auth0/auth0-react";
import {setApiAuthBearerToken} from "../../../services/api/Api.tsx";
import {goToPage} from "../../../PageRouterUtils.tsx";

function UserMenu() {
  const {user, error, isLoading} = useContext(CurrentUserContext);
  const auth0 = useAuth0();

  const doLogin = async () => {
    await auth0.loginWithPopup();
    const accessToken = await auth0.getAccessTokenSilently();
    setApiAuthBearerToken(accessToken);
    goToPage('/');
  };

  const doLogout = async () => {
    await auth0.logout()
    setApiAuthBearerToken(null);
  };

  const renderUser = () => {
    return (
      <>
        {
          !user && !error &&
          <>
              <FontAwesomeIcon icon={faUser} size={"lg"}></FontAwesomeIcon>
              <a onClick={doLogin}>Login</a>
          </>
        }

        {
          user &&
          <>
              <FontAwesomeIcon icon={faUser} size={"lg"}></FontAwesomeIcon>
              <span className={"text-nowrap"}>{user.name}</span>
              <a className={"text-nowrap ms-2"} onClick={doLogout}>Logout</a>
          </>
        }

        {
          error &&
          <>
              <FontAwesomeIcon icon={faWarning} size={"lg"}></FontAwesomeIcon>
              <span>Error</span>
          </>
        }
      </>
    );
  };

  return (
    <div className={"UserMenu d-flex"}>
      { isLoading && <FontAwesomeIcon icon={faSpinner} size={"lg"} spin={true}></FontAwesomeIcon> }

      { !isLoading && renderUser() }
    </div>
  )
}

export default UserMenu
