import './MainLayout.css'
import type {ReactNode} from "react";
import Header from "./headers/Header.tsx";
import Footer from "./footers/Footer.tsx";
import QuickNav from "./menus/quick-nav/QuickNav.tsx";

type MainLayoutProps = {
  children: ReactNode
}

function MainLayout(props: MainLayoutProps) {
  return (
    <>
      <Header></Header>

      <div className={"d-flex flex-grow-1 flex-shrink-2 overflow-hidden"}>
        <QuickNav></QuickNav>
        <div className={"content flex-grow-1 overflow-scroll py-3 pe-4"}>
          {props.children}
        </div>
      </div>

      <Footer></Footer>
    </>
  )
}

export default MainLayout
