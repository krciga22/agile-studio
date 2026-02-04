import './ProjectSubMenu.css'
import type {ProjectDto} from "../../../services/api/dtos/ProjectDtos.tsx";
import {linkToPage} from "../../../PageRouterUtils.tsx";
import CurrentPageContext from "../../../services/CurrentPage.tsx";
import {useContext} from "react";

type ProjectSubMenuProps = {
  Project: ProjectDto
}

function ProjectSubMenu(props:ProjectSubMenuProps) {
  const projectPage = `/projects/${props.Project.id}`;
  const pageContext = useContext(CurrentPageContext);

  const activePageClassName = (subPath:string):string => {
    const pathname:string = pageContext.pathname ?? "";
    return pathname.startsWith(projectPage + subPath) ? "active" : "";
  }

  return (
    <ul className={"ProjectSubMenu QuickNavSubMenu"}>
      <li className={activePageClassName("/backlog")}>
        <a href={projectPage + "/backlog"} onClick={linkToPage}>Backlog</a>
      </li>
      <li className={activePageClassName("/sprints")}>
        <a href={projectPage + "/sprints"} onClick={linkToPage}>Sprints</a>
      </li>
      <li className={activePageClassName("/releases")}>
        <a href={projectPage + "/releases"} onClick={linkToPage}>Releases</a>
      </li>
      <li className={activePageClassName("/settings")}>
        <a href={projectPage + "/settings"} onClick={linkToPage}>Settings</a>
      </li>
    </ul>
  )
}

export default ProjectSubMenu