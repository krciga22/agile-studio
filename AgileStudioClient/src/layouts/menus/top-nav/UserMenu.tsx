import './UserMenu.css'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faUser, faWarning, faSpinner } from '@fortawesome/free-solid-svg-icons'
import ENV from "../../../config/ENV.tsx";
import {useEffect, useState} from "react";
import {getCurrentUser} from "../../../services/api/AuthEndpoints.tsx";
import type {CurrentUserDto} from "../../../services/api/dtos/CurrentUserDto.tsx";
import type {AxiosError} from "axios";

function UserMenu() {
  const [currentUser, setCurrentUser] = useState<CurrentUserDto|null>(null);
  const [currentUserError, setCurrentUserError] = useState<AxiosError|null>(null);
  const [isLoading, setIsLoading] = useState<boolean>(true);

  useEffect(() => {
    getCurrentUser()
      .then(response => {
        console.debug('Current user', response.data);
        setCurrentUser(response.data);
      })
      .catch((error) => {
        console.error(error);
        setCurrentUserError(error);
      })
      .finally(() => {
        setIsLoading(false);
      });
  }, []);

  const renderUser = () => {
    return (
      <>
        {
          !currentUser && !currentUserError &&
          <>
              <FontAwesomeIcon icon={faUser} size={"lg"}></FontAwesomeIcon>
              <a href={`${ENV.API_URL}/Auth/Login`}>Login</a>
          </>
        }

        {
          currentUser &&
          <>
              <FontAwesomeIcon icon={faUser} size={"lg"}></FontAwesomeIcon>
              <span>{currentUser.name}</span>
          </>
        }

        {
          currentUserError &&
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
