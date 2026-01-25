import './TopNav.css'
import {linkToPage} from "../../../PageRouterUtils.tsx";

function TopNav() {
  return (
    <nav className={"TopNav"}>
      <ul className={"d-flex m-0 p-0"}>
        <li className={"px-1"}><a href={"/"} onClick={linkToPage}>Home</a></li>
        <li className={"px-1"}><a href={"/about"} onClick={linkToPage}>About</a></li>
      </ul>
    </nav>
  )
}

export default TopNav
