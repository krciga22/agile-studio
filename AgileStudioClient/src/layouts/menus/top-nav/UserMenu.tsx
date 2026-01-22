import './UserMenu.css'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faUser, faWarning, faSpinner } from '@fortawesome/free-solid-svg-icons'
import ENV from "../../../config/ENV.tsx";
import {useContext} from "react";
import CurrentUserContext from "../../../services/CurrentUser.tsx";

function UserMenu() {
  const {user, error, isLoading} = useContext(CurrentUserContext);

  const renderUser = () => {
    return (
      <>
        {
          !user && !error &&
          <>
              <FontAwesomeIcon icon={faUser} size={"lg"}></FontAwesomeIcon>
              <a href={`${ENV.API_URL}/Auth/Login`}>Login</a>
          </>
        }

        {
          user &&
          <>
              <FontAwesomeIcon icon={faUser} size={"lg"}></FontAwesomeIcon>
              <span>{user.name}</span>
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
