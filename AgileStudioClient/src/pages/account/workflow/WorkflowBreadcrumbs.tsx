import './WorkflowBreadcrumbs.css'
import {
  getAccountHomePagePath,
  getAccountsPagePath,
  getAccountWorkflowPagePath,
  getAccountWorkflowsPagePath,
} from "../../../PageRoutes.tsx";
import {linkToPage} from "../../../PageRouterUtils.tsx";
import type {WorkflowDto} from "../../../api/dtos/accounts/WorkflowDtos.tsx";
import type {AccountDto} from "../../../api/dtos/accounts/AccountDtos.tsx";
import Breadcrumbs, {Breadcrumb} from "../../../components/breadcrumbs/Breadcrumbs.tsx";
import {getAccountTitle} from "../../../util/account-utils.tsx";

type WorkflowBreadcrumbsProps = {
  account: AccountDto,
  workflow: WorkflowDto
}

function WorkflowBreadcrumbs(props: WorkflowBreadcrumbsProps) {
  const {account, workflow} = props;

  const accountTitle = getAccountTitle(account);
  const accountPagePath = account ? getAccountHomePagePath(account.id) : "";
  const accountWorkflowsPagePath = account ? getAccountWorkflowsPagePath(account.id) : "";
  const workflowPagePath = workflow ?
    getAccountWorkflowPagePath(workflow.account.id, workflow.id) : "";

  return (
    <Breadcrumbs>
      <Breadcrumb href={getAccountsPagePath()} onClick={linkToPage}>Accounts</Breadcrumb>
      <Breadcrumb href={accountPagePath} onClick={linkToPage}>{accountTitle}</Breadcrumb>
      <Breadcrumb href={accountWorkflowsPagePath} onClick={linkToPage}>Workflows</Breadcrumb>
      <Breadcrumb href={workflowPagePath} onClick={linkToPage}>{workflow.title}</Breadcrumb>
    </Breadcrumbs>
  )
}

export default WorkflowBreadcrumbs;
