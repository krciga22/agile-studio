import './App.css'
import HomePage from "./pages/HomePage.tsx";
import AboutPage from "./pages/AboutPage.tsx";
import ErrorPage from "./pages/ErrorPage.tsx";
import InitPage from "./pages/InitPage.tsx";
import {useAuth0} from "@auth0/auth0-react";
import {useEffect, useState} from "react";
import LoginPage from "./pages/LoginPage.tsx";

type CurrentPathAndState = {
  pathname: string,
  state: object|null
}

function PageRouter() {
  const [currentPathAndState, setCurrentPathAndState] = useState<CurrentPathAndState>({
    pathname: location.pathname,
    state: null
  });

  const auth0 = useAuth0();

  useEffect(() => {
    window.addEventListener('pushstate', (e:Event) => {
      setCurrentPathAndState({
        pathname: location.pathname,
        state: (e as CustomEvent).detail.state
      });
    });

    window.addEventListener('popstate', (e:PopStateEvent) => {
      setCurrentPathAndState({
        pathname: location.pathname,
        state: e.state
      });
    });
  }, []);

  const pathname = currentPathAndState.pathname;
  const state = currentPathAndState.state;

  console.debug(`PageRouter: pathname=${pathname}, state=${state}`);

  if(auth0.isLoading){
    return <InitPage></InitPage>
  }

  if(!auth0.isAuthenticated){
    return <LoginPage></LoginPage>
  }

  let page;
  if(pathname === "/"){
    page = <HomePage></HomePage>
  }
  else if(pathname === "/about"){
    page = <AboutPage></AboutPage>
  }
  else{
    page = <ErrorPage error={404}></ErrorPage>
  }

  return (
    <>
      {page}
    </>
  )
}

export default PageRouter
