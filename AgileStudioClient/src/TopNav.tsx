import './TopNav.css'

function TopNav() {
  return (
    <nav className={"TopNav"}>
      <ul className={"d-flex m-0 p-0"}>
        <li className={"px-1"}><a href={"/"}>Home</a></li>
        <li className={"px-1"}><a href={"/about"}>About</a></li>
      </ul>
    </nav>
  )
}

export default TopNav
