import './App.css'
import TopNav from "./TopNav.tsx";
import UserMenu from "./UserMenu.tsx";
import PageRouter from "./PageRouter.tsx";

function App() {
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
        <PageRouter></PageRouter>
      </div>
      <footer className={"p-2"}>
        Copyright &copy; 2026
      </footer>
    </>
  )
}

export default App
