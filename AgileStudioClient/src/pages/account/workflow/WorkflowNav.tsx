import './WorkflowPage.css'
import {
  getAccountWorkflowPagePath,
  getAccountWorkflowStatesPagePath, isCurrentPagePath
} from "../../../PageRoutes.tsx";
import {goToPage} from "../../../PageRouterUtils.tsx";
import type {WorkflowDto} from "../../../api/dtos/accounts/WorkflowDtos.tsx";
import type {AccountDto} from "../../../api/dtos/accounts/AccountDtos.tsx";
import PillNav, {type PillNavItem} from "../../../components/nav/PillNav.tsx";
import {useContext} from "react";
import CurrentPageContext from "../../../services/CurrentPage.tsx";

type WorkflowNavProps = {
  account: AccountDto,
  workflow: WorkflowDto
}

function WorkflowNav(props: WorkflowNavProps) {
  const {account, workflow} = props;
  const pageContext = useContext(CurrentPageContext);

  const isInitialized = (account && workflow);

  const workflowPagePath = isInitialized ? getAccountWorkflowPagePath(account.id, workflow.id) : "";
  const workflowStatesPagePath = isInitialized ? getAccountWorkflowStatesPagePath(account.id, workflow.id) : "";

  const pillNavItems: PillNavItem[] = [
    {
      label: 'Details',
      selected: isCurrentPagePath(pageContext, workflowPagePath),
      onSelect: () => {
        goToPage(workflowPagePath);
      }
    },
    {
      label: 'States',
      selected: isCurrentPagePath(pageContext, workflowStatesPagePath),
      onSelect: () => {
        goToPage(workflowStatesPagePath);
      }
    }
  ];

  return (
    <PillNav pillNavItems={pillNavItems} className={"mb-3"}></PillNav>
  )
}

export default WorkflowNav;
