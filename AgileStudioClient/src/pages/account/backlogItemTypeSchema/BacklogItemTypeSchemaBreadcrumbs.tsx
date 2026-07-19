import './BacklogItemTypeSchemaBreadcrumbs.css'
import {
  getAccountBacklogItemTypeSchemasPagePath,
  getAccountBacklogItemTypeSchemaPagePath,
  getAccountHomePagePath,
  getAccountsPagePath,
} from "../../../PageRoutes.tsx";
import {linkToPage} from "../../../PageRouterUtils.tsx";
import type {BacklogItemTypeSchemaDto} from "../../../api/dtos/accounts/BacklogItemTypeSchemaDtos.tsx";
import type {AccountDto} from "../../../api/dtos/accounts/AccountDtos.tsx";
import Breadcrumbs, {Breadcrumb} from "../../../components/breadcrumbs/Breadcrumbs.tsx";
import {getAccountTitle} from "../../../util/account-utils.tsx";

type BacklogItemTypeSchemaBreadcrumbsProps = {
  account: AccountDto,
  backlogItemTypeSchema: BacklogItemTypeSchemaDto
}

function BacklogItemTypeSchemaBreadcrumbs(props: BacklogItemTypeSchemaBreadcrumbsProps) {
  const {account, backlogItemTypeSchema} = props;

  const accountTitle = getAccountTitle(account);
  const accountPagePath = account ? getAccountHomePagePath(account.id) : "";
  const schemasPagePath = account ? getAccountBacklogItemTypeSchemasPagePath(account.id) : "";
  const schemaPagePath = backlogItemTypeSchema ?
    getAccountBacklogItemTypeSchemaPagePath(account.id, backlogItemTypeSchema.id) : "";

  return (
    <Breadcrumbs>
      <Breadcrumb href={getAccountsPagePath()} onClick={linkToPage}>Accounts</Breadcrumb>
      <Breadcrumb href={accountPagePath} onClick={linkToPage}>{accountTitle}</Breadcrumb>
      <Breadcrumb href={schemasPagePath} onClick={linkToPage}>Backlog Item Type Schemas</Breadcrumb>
      <Breadcrumb href={schemaPagePath} onClick={linkToPage}>{backlogItemTypeSchema.title}</Breadcrumb>
    </Breadcrumbs>
  )
}

export default BacklogItemTypeSchemaBreadcrumbs;
