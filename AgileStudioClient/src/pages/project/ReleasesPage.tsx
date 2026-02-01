import './ReleasesPage.css'
import MainLayout from "../../layouts/MainLayout.tsx";
import {useContext, useEffect, useState} from "react";
import Utils from "../../Utils.tsx";
import type {ProjectDto} from "../../services/api/dtos/ProjectDtos.tsx";
import CurrentUserContext from "../../services/CurrentUser.tsx";
import {getProject} from "../../services/api/endpoints/project.tsx";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faSpinner} from "@fortawesome/free-solid-svg-icons";

type ReleasesPageProps = {
  projectId: number
}

function ReleasesPage(props: ReleasesPageProps) {
  const [isRefreshing, setIsRefreshing] = useState<boolean|null>(null);
  const [project, setProject] = useState<ProjectDto>(null);
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

    getProject(props.projectId)
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

  if(isRefreshing === null && !currentUser.isLoading && currentUser.user){
    refresh();
  }

  return (
    <MainLayout>
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
    </MainLayout>
  )
}

export default ReleasesPage
