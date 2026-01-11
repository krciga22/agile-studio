import './App.css'
import Home from "./pages/Home.tsx";
import About from "./pages/About.tsx";

function PageRouter() {

  // basic page routing
  let page = <Home></Home>
  if(location.href.indexOf("/about") !== -1){
    page = <About></About>
  }

  return (
    <>
      {page}
    </>
  )
}

export default PageRouter
