import './AccountsMenu.css'
import {type ReactElement, useContext, useState} from "react";
import type {AccountDto} from "../../../services/api/dtos/AccountDtos.tsx";
import CurrentUserContext from "../../../services/CurrentUser.tsx";
import AccountSubMenu from "./AccountSubMenu.tsx";
import {faSpinner} from "@fortawesome/free-solid-svg-icons";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {linkToPage} from "../../../PageRouterUtils.tsx";
import CurrentPageContext from "../../../services/CurrentPage.tsx";
import {
  getAccountHomePagePath,
  getAccountPagePath,
  getAccountsPagePath,
  isCurrentPageBasePath,
} from "../../../PageRoutes.tsx";
import {getAccounts} from "../../../services/api/endpoints/accounts/Accounts.tsx";
import {getAccountTitle} from "../../../services/util/account-utils.tsx";

function AccountsMenu() {
  const [isRefreshing, setIsRefreshing] = useState<boolean|null>(null);
  const [accounts, setAccounts] = useState<AccountDto[]>([]);
  const currentUser = useContext(CurrentUserContext);
  const currentPage = useContext(CurrentPageContext);

  const refresh = () => {
    if(isRefreshing){
      return;
    }

    setIsRefreshing(true);

    getAccounts()
      .then(response => {
        setAccounts(response.data.items);
      })
      .catch(error => {
        console.error("Error fetching accounts:", error);
      })
      .finally(() => {
        setIsRefreshing(false);
      });
  }

  const renderAccount = (account:AccountDto) => {
    const accountPagePath = getAccountPagePath(account.id);
    const accountHomePath = getAccountHomePagePath(account.id);
    const isActiveAccount:boolean = isCurrentPageBasePath(currentPage, accountPagePath);

    const listItems:ReactElement[] = [];
    listItems.push(
      <a key={account.id}
         className={`list-group-item ${isActiveAccount ? 'active' : ''}`}
         href={accountHomePath}
         onClick={linkToPage}>
        {getAccountTitle(account)}
      </a>
    );

    if(isActiveAccount){
      listItems.push(
        <li key={`${account.id}-sub-menu`} className={"list-group-item p-0"}>
          <AccountSubMenu account={account}></AccountSubMenu>
        </li>
      );
    }

    return listItems;
  }

  if(isRefreshing === null && !currentUser.isLoading && currentUser.user){
    refresh();
  }

  return (
    <div className={"AccountsMenu QuickNavMenu"}>
      <div className={"d-flex justify-content-between mb-2"}>
        <span><a href={getAccountsPagePath()} onClick={linkToPage}>Accounts</a></span>
      </div>

      { isRefreshing !== false && <FontAwesomeIcon icon={faSpinner} size={"lg"} spin={true}></FontAwesomeIcon> }

      {
        isRefreshing === false &&
          <ul className="list-group">
            {accounts.map(account => renderAccount(account))}
          </ul>
      }
    </div>
  )
}

export default AccountsMenu;