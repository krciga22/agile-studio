import './App.css'
import HomePage from "./pages/HomePage.tsx";
import AboutPage from "./pages/AboutPage.tsx";
import ErrorPage from "./pages/ErrorPage.tsx";

function PageRouter() {

  // basic page routing
  let page = <HomePage></HomePage>
  if(location.pathname === "/"){
    page = <HomePage></HomePage>
  }
  else if(location.pathname === "/about"){
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
