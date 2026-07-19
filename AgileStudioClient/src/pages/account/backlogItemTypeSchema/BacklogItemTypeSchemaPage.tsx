import './BacklogItemTypeSchemaPage.css'
import React, {useContext, useEffect, useState} from "react";
import Utils, {debounce} from "../../../Utils.tsx";
import CurrentUserContext from "../../../services/CurrentUser.tsx";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faSpinner} from "@fortawesome/free-solid-svg-icons";
import type {BacklogItemTypeSchemaDto, BacklogItemTypeSchemaPatchDto} from "../../../api/dtos/accounts/BacklogItemTypeSchemaDtos.tsx";
import {getBacklogItemTypeSchema, updateBacklogItemTypeSchema, deleteBacklogItemTypeSchema} from "../../../api/endpoints/accounts/BacklogItemTypeSchemas.tsx";
import type {AccountDto} from "../../../api/dtos/accounts/AccountDtos.tsx";
import {getAccount} from "../../../api/endpoints/accounts/Accounts.tsx";
import BacklogItemTypeSchemaBreadcrumbs from "./BacklogItemTypeSchemaBreadcrumbs.tsx";
import Constants from "../../../Constants.tsx";
import type {ProblemDetailsErrorMap} from "../../../api/dtos/ProblemDetailsDtos.tsx";
import axios from "axios";
import {ERROR_CONTEXT, ERROR_MESSAGE_DEFAULT, getErrorMessageForAxiosError} from "../../../util/error.tsx";
import {getProblemDetailsErrorMapFromResponse} from "../../../api/Api.tsx";
import FormError from "../../../components/form/FormError.tsx";
import ConfirmDeleteModal from "../../../modals/ConfirmDeleteModal.tsx";
import {goToPage} from "../../../PageRouterUtils.tsx";
import {getAccountBacklogItemTypeSchemasPagePath} from "../../../PageRoutes.tsx";
import BacklogItemTypeSchemaNav from "./BacklogItemTypeSchemaNav.tsx";
import {toast} from "react-toastify";

type BacklogItemTypeSchemaPageProps = {
  accountID: number,
  backlogItemTypeSchemaID: number
}

function BacklogItemTypeSchemaPage(props: BacklogItemTypeSchemaPageProps) {
  const {accountID, backlogItemTypeSchemaID} = props;
  const [isSubmitting, setIsSubmitting] = useState<boolean>(false);
  const [isRefreshing, setIsRefreshing] = useState<boolean|null>(null);
  const [schema, setSchema] = useState<BacklogItemTypeSchemaDto|null>(null);
  const [account, setAccount] = useState<AccountDto|null>(null);
  const [title, setTitle] = useState(Constants.DEFAULT_VALUE_STRING);
  const [description, setDescription] = useState(Constants.DEFAULT_VALUE_STRING);
  const [formFieldErrors, setFormFieldErrors] = useState<ProblemDetailsErrorMap>({});
  const [formSubmissionError, setFormSubmissionError] = useState<string | null>(null);
  const [enableRequiredFieldValidation] = useState<boolean>(true);
  const [isConfirmingDelete, setIsConfirmingDelete] = useState<boolean>(false);
  const refreshTimeoutIdRef = React.useRef<number | null>(null);
  const submitTimeoutIdRef = React.useRef<number | null>(null);
  const currentUser = useContext(CurrentUserContext);

  useEffect(() => {
    if(schema){
      Utils.setDocumentTitle(`Backlog Item Type Schema - ${schema.title}`);
      setTitle(schema.title ?? Constants.DEFAULT_VALUE_STRING);
      setDescription(schema.description ?? Constants.DEFAULT_VALUE_STRING);
    }
  }, [schema]);

  const refresh = async () => {
    if(isRefreshing){
      return;
    }

    setIsRefreshing(true);

    debounce(
      _refresh,
      Constants.EXTRA_WAIT_TIME_MS,
      refreshTimeoutIdRef
    );
  }

  const _refresh = async () => {
    try{
      const schemaResponse = await getBacklogItemTypeSchema(backlogItemTypeSchemaID);
      setSchema(schemaResponse.data);

      const accountResponse = await getAccount(accountID);
      setAccount(accountResponse.data);
    }
    catch(err){
      console.error(err);

      let errMsg = ERROR_MESSAGE_DEFAULT;
      if (axios.isAxiosError(err) && err.response) {
        errMsg = getErrorMessageForAxiosError(
          err, ERROR_CONTEXT.FORM_SUBMISSION);
      }
      toast.error(errMsg, Constants.DEFAULT_TOAST_PROPS);
    }
    finally {
      setIsRefreshing(false);
    }
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (isSubmitting) return;

    setIsSubmitting(true);
    setFormFieldErrors({});
    setFormSubmissionError(null);

    debounce(
      _handleSubmit,
      Constants.EXTRA_WAIT_TIME_MS,
      submitTimeoutIdRef
    );
  };

  const _handleSubmit = async () => {
    try {
      const dto: BacklogItemTypeSchemaPatchDto = {
        id: backlogItemTypeSchemaID,
        title: title.trim(),
        description: description.trim() || undefined
      };

      const response = await updateBacklogItemTypeSchema(dto);
      setSchema(response.data);
    } catch (err) {
      let newFormSubmissionError = ERROR_MESSAGE_DEFAULT;

      if (axios.isAxiosError(err) && err.response) {
        newFormSubmissionError = getErrorMessageForAxiosError(
          err, ERROR_CONTEXT.FORM_SUBMISSION);

        const newFormFieldErrors = await getProblemDetailsErrorMapFromResponse(err.response);
        if(newFormFieldErrors && Object.keys(newFormFieldErrors).length > 0){
          setFormFieldErrors(newFormFieldErrors);
        }
      }

      setFormSubmissionError(newFormSubmissionError);
    } finally {
      setIsSubmitting(false);
    }
  }

  if((schema === null || schema.id !== backlogItemTypeSchemaID) && !currentUser.isLoading && currentUser.user){
    refresh();
  }

  return (
    <div className={"BacklogItemTypeSchemaPage"}>
      { isRefreshing !== false && <FontAwesomeIcon icon={faSpinner} size={"lg"} spin={true}></FontAwesomeIcon> }

      {
        isRefreshing === false && account && schema &&
          <div style={{maxWidth: '700px'}}>
              <BacklogItemTypeSchemaBreadcrumbs account={account} backlogItemTypeSchema={schema} />
              <BacklogItemTypeSchemaNav account={account} backlogItemTypeSchema={schema} />

              <h2>Details</h2>
              <form className={"py-4"} onSubmit={handleSubmit}>
                <div className={"row py-2"}>
                  <div className={"col col-12 col-md-5 text-start"}>
                    <label>Schema ID *</label>
                  </div>
                  <div className={"col col-12 col-md-7"}>
                    <input
                      className={"form-control"}
                      type={"text"}
                      value={schema.id}
                      disabled={true}
                      required={enableRequiredFieldValidation}
                    />
                    <FormError error={formFieldErrors} id="id" />
                  </div>
                </div>

                <div className={"row py-2"}>
                  <div className={"col col-12 col-md-5 text-start"}>
                    <label>Account ID *</label>
                  </div>
                  <div className={"col col-12 col-md-7"}>
                    <input
                      className={"form-control"}
                      type={"text"}
                      value={account.id}
                      disabled={true}
                      required={enableRequiredFieldValidation}
                    />
                    <FormError error={formFieldErrors} id="accountid" />
                  </div>
                </div>

                <div className={"row py-2"}>
                  <div className={"col col-12 col-md-5 text-start"}>
                    <label>Title *</label>
                  </div>
                  <div className={"col col-12 col-md-7"}>
                    <input
                      className={"form-control"}
                      type={"text"}
                      value={title}
                      onChange={e => setTitle(e.target.value)}
                      disabled={isSubmitting}
                      required={enableRequiredFieldValidation}
                    />
                    <FormError error={formFieldErrors} id="title" />
                  </div>
                </div>

                <div className={"row py-2"}>
                  <div className={"col col-12 col-md-5 text-start"}>
                    <label>Description</label>
                  </div>
                  <div className={"col col-12 col-md-7"}>
                    <textarea
                      className={"form-control"}
                      value={description}
                      onChange={e => setDescription(e.target.value)}
                      disabled={isSubmitting}
                    />
                    <FormError error={formFieldErrors} id="description" />
                  </div>
                </div>

                <div className={"row py-2"}>
                  <div className={"col col-12 col-md-5 text-start text-md-end"}></div>
                  <div className={"col col-12 col-md-7 d-flex justify-content-end gap-8"}>
                    <FormError error={formSubmissionError} />
                    <button type="submit" className={"btn btn-primary"} disabled={isSubmitting}>
                      {
                        isSubmitting ?
                          <FontAwesomeIcon icon={faSpinner} size={"lg"} spin={true}></FontAwesomeIcon> :
                          'Save'
                      }
                    </button>
                  </div>
                </div>
              </form>

              <hr />

              <h3>Danger Zone</h3>
              <div className={"row py-2"}>
                <div className={"col col-12 col-md-5 text-start"}>
                  <label>Delete Schema</label>
                </div>
                <div className={"col col-12 col-md-7"}>
                  <button
                    type="button"
                    className={"btn btn-danger"}
                    disabled={isSubmitting}
                    onClick={() => setIsConfirmingDelete(true)}
                  >
                    Delete Schema
                  </button>
                </div>
              </div>

              <ConfirmDeleteModal
                  resourceType={"Backlog Item Type Schema"}
                  resourceTitle={schema?.title}
                  resourceID={isConfirmingDelete ? schema.id : null}
                  deleteEndpoint={deleteBacklogItemTypeSchema}
                  onCancel={() => {
                    setIsConfirmingDelete(false);
                  }}
                  onDeleteSuccess={async () => {
                    setIsConfirmingDelete(false);
                    goToPage(getAccountBacklogItemTypeSchemasPagePath(accountID));
                  }}
              />
          </div>
      }
    </div>
  )
}

export default BacklogItemTypeSchemaPage;
