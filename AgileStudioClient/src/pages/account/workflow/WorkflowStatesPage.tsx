import './WorkflowStatesPage.css'
import {useContext, useEffect, useState} from "react";
import Utils from "../../../Utils.tsx";
import type {AccountDto} from "../../../api/dtos/accounts/AccountDtos.tsx";
import CurrentUserContext from "../../../services/CurrentUser.tsx";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faSpinner} from "@fortawesome/free-solid-svg-icons";
import {getAccount} from "../../../api/endpoints/accounts/Accounts.tsx";
import WorkflowNav from "./WorkflowNav.tsx";
import {getWorkflow} from "../../../api/endpoints/accounts/Workflows.tsx";
import type {WorkflowDto} from "../../../api/dtos/accounts/WorkflowDtos.tsx";
import WorkflowBreadcrumbs from "./WorkflowBreadcrumbs.tsx";

type WorkflowStatesPageProps = {
  workflowId: number
}

function WorkflowStatesPage(props: WorkflowStatesPageProps) {
  const {workflowId} = props;
  const [isRefreshing, setIsRefreshing] = useState<boolean|null>(null);
  const [workflow, setWorkflow] = useState<WorkflowDto|null>(null);
  const [account, setAccount] = useState<AccountDto|null>(null);
  const currentUser = useContext(CurrentUserContext);

  useEffect(() => {
    if(account && workflow){
      Utils.setDocumentTitle(`Workflow States - ${workflow.title}`);
    }
  }, [account, workflow]);

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

  if(!isRefreshing && (workflow === null || workflow.id !== workflowId) && !currentUser.isLoading && currentUser.user){
    refresh();
  }

  return (
    <div className={"WorkflowStatesPage"}>
      { isRefreshing !== false && <FontAwesomeIcon icon={faSpinner} size={"lg"} spin={true}></FontAwesomeIcon> }

      {
        isRefreshing === false && account && workflow &&
          <div>
              <WorkflowBreadcrumbs account={account} workflow={workflow} />

              <WorkflowNav account={account} workflow={workflow}></WorkflowNav>

              <p>Show workflow states...</p>
          </div>
      }
    </div>
  )
}

export default WorkflowStatesPage;
