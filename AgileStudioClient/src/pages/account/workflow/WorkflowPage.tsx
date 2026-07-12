import './WorkflowPage.css'
import {useContext, useEffect, useState} from "react";
import Utils from "../../../Utils.tsx";
import CurrentUserContext from "../../../services/CurrentUser.tsx";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faSpinner} from "@fortawesome/free-solid-svg-icons";
import type {WorkflowDto} from "../../../api/dtos/accounts/WorkflowDtos.tsx";
import {getWorkflow} from "../../../api/endpoints/accounts/Workflows.tsx";
import type {AccountDto} from "../../../api/dtos/accounts/AccountDtos.tsx";
import {getAccount} from "../../../api/endpoints/accounts/Accounts.tsx";
import WorkflowNav from "./WorkflowNav.tsx";
import WorkflowBreadcrumbs from "./WorkflowBreadcrumbs.tsx";

type WorkflowPageProps = {
  workflowId: number
}

function WorkflowPage(props: WorkflowPageProps) {
  const {workflowId} = props;
  const [isRefreshing, setIsRefreshing] = useState<boolean|null>(null);
  const [workflow, setWorkflow] = useState<WorkflowDto|null>(null);
  const [account, setAccount] = useState<AccountDto|null>(null);
  const currentUser = useContext(CurrentUserContext);

  useEffect(() => {
    if(workflow){
      Utils.setDocumentTitle(`Workflow - ${workflow.title}`);
    }
  }, [workflow]);

  const refresh = async () => {
    if(isRefreshing){
      return;
    }

    setIsRefreshing(true);

    try{
      const workflowResponse = await getWorkflow(workflowId);
      setWorkflow(workflowResponse.data);

      const accountResponse = await getAccount(workflowResponse.data.account.id);
      setAccount(accountResponse.data);
    }
    catch(error){
      console.error("Error refreshing", error);
    }
    finally {
      setIsRefreshing(false);
    }
  }

  if((workflow === null || workflow.id !== workflowId) && !currentUser.isLoading && currentUser.user){
    refresh();
  }

  return (
    <div className={"WorkflowPage"}>
      { isRefreshing !== false && <FontAwesomeIcon icon={faSpinner} size={"lg"} spin={true}></FontAwesomeIcon> }

      {
        isRefreshing === false && account && workflow &&
          <div>
              <WorkflowBreadcrumbs account={account} workflow={workflow} />

              <WorkflowNav account={account} workflow={workflow} />

              <p>Show workflow details...</p>
          </div>
      }
    </div>
  )
}

export default WorkflowPage;
