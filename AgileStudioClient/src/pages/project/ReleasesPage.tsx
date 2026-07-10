import './ReleasesPage.css'
import {useContext, useEffect, useState} from "react";
import Utils from "../../Utils.tsx";
import type {ProjectDto} from "../../api/dtos/projects/ProjectDtos.tsx";
import CurrentUserContext from "../../services/CurrentUser.tsx";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faSpinner} from "@fortawesome/free-solid-svg-icons";
import {getProject} from "../../api/endpoints/projects/Projects.tsx";

type ReleasesPageProps = {
  projectId: number
}

function ReleasesPage(props: ReleasesPageProps) {
  const {projectId} = props;
  const [isRefreshing, setIsRefreshing] = useState<boolean|null>(null);
  const [project, setProject] = useState<ProjectDto|null>(null);
  const currentUser = useContext(CurrentUserContext);

  useEffect(() => {
    if(project){
      Utils.setDocumentTitle(`Releases - ${project.title}`);
    }
  }, [project]);

  const refresh = () => {
    if(isRefreshing){
      return;
    }

    setIsRefreshing(true);

    getProject(projectId)
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
    <div className={"ReleasesPage"}>
      { isRefreshing !== false && <FontAwesomeIcon icon={faSpinner} size={"lg"} spin={true}></FontAwesomeIcon> }

      {
        isRefreshing === false && project &&
          <div>
              <h1>Releases</h1>
              <p>{project.title}</p>
          </div>
      }
    </div>
  )
}

export default ReleasesPage
