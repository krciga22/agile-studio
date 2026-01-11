import './App.css'
import Home from "./pages/Home.tsx";
import TopNav from "./TopNav.tsx";
import UserMenu from "./UserMenu.tsx";
import About from "./pages/About.tsx";

function App() {

  // basic page routing
  let page = <Home></Home>
  if(location.href.indexOf("/about") !== -1){
    page = <About></About>
  }

  return (
    <>
      <header className={"p-2"}>
        <div className={"d-flex justify-content-between"}>
          <div className={"d-flex"}>
            <span className={"appName me-3"}>Agile Studio</span>
            <TopNav></TopNav>
          </div>
          <div className={"d-flex"}>
            <UserMenu></UserMenu>
          </div>
        </div>
      </header>
      <div className={"p-2 content"}>
        {page}
      </div>
      <footer className={"p-2"}>
        Copyright &copy; 2026
      </footer>
    </>
  )
}

export default App
