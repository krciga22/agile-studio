import './App.css'
import Home from "./pages/Home.tsx";
import About from "./pages/About.tsx";
import ErrorPage from "./pages/ErrorPage.tsx";

function PageRouter() {

  // basic page routing
  let page = <Home></Home>
  if(location.pathname === "/"){
    page = <Home></Home>
  }
  else if(location.pathname === "/about"){
    page = <About></About>
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
