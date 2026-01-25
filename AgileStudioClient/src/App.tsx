import './App.css'
import PageRouter from "./PageRouter.tsx";
import type {CurrentUserDto} from "./services/api/dtos/CurrentUserDto.tsx";
import {useState} from "react";
import type {AxiosError} from "axios";
import {useAuth0} from "@auth0/auth0-react";
import {getCurrentUser} from "./services/api/AuthEndpoints.tsx";
import {initApiAuthBearerToken} from "./services/api/Api.tsx";
import CurrentUserContext from "./services/CurrentUser.tsx";

function App() {
  const [isAccessTokenInit, setIsAccessTokenInit] = useState<boolean>(false);
  const [currentUser, setCurrentUser] = useState<CurrentUserDto|null>(null);
  const [currentUserError, setCurrentUserError] = useState<AxiosError|null>(null);
  const [isCurrentUserLoading, setIsCurrentUserLoading] = useState<boolean>(true);

  const auth0 = useAuth0();

  if(!isAccessTokenInit && !auth0.isLoading && auth0.isAuthenticated) {
    setIsAccessTokenInit(true);
    initApiAuthBearerToken(auth0).then(() => {
      console.info("Getting Current User");

      getCurrentUser()
        .then((response) => {
          console.info('Current user', response.data.name);
          setCurrentUser(response.data);
        })
        .catch((error) => {
          console.error("Error getting current user", error);
          setCurrentUserError(error);
        })
        .finally(() => {
          setIsCurrentUserLoading(false);
        });
    });
  }

  return (
    <>
      <CurrentUserContext value={{
        user: currentUser,
        error: currentUserError,
        isLoading: isCurrentUserLoading
      }}>
        <PageRouter></PageRouter>
      </CurrentUserContext>
    </>
  )
}

export default App
