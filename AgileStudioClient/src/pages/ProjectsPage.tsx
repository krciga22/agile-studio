import './ProjectsPage.css'
import {useEffect, useState, useContext, useRef} from "react";
import {getProjects} from "../services/api/endpoints/project.tsx";
import type {ProjectDto} from "../services/api/dtos/ProjectDtos.tsx";
import type {UserSummaryDto} from "../services/api/dtos/UserDtos.tsx";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faSpinner} from "@fortawesome/free-solid-svg-icons";
import Utils, {debounce} from "../Utils.tsx";
import CurrentUserContext from "../services/CurrentUser.tsx";
import {linkToPage} from "../PageRouterUtils.tsx";
import {getProjectHomePagePath} from "../PageRoutes.tsx";
import DataTable, {type DataTableColumn} from "../components/data-table/DataTable";
import Constants from "../Constants.tsx";

function ProjectsPage() {
  const [isRefreshing, setIsRefreshing] = useState<boolean|undefined>();
  const [projects, setProjects] = useState<ProjectDto[]>([]);
  const currentUser = useContext(CurrentUserContext);
  const refreshTimeoutRef = useRef<number|null>(null);

  useEffect(() => {
    Utils.setDocumentTitle("Projects");
  }, []);

  const refresh = () => {
    if(isRefreshing){
      return;
    }

    setIsRefreshing(true);

    debounce(async () => {
      try{
        const response = await getProjects();
        setProjects(response.data);
      }
      catch(error){
        console.error("Error fetching projects:", error);
      }
      finally {
        setIsRefreshing(false);
      }
    }, Constants.EXTRA_WAIT_TIME_MS, refreshTimeoutRef);
  }

  const getCreatorName = (createdBy: UserSummaryDto|undefined): string => {
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

  const columns: DataTableColumn<ProjectDto>[] = [
    {
      key: 'id',
      field: 'id',
      header: 'ID',
      width: '10%'
    },
    {
      key: 'title',
      header: 'Title',
      render: (p) => (
        <a href={getProjectHomePagePath(p.id)} onClick={linkToPage}>{p.title ?? `Project ${p.id}`}</a>
      ),
      width: '40%'
    },
    {
      key: 'creator',
      header: 'Creator',
      render: (p) => getCreatorName(p.createdBy),
      width: '30%'},
    {
      key: 'createdOn',
      header: 'Date created',
      render: (p) => formatDate(p.createdOn),
      width: '20%'}
  ];

  if(projects.length === 0 && !currentUser.isLoading && currentUser.user){
    refresh();
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
        <DataTable
          columns={columns}
          data={projects}
          isLoading={isRefreshing}
          emptyMessage={projects.length === 0 ? 'No projects found.' : undefined}
          rowKey={(p) => p.id}
        />
      </div>
    </div>
  )
}

export default ProjectsPage
