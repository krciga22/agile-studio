import './ProjectSubMenu.css'
import type {ProjectDto} from "../../../services/api/dtos/ProjectDtos.tsx";
import {linkToPage} from "../../../PageRouterUtils.tsx";

type ProjectSubMenuProps = {
  Project: ProjectDto
}

function ProjectSubMenu(props:ProjectSubMenuProps) {
  const projectPage = `/projects/${props.Project.id}`;

  return (
    <ul className={"ProjectSubMenu"}>
      <li><a href={projectPage + "/backlog"} onClick={linkToPage}>Backlog</a></li>
      <li><a href={projectPage + "/sprints"} onClick={linkToPage}>Sprints</a></li>
      <li><a href={projectPage + "/releases"} onClick={linkToPage}>Releases</a></li>
      <li><a href={projectPage + "/settings"} onClick={linkToPage}>Settings</a></li>
    </ul>
  )
}

export default ProjectSubMenu