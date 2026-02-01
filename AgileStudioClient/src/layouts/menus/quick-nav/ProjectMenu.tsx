import './ProjectMenu.css'
import {type ReactElement, useContext, useState} from "react";
import {getProjects} from "../../../services/api/endpoints/project.tsx";
import type {ProjectDto} from "../../../services/api/dtos/ProjectDtos.tsx";
import CurrentUserContext from "../../../services/CurrentUser.tsx";
import ProjectSubMenu from "./ProjectSubMenu.tsx";
import {faSpinner} from "@fortawesome/free-solid-svg-icons";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";

function ProjectsMenu() {
  const [isRefreshing, setIsRefreshing] = useState<boolean|null>(null);
  const [projects, setProjects] = useState<ProjectDto[]>([]);
  const [activeProject, setActiveProject] = useState<ProjectDto>(null);
  const currentUser = useContext(CurrentUserContext);

  const refresh = () => {
    if(isRefreshing){
      return;
    }

    setIsRefreshing(true);

    getProjects()
      .then(response => {
        setProjects(response.data);

        if(response.data.length > 0){
          setActiveProject(response.data[0]);
        }
      })
      .catch(error => {
        console.error("Error fetching projects:", error);
      })
      .finally(() => {
        setIsRefreshing(false);
      });
  }

  const renderProject = (project:ProjectDto) => {
    const isActiveProject:boolean = (project.id === activeProject?.id);
    const href = `/projects/${project.id}`

    const listItems:ReactElement[] = [];
    listItems.push(
      <a key={project.id} className={`list-group-item ${isActiveProject ? 'active' : ''}`} href={href}>
        {project.title}
      </a>
    );

    if(isActiveProject){
      listItems.push(
        <li key={`${project.id}-sub-menu`} className={"list-group-item"}>
          <ProjectSubMenu Project={project}></ProjectSubMenu>
        </li>
      );
    }

    return listItems;
  }

  if(isRefreshing === null && !currentUser.isLoading && currentUser.user){
    refresh();
  }

  return (
    <div className={"ProjectsMenu"}>
      <p>Projects:</p>
      { isRefreshing !== false && <FontAwesomeIcon icon={faSpinner} size={"lg"} spin={true}></FontAwesomeIcon> }

      {
        isRefreshing === false &&
        <ul className="list-group">
          {projects.map(project => renderProject(project))}
        </ul>
      }
    </div>
  )
}

export default ProjectsMenu