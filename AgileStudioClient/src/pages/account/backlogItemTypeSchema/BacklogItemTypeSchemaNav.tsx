import './BacklogItemTypeSchemaPage.css'
import {
  getAccountBacklogItemTypeSchemaPagePath,
  isCurrentPagePath
} from "../../../PageRoutes.tsx";
import {goToPage} from "../../../PageRouterUtils.tsx";
import type {BacklogItemTypeSchemaDto} from "../../../api/dtos/accounts/BacklogItemTypeSchemaDtos.tsx";
import type {AccountDto} from "../../../api/dtos/accounts/AccountDtos.tsx";
import PillNav, {type PillNavItem} from "../../../components/nav/PillNav.tsx";
import {useContext} from "react";
import CurrentPageContext from "../../../services/CurrentPage.tsx";

type BacklogItemTypeSchemaNavProps = {
  account: AccountDto,
  backlogItemTypeSchema: BacklogItemTypeSchemaDto
}

function BacklogItemTypeSchemaNav(props: BacklogItemTypeSchemaNavProps) {
  const {account, backlogItemTypeSchema} = props;
  const pageContext = useContext(CurrentPageContext);

  const isInitialized = (account && backlogItemTypeSchema);
  const schemaPagePath = isInitialized ? getAccountBacklogItemTypeSchemaPagePath(account.id, backlogItemTypeSchema.id) : "";
  const typesPagePath = "not-yet-implemented";

  const pillNavItems: PillNavItem[] = [
    {
      label: 'Details',
      selected: isCurrentPagePath(pageContext, schemaPagePath),
      onSelect: () => {
        goToPage(schemaPagePath);
      }
    },
    {
      label: 'Types',
      selected: isCurrentPagePath(pageContext, typesPagePath),
      onSelect: () => {
        goToPage(typesPagePath);
      }
    }
  ];

  return (
    <PillNav pillNavItems={pillNavItems} className={"mb-3"}></PillNav>
  )
}

export default BacklogItemTypeSchemaNav;
