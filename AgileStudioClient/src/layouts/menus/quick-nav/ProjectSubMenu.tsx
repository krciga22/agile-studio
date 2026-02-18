import './ProjectSubMenu.css'
import type {ProjectDto} from "../../../services/api/dtos/ProjectDtos.tsx";
import {linkToPage} from "../../../PageRouterUtils.tsx";
import CurrentPageContext from "../../../services/CurrentPage.tsx";
import {useContext} from "react";
import {
  getProjectBacklogPagePath,
  getProjectReleasesPagePath,
  getProjectSettingsPagePath,
  getProjectSprintsPagePath,
  isCurrentPageBasePath,
} from "../../../PageRoutes.tsx";

type ProjectSubMenuProps = {
  project: ProjectDto
}

function ProjectSubMenu(props:ProjectSubMenuProps) {
  const {project} = props;
  const pageContext = useContext(CurrentPageContext);

  const activePageClassName = (pagePath:string):string => {
    return isCurrentPageBasePath(pageContext, pagePath) ? "active" : "";
  }

  const backlogPagePath = getProjectBacklogPagePath(project.id);
  const sprintsPagePath = getProjectSprintsPagePath(project.id);
  const releasesPagePath = getProjectReleasesPagePath(project.id);
  const settingsPagePath = getProjectSettingsPagePath(project.id);

  return (
    <ul className={"ProjectSubMenu QuickNavSubMenu"}>
      <li className={activePageClassName(backlogPagePath)}>
        <a href={backlogPagePath} onClick={linkToPage}>Backlog</a>
      </li>
      <li className={activePageClassName(sprintsPagePath)}>
        <a href={sprintsPagePath} onClick={linkToPage}>Sprints</a>
      </li>
      <li className={activePageClassName(releasesPagePath)}>
        <a href={releasesPagePath} onClick={linkToPage}>Releases</a>
      </li>
      <li className={activePageClassName(settingsPagePath)}>
        <a href={settingsPagePath} onClick={linkToPage}>Settings</a>
      </li>
    </ul>
  )
}

export default ProjectSubMenu