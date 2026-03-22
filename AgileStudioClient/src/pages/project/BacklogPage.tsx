import './BacklogPage.css'
import {useContext, useEffect, useState} from "react";
import Utils from "../../Utils.tsx";
import type {ProjectDto} from "../../services/api/dtos/ProjectDtos.tsx";
import CurrentUserContext from "../../services/CurrentUser.tsx";
import {getResource} from "../../services/api/endpoints/resource.tsx";
import ResourceTypes from "../../services/api/ResourceTypes.tsx";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faSpinner} from "@fortawesome/free-solid-svg-icons";

type BacklogPageProps = {
  projectId: number
}

function BacklogPage(props: BacklogPageProps) {
  const {projectId} = props;
  const [isRefreshing, setIsRefreshing] = useState<boolean|null>(null);
  const [project, setProject] = useState<ProjectDto|null>(null);
  const currentUser = useContext(CurrentUserContext);

  useEffect(() => {
    if(project){
      Utils.setDocumentTitle(`Backlog - ${project.title}`);
    }
  }, [project]);

  const refresh = () => {
    if(isRefreshing){
      return;
    }

    setIsRefreshing(true);

    getResource<ProjectDto>(ResourceTypes.ProjectsProject, projectId)
      .then(response => {
        setProject(response.data);
      })
      .catch(error => {
        console.error("Error refreshing", error);
      })
      .finally(() => {
        setIsRefreshing(false);
      });
  }

  if((project === null || project.id !== projectId) && !currentUser.isLoading && currentUser.user){
    refresh();
  }

  return (
    <div className={"BacklogPage"}>
      { isRefreshing !== false && <FontAwesomeIcon icon={faSpinner} size={"lg"} spin={true}></FontAwesomeIcon> }

      {
        isRefreshing === false && project &&
          <div>
              <h1>Backlog</h1>
              <p>{project.title}</p>
          </div>
      }
    </div>
  )
}

export default BacklogPage
