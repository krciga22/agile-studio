import './BacklogItemTypeSchemaPage.css'
import {useContext, useEffect, useState} from "react";
import Utils from "../../../Utils.tsx";
import type {BacklogItemTypeSchemaDto} from "../../../api/dtos/accounts/BacklogItemTypeSchemaDtos.tsx";
import type {AccountDto} from "../../../api/dtos/accounts/AccountDtos.tsx";
import CurrentUserContext from "../../../services/CurrentUser.tsx";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faSpinner} from "@fortawesome/free-solid-svg-icons";
import {getAccount} from "../../../api/endpoints/accounts/Accounts.tsx";
import {getBacklogItemTypeSchema} from "../../../api/endpoints/accounts/BacklogItemTypeSchemas.tsx";
import BacklogItemTypeSchemaBreadcrumbs from "./BacklogItemTypeSchemaBreadcrumbs.tsx";
import BacklogItemTypeSchemaNav from "./BacklogItemTypeSchemaNav.tsx";
import BacklogItemTypeSchemaNodesDataTable from "../../../data-tables/account/BacklogItemTypeSchemaNodesDataTable.tsx";
import axios from "axios";
import {ERROR_CONTEXT, ERROR_MESSAGE_DEFAULT, getErrorMessageForAxiosError} from "../../../util/error.tsx";
import Constants from "../../../Constants.tsx";
import {toast} from "react-toastify";

type BacklogItemTypeSchemaNodesPageProps = {
  accountID: number,
  backlogItemTypeSchemaID: number
}

function BacklogItemTypeSchemaNodesPage(props: BacklogItemTypeSchemaNodesPageProps) {
  const {accountID, backlogItemTypeSchemaID} = props;
  const [isRefreshing, setIsRefreshing] = useState<boolean|null>(null);
  const [schema, setSchema] = useState<BacklogItemTypeSchemaDto|null>(null);
  const [account, setAccount] = useState<AccountDto|null>(null);
  const currentUser = useContext(CurrentUserContext);

  useEffect(() => {
    if(schema){
      Utils.setDocumentTitle(`Backlog Item Type Schema Types - ${schema.title}`);
    }
  }, [schema]);

  const refresh = () => {
    if(isRefreshing){
      return;
    }

    setIsRefreshing(true);

    Promise.all([
      getBacklogItemTypeSchema(backlogItemTypeSchemaID),
      getAccount(accountID)
    ])
      .then(([schemaResponse, accountResponse]) => {
        setSchema(schemaResponse.data);
        setAccount(accountResponse.data);
      })
      .catch(error => {
        console.error("Error refreshing", error);

        let errMsg = ERROR_MESSAGE_DEFAULT;
        if (axios.isAxiosError(error) && error.response) {
          errMsg = getErrorMessageForAxiosError(
            error, ERROR_CONTEXT.FORM_SUBMISSION);
        }
        toast.error(errMsg, Constants.DEFAULT_TOAST_PROPS);
      })
      .finally(() => {
        setIsRefreshing(false);
      });
  }

  if((schema === null || schema.id !== backlogItemTypeSchemaID || account === null || account.id !== accountID) &&
    !currentUser.isLoading && currentUser.user){
    refresh();
  }

  return (
    <div className={"BacklogItemTypeSchemaNodesPage"}>
      { isRefreshing !== false && <FontAwesomeIcon icon={faSpinner} size={"lg"} spin={true}></FontAwesomeIcon> }

      {
        isRefreshing === false && account && schema &&
          <div>
            <BacklogItemTypeSchemaBreadcrumbs account={account} backlogItemTypeSchema={schema} />
            <BacklogItemTypeSchemaNav account={account} backlogItemTypeSchema={schema} />

            <h2>Types</h2>
            <BacklogItemTypeSchemaNodesDataTable backlogItemTypeSchemaId={backlogItemTypeSchemaID} />
          </div>
      }
    </div>
  )
}

export default BacklogItemTypeSchemaNodesPage;
