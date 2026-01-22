import './App.css'
import PageRouter from "./PageRouter.tsx";
import type {CurrentUserDto} from "./services/api/dtos/CurrentUserDto.tsx";
import {useEffect, useState} from "react";
import {getCurrentUser} from "./services/api/AuthEndpoints.tsx";
import CurrentUserContext from "./services/CurrentUser.tsx";
import type {AxiosError} from "axios";

const UserContext = CurrentUserContext;

function App() {
  const [currentUser, setCurrentUser] = useState<CurrentUserDto|null>(null);
  const [currentUserError, setCurrentUserError] = useState<AxiosError|null>(null);
  const [isCurrentUserLoading, setIsCurrentUserLoading] = useState<boolean>(true);

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
        setIsCurrentUserLoading(false);
      });
  }, []);

  return (
    <>
      <UserContext value={{
        user: currentUser,
        error: currentUserError,
        isLoading: isCurrentUserLoading
      }}>
        <PageRouter></PageRouter>
      </UserContext>
    </>
  )
}

export default App
