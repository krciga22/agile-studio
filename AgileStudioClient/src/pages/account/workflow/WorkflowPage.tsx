import './WorkflowPage.css'
import React, {useContext, useEffect, useState} from "react";
import Utils, {debounce} from "../../../Utils.tsx";
import CurrentUserContext from "../../../services/CurrentUser.tsx";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faSpinner} from "@fortawesome/free-solid-svg-icons";
import type {WorkflowDto, WorkflowPatchDto} from "../../../api/dtos/accounts/WorkflowDtos.tsx";
import {getWorkflow, updateWorkflow} from "../../../api/endpoints/accounts/Workflows.tsx";
import type {AccountDto} from "../../../api/dtos/accounts/AccountDtos.tsx";
import {getAccount} from "../../../api/endpoints/accounts/Accounts.tsx";
import WorkflowNav from "./WorkflowNav.tsx";
import WorkflowBreadcrumbs from "./WorkflowBreadcrumbs.tsx";
import Constants from "../../../Constants.tsx";
import type {ProblemDetailsErrorMap} from "../../../api/dtos/ProblemDetailsDtos.tsx";
import axios from "axios";
import {ERROR_CONTEXT, ERROR_MESSAGE_DEFAULT, getErrorMessageForAxiosError} from "../../../util/error.tsx";
import {getProblemDetailsErrorMapFromResponse} from "../../../api/Api.tsx";
import FormError from "../../../components/form/FormError.tsx";
import ConfirmModal from "../../../modals/ConfirmModal.tsx";
import {goToPage} from "../../../PageRouterUtils.tsx";
import {getAccountWorkflowsPagePath} from "../../../PageRoutes.tsx";
import {toast} from "react-toastify";
import {deleteWorkflow} from "../../../api/endpoints/accounts/Workflows.tsx";

type WorkflowPageProps = {
  workflowId: number
}

function WorkflowPage(props: WorkflowPageProps) {
  const {workflowId} = props;
  const [isSubmitting, setIsSubmitting] = useState<boolean>(false);
  const [isRefreshing, setIsRefreshing] = useState<boolean|null>(null);
  const [workflow, setWorkflow] = useState<WorkflowDto|null>(null);
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
    if(workflow){
      Utils.setDocumentTitle(`Workflow - ${workflow.title}`);
      setTitle(workflow.title ?? Constants.DEFAULT_VALUE_STRING);
      setDescription(workflow.description ?? Constants.DEFAULT_VALUE_STRING);
    }
  }, [workflow]);

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
      const workflowResponse = await getWorkflow(workflowId);
      setWorkflow(workflowResponse.data);

      const accountResponse = await getAccount(workflowResponse.data.account.id);
      setAccount(accountResponse.data);
    }
    catch(error){
      console.error("Error refreshing workflow page", error);
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
      const dto: WorkflowPatchDto = {
        id: workflowId,
        title: title.trim(),
        description: description.trim() || undefined
      };

      const response = await updateWorkflow(dto);
      setWorkflow(response.data);
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

  if((workflow === null || workflow.id !== workflowId) && !currentUser.isLoading && currentUser.user){
    refresh();
  }

  return (
    <div className={"WorkflowPage"}>
      { isRefreshing !== false && <FontAwesomeIcon icon={faSpinner} size={"lg"} spin={true}></FontAwesomeIcon> }

      {
        isRefreshing === false && account && workflow &&
          <div style={{maxWidth: '700px'}}>
              <WorkflowBreadcrumbs account={account} workflow={workflow} />

              <WorkflowNav account={account} workflow={workflow} />

              <h2>Details</h2>
              <form className={"py-4"} onSubmit={handleSubmit}>
                <div className={"row py-2"}>
                  <div className={"col col-12 col-md-5 text-start"}>
                    <label>Workflow ID *</label>
                  </div>
                  <div className={"col col-12 col-md-7"}>
                    <input
                      className={"form-control"}
                      type={"text"}
                      value={workflow.id}
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
                      value={workflow.account.id}
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
                  <label>Delete Workflow</label>
                </div>
                <div className={"col col-12 col-md-7"}>
                  <button
                    type="button"
                    className={"btn btn-danger"}
                    disabled={isSubmitting}
                    onClick={() => setIsConfirmingDelete(true)}
                  >
                    Delete Workflow
                  </button>
                </div>
              </div>

              <ConfirmModal
                isOpen={isConfirmingDelete}
                title={`Delete Workflow`}
                message={<span>Are you sure you want to delete the workflow <strong>{workflow.title}</strong>? This action cannot be undone.</span>}
                confirmText={'Delete Workflow'}
                onCancel={() => setIsConfirmingDelete(false)}
                onConfirm={async () => {
                  await deleteWorkflow(workflow.id);
                  toast.success('Workflow Deleted', Constants.DEFAULT_TOAST_PROPS);
                  goToPage(getAccountWorkflowsPagePath(workflow.account.id));
                }}
              />
          </div>
      }
    </div>
  )
}

export default WorkflowPage;
