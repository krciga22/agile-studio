import './BacklogItemTypeSchemasPage.css'
import {useContext, useEffect, useState} from "react";
import Utils from "../../Utils.tsx";
import type {AccountDto} from "../../services/api/dtos/AccountDtos.tsx";
import CurrentUserContext from "../../services/CurrentUser.tsx";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faSpinner} from "@fortawesome/free-solid-svg-icons";
import {getAccount} from "../../services/api/endpoints/accounts/Accounts.tsx";
import Breadcrumbs, {Breadcrumb} from "../../components/breadcrumbs/Breadcrumbs.tsx";
import {getAccountPagePath, getAccountsPagePath} from "../../PageRoutes.tsx";
import {linkToPage} from "../../PageRouterUtils.tsx";
import {getAccountTitle} from "../../services/util/account-utils.tsx";
import BacklogItemTypeSchemasDataTable from "../../data-tables/account/BacklogItemTypeSchemasDataTable.tsx";

type BacklogItemTypeSchemasPageProps = {
  accountId: number
}

function BacklogItemTypeSchemasPage(props: BacklogItemTypeSchemasPageProps) {
  const {accountId} = props;
  const [isRefreshing, setIsRefreshing] = useState<boolean|null>(null);
  const [account, setAccount] = useState<AccountDto|null>(null);
  const currentUser = useContext(CurrentUserContext);

  useEffect(() => {
    if(account){
      Utils.setDocumentTitle(`Backlog Item Type Schemas - ${getAccountTitle(account)}`);
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
    <div className={"BacklogItemTypeSchemasPage"}>
      { isRefreshing !== false && <FontAwesomeIcon icon={faSpinner} size={"lg"} spin={true}></FontAwesomeIcon> }

      {
        isRefreshing === false && account &&
          <div>
              <Breadcrumbs>
                  <Breadcrumb href={getAccountsPagePath()} onClick={linkToPage}>Accounts</Breadcrumb>
                  <Breadcrumb href={getAccountPagePath(account.id)} onClick={linkToPage}>{getAccountTitle(account)}</Breadcrumb>
              </Breadcrumbs>

              <h1>Backlog Item Type Schemas</h1>

              <BacklogItemTypeSchemasDataTable accountId={accountId}></BacklogItemTypeSchemasDataTable>
          </div>
      }
    </div>
  )
}

export default BacklogItemTypeSchemasPage;
