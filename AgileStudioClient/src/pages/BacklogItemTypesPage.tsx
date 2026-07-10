import './BacklogItemTypesPage.css'
import {useEffect} from "react";
import Utils from "../Utils.tsx";
import BacklogItemTypesDataTable from "../data-tables/account/BacklogItemTypesDataTable.tsx";

function BacklogItemTypesPage() {
  useEffect(() => {
    Utils.setDocumentTitle("Backlog Item Types");
  }, []);

  return (
    <div className={"BacklogItemTypesPage"}>
      <div className="page-header d-flex align-items-center justify-content-between">
        <h1>Backlog Item Types</h1>
      </div>
      <BacklogItemTypesDataTable></BacklogItemTypesDataTable>
    </div>
  )
}

export default BacklogItemTypesPage
