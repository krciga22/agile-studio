// ...existing code...
import './ProjectsPage.css'
import {useEffect, useState, useContext} from "react";
import {getProjects} from "../services/api/endpoints/project.tsx";
import type {ProjectDto} from "../services/api/dtos/ProjectDtos.tsx";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faSpinner} from "@fortawesome/free-solid-svg-icons";
import Utils from "../Utils.tsx";
import CurrentUserContext from "../services/CurrentUser.tsx";
import {goToPage, linkToPage} from "../PageRouterUtils.tsx";
import {getProjectHomePagePath} from "../PageRoutes.tsx";

function ProjectsPage() {
  const [isRefreshing, setIsRefreshing] = useState<boolean|null>(null);
  const [projects, setProjects] = useState<ProjectDto[]>([]);
  const currentUser = useContext(CurrentUserContext);

  useEffect(() => {
    Utils.setDocumentTitle("Projects");
  }, []);

  const refresh = () => {
    if(isRefreshing){
      return;
    }

    setIsRefreshing(true);

    getProjects()
      .then(response => {
        setProjects(response.data);
      })
      .catch(error => {
        console.error("Error fetching projects:", error);
      })
      .finally(() => {
        setIsRefreshing(false);
      });
  }

  if(projects.length === 0 && !currentUser.isLoading && currentUser.user){
    refresh();
  }

  const openProject = (projectId:number) => {
    goToPage(getProjectHomePagePath(projectId));
  }

  const getCreatorName = (createdBy: any): string => {
    if(!createdBy) return '';
    const name = `${createdBy.firstName ?? ''} ${createdBy.lastName ?? ''}`.trim();
    return name || `User ${createdBy.id}`;
  }

  const formatDate = (iso?: string): string => {
    if(!iso) return '';
    const d = new Date(iso);
    if(isNaN(d.getTime())) return iso;
    return d.toLocaleString();
  }

  return (
    <div className={"ProjectsPage"}>
      <div className="page-header d-flex align-items-center justify-content-between">
        <h1>Projects</h1>
        <div>
          {isRefreshing && <FontAwesomeIcon icon={faSpinner} spin />}
        </div>
      </div>

      <div className="projects-list">
        {projects.length === 0 && !isRefreshing &&
          <div className="empty">No projects found.</div>
        }

        <div className="table-responsive">
          <table className="table table-hover mb-0">
            <thead>
              <tr>
                <th style={{width: '10%'}}>ID</th>
                <th style={{width: '40%'}}>Title</th>
                <th style={{width: '30%'}}>Creator</th>
                <th style={{width: '20%'}}>Date created</th>
              </tr>
            </thead>
            <tbody>
              {projects.map(p => (
                <tr key={p.id} style={{cursor: 'pointer'}} onClick={() => openProject(p.id)}>
                  <td>{p.id}</td>
                  <td>
                    <a href={getProjectHomePagePath(p.id)} onClick={(e) => { e.stopPropagation(); linkToPage(e); }}>
                      {p.title ?? `Project ${p.id}`}
                    </a>
                  </td>
                  <td>{getCreatorName(p.createdBy)}</td>
                  <td>{formatDate(p.createdOn)}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
       </div>
     </div>
   )
 }

 export default ProjectsPage
