import './Header.css'
import TopNav from "../../layouts/menus/top-nav/TopNav.tsx";
import UserMenu from "../../layouts/menus/top-nav/UserMenu.tsx";
import Constants from "../../Constants.tsx";
import SettingsMenu from "../../layouts/menus/top-nav/SettingsMenu.tsx";

function Header() {
  return (
    <header className={"p-2"}>
      <div className={"d-flex flex-wrap justify-content-between"}>
        <div className={"d-flex"}>
          <span className={"appName me-3"}>{Constants.PRODUCT_NAME}</span>
          <TopNav></TopNav>
        </div>
        <div className={"d-flex"}>
          <div className={"container-fluid"}>
            <div className={"row"}>
              <div className={"col"}>
                <UserMenu></UserMenu>
              </div>
              <div className={"col"}>
                <SettingsMenu></SettingsMenu>
              </div>
            </div>
          </div>
        </div>
      </div>
    </header>
  )
}

export default Header
