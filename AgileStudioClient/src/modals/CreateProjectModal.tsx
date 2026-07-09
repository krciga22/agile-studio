import React, {useEffect, useState} from 'react';
import type {ProjectDto, ProjectPostDto} from '../services/api/dtos/ProjectDtos.tsx';
import type {BacklogItemTypeSchemaDto} from "../services/api/dtos/BacklogItemTypeSchemaDtos.tsx";
import {getBacklogItemTypeSchemas} from "../services/api/endpoints/accounts/Accounts.tsx";
import {faSpinner} from "@fortawesome/free-solid-svg-icons";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import type {BacklogItemLinkTypeSchemaDto} from "../services/api/dtos/BacklogItemLinkTypeSchemaDtos.tsx";
import {getBacklogItemLinkTypeSchemas} from "../services/api/endpoints/accounts/BacklogItemLinkTypeSchemas.tsx";
import Constants from "../Constants.tsx";
import type {
  ProblemDetailsErrorMap
} from "../services/api/dtos/ProblemDetailsDtos.tsx";
import axios from "axios";
import {
  getProblemDetailsErrorMapFromResponse
} from "../services/api/Api.tsx";
import FormError from "../components/form/FormError.tsx";
import {debounce} from "../Utils.tsx";
import {
  ERROR_CONTEXT,
  ERROR_MESSAGE_DEFAULT,
  getErrorMessageForAxiosError
} from "../services/util/error.tsx";
import {toast} from "react-toastify";
import {createProject} from "../services/api/endpoints/projects/Projects.tsx";
import type {AccountDto} from "../services/api/dtos/AccountDtos.tsx";
import { getAccounts } from "../services/api/endpoints/accounts/Accounts.tsx";
import {getAccountTitle} from "../services/util/account-utils.tsx";

type Props = {
  isOpen: boolean;
  onClose: () => void;
  onCreated: (project: ProjectDto) => void;
};

export default function CreateProjectModal({ isOpen, onClose, onCreated }: Props) {
  const defaultValue:string = "";
  const [isWorking, setIsWorking] = useState(false);
  const [isRefreshing, setIsRefreshing] = useState<boolean>(false);
  const [isSubmitting, setIsSubmitting] = useState<boolean>(false);
  const [shouldRefresh, setShouldRefresh] = useState<boolean>(false);
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const [accountId, setAccountId] = useState<string>(defaultValue);
  const [backlogItemTypeSchemaId, setBacklogItemTypeSchemaId] = useState<string>(defaultValue);
  const [backlogItemLinkTypeSchemaId, setBacklogItemLinkTypeSchemaId] = useState<string>(defaultValue);
  const [accounts, setAccounts] = useState<AccountDto[]>([]);
  const [backlogItemTypeSchemas, setBacklogItemTypeSchemas] = useState<BacklogItemTypeSchemaDto[]>([]);
  const [backlogItemLinkTypeSchemas, setBacklogItemLinkTypeSchemas] = useState<BacklogItemLinkTypeSchemaDto[]>([]);
  const [formFieldErrors, setFormFieldErrors] = useState<ProblemDetailsErrorMap>({});
  const [formSubmissionError, setFormSubmissionError] = useState<string|null>(null);
  const [enableRequiredFieldValidation] = useState<boolean>(false);

  useEffect(() => {
    if(isOpen){
      setShouldRefresh(true);
    }
  }, [isOpen]);

  useEffect(() => {
    setIsWorking(isRefreshing);
  }, [isRefreshing]);

  const refresh = async () => {
    if(isRefreshing){
      return;
    }

    setIsRefreshing(true);

    const [
      backlogItemTypeSchemasResponse,
      backlogItemLinkTypeSchemasResponse,
      accountsResponse
    ] = await Promise.all([
      getBacklogItemTypeSchemas(parseInt(accountId)),
      getBacklogItemLinkTypeSchemas(),
      getAccounts()
    ]);

    setBacklogItemTypeSchemas(backlogItemTypeSchemasResponse.data.items ?? []);
    setBacklogItemLinkTypeSchemas(backlogItemLinkTypeSchemasResponse.data ?? []);
    setAccounts(accountsResponse.data?.items ?? []);

    setTimeout(() => {
      setIsRefreshing(false);
    }, Constants.EXTRA_WAIT_TIME_MS);
  }

  const submitTimeoutIdRef = React.useRef<ReturnType<typeof setTimeout> | null>(null);

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
      const projectPostDto: ProjectPostDto = {
        title: title.trim(),
        description: description.trim(),
        accountId: parseInt(accountId),
        backlogItemTypeSchemaId: parseInt(backlogItemTypeSchemaId),
        backlogItemLinkTypeSchemaId: parseInt(backlogItemLinkTypeSchemaId)
      };

      const response = await createProject(projectPostDto);

      toast.success("Project Created", Constants.DEFAULT_TOAST_PROPS);

      onCreated(response.data);

      setTitle(defaultValue);
      setDescription(defaultValue);
      setAccountId(defaultValue);
      setBacklogItemTypeSchemaId(defaultValue);
      setBacklogItemLinkTypeSchemaId(defaultValue);

      onClose();
    }
    catch (err) {
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
    }
    finally {
      setIsWorking(false);
      setIsSubmitting(false);
    }
  };

  if (!isOpen){
    return null;
  }

  if(shouldRefresh){
    setShouldRefresh(false);

    refresh()
      .catch(error => {
        console.error('Error refreshing:', error);
      });
  }

  return (
    <div className="create-modal-overlay" onMouseDown={onClose} style={{
      position: 'fixed', inset: 0, background: 'rgba(0,0,0,0.4)', display: 'flex',
      alignItems: 'center', justifyContent: 'center', zIndex: 2000
    }}>
      <div className="create-modal" onMouseDown={e => e.stopPropagation()} style={{
        background: '#fff', padding: 20, borderRadius: 6, width: 480, maxWidth: '90%'
      }}>
        <h3 style={{ marginTop: 0 }}>Create Project</h3>
        <form onSubmit={handleSubmit}>
          <div className="form-group" style={{ marginBottom: 12 }}>
            <label htmlFor="project-title">Title *</label>
            <input
              id="project-title"
              className="form-control"
              value={title}
              onChange={e => setTitle(e.target.value)}
              disabled={isWorking}
              required={enableRequiredFieldValidation}
            />
            <FormError error={formFieldErrors} id="title" />
          </div>

          <div className="form-group" style={{ marginBottom: 12 }}>
            <label htmlFor="project-account">Account *</label>
            <select
              id="project-account"
              className="form-control"
              value={accountId}
              onChange={e => setAccountId(e.target.value)}
              disabled={isWorking}
              required
            >
              <option value={defaultValue}></option>
              {accounts.map(account => (
                <option key={account.id} value={account.id}>{getAccountTitle(account)}</option>
              ))}
            </select>
            <FormError error={formFieldErrors} id="accountid" />
          </div>

          <div className="form-group" style={{ marginBottom: 12 }}>
            <label htmlFor="project-backlog-item-type-schema">Backlog Item Type Schema *</label>
            <select
              id="project-backlog-item-type-schema"
              className="form-control"
              value={backlogItemTypeSchemaId}
              onChange={e => setBacklogItemTypeSchemaId(e.target.value)}
              disabled={isWorking}
              required={enableRequiredFieldValidation}
            >
              <option value={defaultValue}></option>

              {backlogItemTypeSchemas.map(schema => {
                let label:string = `${schema.title}`;
                const description:string = schema.description ?? "";
                if(description.length > 0){
                  label += ` - ${schema.description}`;
                }

                return (
                  <option key={schema.id} value={schema.id}>{label}</option>
                );
              })}
            </select>
            <FormError error={formFieldErrors} id="backlogitemtypeschemaid" />
          </div>

          <div className="form-group" style={{ marginBottom: 12 }}>
            <label htmlFor="project-backlog-item-link-type-schema">Backlog Item Link Type Schema *</label>
            <select
              id="project-backlog-item-link-type-schema"
              className="form-control"
              value={backlogItemLinkTypeSchemaId}
              onChange={e => setBacklogItemLinkTypeSchemaId(e.target.value)}
              disabled={isWorking}
              required={enableRequiredFieldValidation}
            >
              <option value={defaultValue}></option>

              {backlogItemLinkTypeSchemas.map(schema => {
                let label:string = `${schema.title}`;
                const description:string = schema.description ?? "";
                if(description.length > 0){
                  label += ` - ${schema.description}`;
                }

                return (
                  <option key={schema.id} value={schema.id}>{label}</option>
                );
              })}
            </select>
            <FormError error={formFieldErrors} id="backlogitemlinktypeschemaid" />
          </div>

          <div className="form-group" style={{ marginBottom: 12 }}>
            <label htmlFor="project-description">Description</label>
            <textarea
              id="project-description"
              className="form-control"
              value={description}
              onChange={e => setDescription(e.target.value)}
              rows={3}
              disabled={isWorking}
            />
            <FormError error={formFieldErrors} id="description" />
          </div>

          <div style={{ display: 'flex', gap: 8, justifyContent: 'flex-end' }}>
            <FormError error={formSubmissionError} />
            <button type="button" className="btn btn-secondary" onClick={onClose} disabled={isWorking}>
              Cancel
            </button>
            <button type="submit" className="btn btn-primary" disabled={isWorking}>
              {
                isWorking ?
                  <FontAwesomeIcon icon={faSpinner} size={"lg"} spin={true}></FontAwesomeIcon> :
                  'Create'
              }
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
