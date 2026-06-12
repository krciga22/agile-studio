import './AccountSubMenu.css'
import type {AccountDto} from "../../../services/api/dtos/AccountDtos.tsx";
import {linkToPage} from "../../../PageRouterUtils.tsx";
import CurrentPageContext from "../../../services/CurrentPage.tsx";
import {useContext} from "react";
import {
  getAccountBacklogItemTypesPagePath,
  getAccountSettingsPagePath,
  isCurrentPageBasePath,
} from "../../../PageRoutes.tsx";

type AccountSubMenuProps = {
  account: AccountDto
}

function AccountSubMenu(props:AccountSubMenuProps) {
  const {account} = props;
  const pageContext = useContext(CurrentPageContext);

  const activePageClassName = (pagePath:string):string => {
    return isCurrentPageBasePath(pageContext, pagePath) ? "active" : "";
  }

  const backlogItemTypesPagePath = getAccountBacklogItemTypesPagePath(account.id);
  const settingsPagePath = getAccountSettingsPagePath(account.id);

  return (
    <ul className={"AccountSubMenu QuickNavSubMenu"}>
      <li className={activePageClassName(backlogItemTypesPagePath)}>
        <a href={backlogItemTypesPagePath} onClick={linkToPage}>Backlog Item Types</a>
      </li>
      <li className={activePageClassName(settingsPagePath)}>
        <a href={settingsPagePath} onClick={linkToPage}>Settings</a>
      </li>
    </ul>
  )
}

export default AccountSubMenu;