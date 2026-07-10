import './QuickNav.css'
import ProjectMenu from "./ProjectsMenu.tsx";
import AccountsMenu from "./AccountsMenu.tsx";

function QuickNav() {
  return (
    <nav className={"QuickNav flex-shrink-1 overflow-scroll me-sm-4 p-3"}>
      <div className={"mb-2"}>
        <AccountsMenu></AccountsMenu>
      </div>
      <div className={"mb-2"}>
        <ProjectMenu></ProjectMenu>
      </div>
    </nav>
  )
}

export default QuickNav
