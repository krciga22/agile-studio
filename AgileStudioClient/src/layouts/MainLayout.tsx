import './MainLayout.css'
import type {ReactNode} from "react";
import Header from "./headers/Header.tsx";

type MainLayoutProps = {
  children: ReactNode
}

function MainLayout(props: MainLayoutProps) {
  return (
    <>
      <Header></Header>
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
