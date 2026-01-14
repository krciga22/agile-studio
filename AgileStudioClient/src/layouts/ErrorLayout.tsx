import './ErrorLayout.css'
import type {ReactNode} from "react";
import Header from "./headers/Header.tsx";
import Footer from "./footers/Footer.tsx";

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
      <Footer></Footer>
    </>
  )
}

export default ErrorLayout
