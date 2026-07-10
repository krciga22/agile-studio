import './BacklogItemLinkTypeSchemasPage.css'
import {useContext, useEffect, useState} from "react";
import Utils from "../../Utils.tsx";
import type {AccountDto} from "../../services/api/dtos/accounts/AccountDtos.tsx";
import CurrentUserContext from "../../services/CurrentUser.tsx";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faSpinner} from "@fortawesome/free-solid-svg-icons";
import {getAccount} from "../../services/api/endpoints/accounts/Accounts.tsx";
import Breadcrumbs, {Breadcrumb} from "../../components/breadcrumbs/Breadcrumbs.tsx";
import {getAccountPagePath, getAccountsPagePath} from "../../PageRoutes.tsx";
import {linkToPage} from "../../PageRouterUtils.tsx";
import {getAccountTitle} from "../../services/util/account-utils.tsx";

type BacklogItemLinkTypeSchemasPageProps = {
  accountId: number
}

function BacklogItemLinkTypeSchemasPage(props: BacklogItemLinkTypeSchemasPageProps) {
  const {accountId} = props;
  const [isRefreshing, setIsRefreshing] = useState<boolean|null>(null);
  const [account, setAccount] = useState<AccountDto|null>(null);
  const currentUser = useContext(CurrentUserContext);

  useEffect(() => {
    if(account){
      Utils.setDocumentTitle(`Backlog Item Link Type Schemas - ${getAccountTitle(account)}`);
    }
  }, [account]);

  const refresh = () => {
    if(isRefreshing){
      return;
    }

    setIsRefreshing(true);

    getAccount(accountId)
      .then(response => {
        setAccount(response.data);
      })
      .catch(error => {
        console.error("Error refreshing", error);
      })
      .finally(() => {
        setIsRefreshing(false);
      });
  }

  if((account === null || account.id !== accountId) && !currentUser.isLoading && currentUser.user){
    refresh();
  }

  return (
    <div className={"BacklogItemLinkTypeSchemasPage"}>
      { isRefreshing !== false && <FontAwesomeIcon icon={faSpinner} size={"lg"} spin={true}></FontAwesomeIcon> }

      {
        isRefreshing === false && account &&
          <div>
              <Breadcrumbs>
                  <Breadcrumb href={getAccountsPagePath()} onClick={linkToPage}>Accounts</Breadcrumb>
                  <Breadcrumb href={getAccountPagePath(account.id)} onClick={linkToPage}>{getAccountTitle(account)}</Breadcrumb>
              </Breadcrumbs>

              <h1>Backlog Item Link Type Schemas</h1>
              <p>{getAccountTitle(account)}</p>
          </div>
      }
    </div>
  )
}

export default BacklogItemLinkTypeSchemasPage;
