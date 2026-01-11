import './MainLayout.css'
import TopNav from "../TopNav.tsx";
import UserMenu from "../UserMenu.tsx";
import type {ReactNode} from "react";

type MainLayoutProps = {
  children: ReactNode
}

function MainLayout(props: MainLayoutProps) {
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
        {props.children}
      </div>
      <footer className={"p-2"}>
        Copyright &copy; 2026
      </footer>
    </>
  )
}

export default MainLayout
