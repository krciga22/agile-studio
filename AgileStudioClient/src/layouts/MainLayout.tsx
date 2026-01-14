import './MainLayout.css'
import type {ReactNode} from "react";
import Header from "./headers/Header.tsx";
import Footer from "./footers/Footer.tsx";

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
      <Footer></Footer>
    </>
  )
}

export default MainLayout
