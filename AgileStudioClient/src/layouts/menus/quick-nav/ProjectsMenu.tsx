import './ProjectsMenu.css'
import {type ReactElement, useContext, useState} from "react";
import type {ProjectDto} from "../../../api/dtos/projects/ProjectDtos.tsx";
import CurrentUserContext from "../../../services/CurrentUser.tsx";
import ProjectSubMenu from "./ProjectSubMenu.tsx";
import {faSpinner, faPlus} from "@fortawesome/free-solid-svg-icons";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import CreateProjectModal from "../../../modals/CreateProjectModal.tsx";
import {goToPage, linkToPage} from "../../../PageRouterUtils.tsx";
import CurrentPageContext from "../../../services/CurrentPage.tsx";
import {
  getProjectHomePagePath,
  getProjectPagePath, getProjectsPagePath,
  isCurrentPageBasePath,
} from "../../../PageRoutes.tsx";
import {getProjects} from "../../../api/endpoints/projects/Projects.tsx";

function ProjectsMenu() {
  const [isRefreshing, setIsRefreshing] = useState<boolean|null>(null);
  const [projects, setProjects] = useState<ProjectDto[]>([]);
  const [isCreateOpen, setIsCreateOpen] = useState(false);
  const currentUser = useContext(CurrentUserContext);
  const currentPage = useContext(CurrentPageContext);

  const refresh = () => {
    if(isRefreshing){
      return;
    }

    setIsRefreshing(true);

    getProjects()
      .then(response => {
        setProjects(response.data.items);
      })
      .catch(error => {
        console.error("Error fetching projects:", error);
      })
      .finally(() => {
        setIsRefreshing(false);
      });
  }

  const renderProject = (project:ProjectDto) => {
    const projectPagePath = getProjectPagePath(project.id);
    const projectHomePath = getProjectHomePagePath(project.id);
    const isActiveProject:boolean = isCurrentPageBasePath(currentPage, projectPagePath);

    const listItems:ReactElement[] = [];
    listItems.push(
      <a key={project.id}
         className={`list-group-item ${isActiveProject ? 'active' : ''}`}
         href={projectHomePath}
         onClick={linkToPage}>
        {project.title}
      </a>
    );

    if(isActiveProject){
      listItems.push(
        <li key={`${project.id}-sub-menu`} className={"list-group-item p-0"}>
          <ProjectSubMenu project={project}></ProjectSubMenu>
        </li>
      );
    }

    return listItems;
  }

  const handleCreated = (newProject: ProjectDto) => {
    setProjects(prev => [newProject, ...prev]);
    goToPage(getProjectHomePagePath(newProject.id));
  };

  if(isRefreshing === null && !currentUser.isLoading && currentUser.user){
    refresh();
  }

  return (
    <div className={"ProjectsMenu QuickNavMenu"}>
      <div className={"d-flex justify-content-between mb-2"}>
        <span><a href={getProjectsPagePath()} onClick={linkToPage}>Projects</a></span>
        <button className="btn btn-sm btn-secondary" onClick={() => setIsCreateOpen(true)} aria-label="Create project">
          <FontAwesomeIcon icon={faPlus} size={"sm"}></FontAwesomeIcon>
        </button>
      </div>

      { isRefreshing !== false && <FontAwesomeIcon icon={faSpinner} size={"lg"} spin={true}></FontAwesomeIcon> }

      {
        isRefreshing === false &&
        <ul className="list-group">
          {projects.map(project => renderProject(project))}
        </ul>
      }

      <CreateProjectModal
        isOpen={isCreateOpen}
        onClose={() => setIsCreateOpen(false)}
        onCreated={handleCreated}
      />
    </div>
  )
}

export default ProjectsMenu