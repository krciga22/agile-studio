import './ProjectsPage.css'
import {useEffect} from "react";
import Utils from "../Utils.tsx";
import ProjectsDataTable from "../data-tables/ProjectsDataTable.tsx";

function ProjectsPage() {
  useEffect(() => {
    Utils.setDocumentTitle("Projects");
  }, []);

  return (
    <div className={"ProjectsPage"}>
      <div className="page-header d-flex align-items-center justify-content-between">
        <h1>Projects</h1>
      </div>
      <ProjectsDataTable></ProjectsDataTable>
    </div>
  )
}

export default ProjectsPage
