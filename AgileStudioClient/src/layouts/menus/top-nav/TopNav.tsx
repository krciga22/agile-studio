import './TopNav.css'
import {linkToPage} from "../../../PageRouterUtils.tsx";
import {
  getAboutPagePath,
  getHomePagePath,
} from "../../../PageRoutes.tsx";

function TopNav() {
  return (
    <nav className={"TopNav"}>
      <ul className={"d-flex m-0 p-0"}>
        <li className={"px-1"}><a href={getHomePagePath()} onClick={linkToPage}>Home</a></li>
        <li className={"px-1"}><a href={getAboutPagePath()} onClick={linkToPage}>About</a></li>
      </ul>
    </nav>
  )
}

export default TopNav
