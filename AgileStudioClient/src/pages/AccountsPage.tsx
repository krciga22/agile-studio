import './AccountsPage.css'
import {useEffect} from "react";
import Utils from "../Utils.tsx";
import AccountsDataTable from "../data-tables/AccountsDataTable.tsx";

function AccountsPage() {
  useEffect(() => {
    Utils.setDocumentTitle("Accounts");
  }, []);

  return (
    <div className={"AccountsPage"}>
      <div className="page-header d-flex align-items-center justify-content-between">
        <h1>Accounts</h1>
      </div>
      <AccountsDataTable></AccountsDataTable>
    </div>
  )
}

export default AccountsPage
