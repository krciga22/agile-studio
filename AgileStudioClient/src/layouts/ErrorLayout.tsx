import './ErrorLayout.css'
import type {ReactNode} from "react";
import Header from "./headers/Header.tsx";

type ErrorLayoutProps = {
  children: ReactNode,
  error: number
}

function ErrorLayout(props: ErrorLayoutProps) {
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

export default ErrorLayout
