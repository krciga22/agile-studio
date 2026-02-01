import './App.css'
import HomePage from "./pages/HomePage.tsx";
import AboutPage from "./pages/AboutPage.tsx";
import ErrorPage from "./pages/ErrorPage.tsx";
import InitPage from "./pages/InitPage.tsx";
import {useAuth0} from "@auth0/auth0-react";
import {useEffect, useState} from "react";
import LoginPage from "./pages/LoginPage.tsx";
import BacklogPage from "./pages/project/BacklogPage.tsx";
import SprintsPage from "./pages/project/SprintsPage.tsx";
import ReleasesPage from "./pages/project/ReleasesPage.tsx";
import SettingsPage from "./pages/project/SettingsPage.tsx";

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
  let pathSegments: string[] = pathname.split('/').toSpliced(0, 1);
  let subPath: string;
  if(pathname === "/"){
    page = <HomePage></HomePage>
  }
  else if(pathname === "/about"){
    page = <AboutPage></AboutPage>
  }
  else if(pathname.match(/\/projects\/\d+/)?.length === 1){
    const projectId = pathSegments[1];
    if(projectId !== null){
      subPath = pathSegments.toSpliced(0, 2).join('/');

      if(subPath === 'backlog'){
        page = <BacklogPage projectId={projectId}></BacklogPage>
      }
      else if(subPath === 'sprints'){
        page = <SprintsPage projectId={projectId}></SprintsPage>
      }
      else if(subPath === 'releases'){
        page = <ReleasesPage projectId={projectId}></ReleasesPage>
      }
      else if(subPath === 'settings'){
        page = <SettingsPage projectId={projectId}></SettingsPage>
      }
    }
  }

  if(page === undefined){
    page = <ErrorPage error={404}></ErrorPage>
  }

  return (
    <>
      {page}
    </>
  )
}

export default PageRouter
