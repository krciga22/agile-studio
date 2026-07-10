import './AccountSettingsPage.css'
import React, {useContext, useEffect, useState} from "react";
import Utils, {debounce, numberToString, stringToNumber} from "../../Utils.tsx";
import type {AccountDto} from "../../services/api/dtos/accounts/AccountDtos.tsx";
import CurrentUserContext from "../../services/CurrentUser.tsx";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faSpinner} from "@fortawesome/free-solid-svg-icons";
import Constants from "../../Constants.tsx";
import {type AxiosResponse} from "axios";
import type {ProblemDetailsErrorMap} from "../../services/api/dtos/ProblemDetailsDtos.tsx";
import FormError from "../../components/form/FormError.tsx";
import Breadcrumbs, { Breadcrumb } from "../../components/breadcrumbs/Breadcrumbs";
import {linkToPage} from "../../PageRouterUtils.tsx";
import {getAccountPagePath, getAccountsPagePath} from "../../PageRoutes.tsx";
import ConfirmModal from '../../modals/ConfirmModal';
import {getAccount} from "../../services/api/endpoints/accounts/Accounts.tsx";
import type {AccountTypeDto} from "../../services/api/dtos/accounts/AccountTypeDtos.tsx";
import {getAccountTypes} from "../../services/api/endpoints/accounts/AccountTypes.tsx";
import type {PaginatedResultsDto} from "../../services/api/dtos/PaginatedResultsDto.tsx";
import {getAccountTitle} from "../../services/util/account-utils.tsx";

type SettingsPageProps = {
  accountId: number
}

function AccountSettingsPage(props: SettingsPageProps) {
  const {accountId} = props;
  const [isWorking, setIsWorking] = useState(false);
  const [isRefreshing, setIsRefreshing] = useState<boolean>(false);
  const [isSubmitting, setIsSubmitting] = useState<boolean>(false);
  const [account, setAccount] = useState<AccountDto|null>(null);
  const [accountTypes, setAccountTypes] = useState<AccountTypeDto[]>([]);
  const [accountTypeId, setAccountTypeId] = useState<number>(Constants.DEFAULT_VALUE_NUMBER);
  const currentUser = useContext(CurrentUserContext);
  const refreshTimeoutIdRef = React.useRef<number | null>(null);
  const submitTimeoutIdRef = React.useRef<number | null>(null);
  const [formFieldErrors, setFormFieldErrors] = useState<ProblemDetailsErrorMap>({});
  const [formSubmissionError, setFormSubmissionError] = useState<string|null>(null);
  const [enableRequiredFieldValidation] = useState<boolean>(true);
  const [isConfirmingDelete, setIsConfirmingDelete] = useState<boolean>(false);

  useEffect(() => {
    setIsWorking(isRefreshing || isSubmitting);
  }, [isRefreshing, isSubmitting]);

  useEffect(() => {
    if(account){
      Utils.setDocumentTitle(`Settings - ${getAccountTitle(account)}`);
      setAccountTypeId(account.accountType.id);
    }
  }, [account]);

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
      const promises = [
        getAccount(accountId),
        getAccountTypes(),
      ];

      const responses = await Promise.all(promises);

      const accountResponse =
        responses[0] as AxiosResponse<AccountDto>;

      const accountTypesResponse =
        responses[1] as AxiosResponse<PaginatedResultsDto<AccountTypeDto>>;

      setAccount(accountResponse.data);
      setAccountTypes(accountTypesResponse.data.items);
    }
    catch(e){
      console.error("Failed to refresh account settings", e);
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
    throw new Error("Not implemented");

    // try {
    //   const accountPatchDto: AccountPatchDto = {
    //     id: accountId,
    //     // todo update some settings
    //   };
    //
    //   await updateAccount(accountId, accountPatchDto);
    // }
    // catch (err) {
    //   console.log(err);
    //   let newFormSubmissionError = ERROR_MESSAGE_DEFAULT;
    //
    //   if (axios.isAxiosError(err) && err.response) {
    //     newFormSubmissionError = getErrorMessageForAxiosError(
    //       err, ERROR_CONTEXT.FORM_SUBMISSION);
    //
    //     const newFormFieldErrors = await getProblemDetailsErrorMapFromResponse(err.response);
    //     if(newFormFieldErrors && Object.keys(newFormFieldErrors).length > 0){
    //       setFormFieldErrors(newFormFieldErrors);
    //     }
    //   }
    //
    //   setFormSubmissionError(newFormSubmissionError);
    // }
    // finally {
    //   setIsSubmitting(false);
    // }
  };

  if((account === null || account.id !== accountId) && !currentUser.isLoading && currentUser.user){
    refresh();
  }

  return (
    <div className={"AccountSettingsPage"}>
      { isRefreshing && <FontAwesomeIcon icon={faSpinner} size={"lg"} spin={true}></FontAwesomeIcon> }

      {
        !isRefreshing && account &&
          <div style={{maxWidth: '700px'}}>

              <Breadcrumbs>
                <Breadcrumb href={getAccountsPagePath()} onClick={linkToPage}>Accounts</Breadcrumb>
                <Breadcrumb href={getAccountPagePath(account.id)} onClick={linkToPage}>{getAccountTitle(account)}</Breadcrumb>
              </Breadcrumbs>

              <h1>Settings</h1>
              <form className={"py-4"} onSubmit={handleSubmit}>
                  <div className={"row py-2"}>
                      <div className={"col col-12 col-md-5 text-start"}>
                          <label>Account ID *</label>
                      </div>
                      <div className={"col col-12 col-md-7"}>
                          <input
                              type={"text"}
                              className={"form-control"}
                              value={accountId}
                              disabled={true}
                              required={enableRequiredFieldValidation} />

                          <FormError error={formFieldErrors} id="accountid" />
                      </div>
                  </div>
                  <div className={"row py-2"}>
                      <div className={"col col-12 col-md-5 text-start"}>
                          <label>Account Type *</label>
                      </div>
                      <div className={"col col-12 col-md-7"}>
                          <select
                            className={"form-control"}
                            value={numberToString(accountTypeId)}
                            onChange={e => {
                              setAccountTypeId(
                                stringToNumber(e.target.value))
                            }}
                            disabled={true}
                            required={enableRequiredFieldValidation}
                          >
                              <option value={Constants.DEFAULT_VALUE_STRING}></option>

                              {accountTypes.map(accountType => {
                                let label:string = `${accountType.title}`;
                                const description:string = accountType.description ?? "";
                                if(description.length > 0){
                                  label += ` - ${accountType.description}`;
                                }

                                return (
                                  <option key={accountType.id} value={accountType.id}>{label}</option>
                                );
                              })}
                          </select>
                          <FormError error={formFieldErrors} id="accounttypeid" />
                      </div>
                  </div>

                  <div className={"row py-2"}>
                      <div className={"col col-12 col-md-5 text-start text-md-end"}></div>
                      <div className={"col col-12 col-md-7 d-flex justify-content-end gap-8"}>
                        <FormError error={formSubmissionError} />
                        <button type="submit" className={"btn btn-primary"} disabled={true}>
                          {
                            isWorking ?
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
                      <label>Delete Account</label>
                  </div>
                  <div className={"col col-12 col-md-7"}>
                      <button type="button" className={"btn btn-danger"} disabled={true} onClick={() => setIsConfirmingDelete(true)}>
                        {
                          isWorking ?
                            <FontAwesomeIcon icon={faSpinner} size={"lg"} spin={true}></FontAwesomeIcon> :
                            'Delete Account'
                        }
                      </button>
                  </div>
              </div>

              <ConfirmModal
                isOpen={isConfirmingDelete}
                title={`Delete Account`}
                message={<span>Are you sure you want to delete the account <strong>{getAccountTitle(account)}</strong>? This action cannot be undone.</span>}
                confirmText={'Delete Account'}
                onCancel={() => setIsConfirmingDelete(false)}
                onConfirm={async () => {
                  throw new Error("Not implemented");
                }}
              />
          </div>
      }
    </div>
  )
}

export default AccountSettingsPage
