import React, {useEffect, useMemo, useState} from 'react';
import type {
  BacklogItemTypeDto,
  BacklogItemTypePatchDto,
  BacklogItemTypePostDto
} from '../../api/dtos/accounts/BacklogItemTypeDtos.tsx';
import {createBacklogItemType, getWorkflows} from '../../api/endpoints/accounts/Accounts.tsx';
import {getBacklogItemType, updateBacklogItemType} from '../../api/endpoints/accounts/BacklogItemTypes.tsx';
import {faSpinner} from '@fortawesome/free-solid-svg-icons';
import {FontAwesomeIcon} from '@fortawesome/react-fontawesome';
import Constants from '../../Constants.tsx';
import type {ProblemDetailsErrorMap} from '../../api/dtos/ProblemDetailsDtos.tsx';
import axios from 'axios';
import {getProblemDetailsErrorMapFromResponse} from '../../api/Api.tsx';
import FormError from '../../components/form/FormError.tsx';
import {debounce, numberToString, stringToNumber} from '../../Utils.tsx';
import {ERROR_CONTEXT, ERROR_MESSAGE_DEFAULT, getErrorMessageForAxiosError} from '../../util/error.tsx';
import {toast} from 'react-toastify';
import type {WorkflowDto} from '../../api/dtos/accounts/WorkflowDtos.tsx';
import {toSortString} from "../../api/api-utils.tsx";

type Props = {
  isOpen: boolean;
  accountId: number;
  backlogItemTypeId?: number;
  onClose: () => void;
  onSaved: (backlogItemType: BacklogItemTypeDto) => void;
};

export default function BacklogItemTypeModal({
  isOpen,
  accountId,
  backlogItemTypeId,
  onClose,
  onSaved
}: Props) {
  const defaultValue = '';
  const isEditMode = useMemo(() => typeof backlogItemTypeId === 'number', [backlogItemTypeId]);
  const [isSubmitting, setIsSubmitting] = useState<boolean>(false);
  const [isRefreshing, setIsRefreshing] = useState<boolean>(false);
  const [workflows, setWorkflows] = useState<WorkflowDto[]>([]);
  const [title, setTitle] = useState(defaultValue);
  const [description, setDescription] = useState(defaultValue);
  const [workflowId, setWorkflowId] = useState<number>(Constants.DEFAULT_VALUE_NUMBER);
  const [formFieldErrors, setFormFieldErrors] = useState<ProblemDetailsErrorMap>({});
  const [formSubmissionError, setFormSubmissionError] = useState<string | null>(null);
  const submitTimeoutIdRef = React.useRef<ReturnType<typeof setTimeout> | null>(null);

  useEffect(() => {
    if (!isOpen) {
      return;
    }

    setIsRefreshing(true);
    setFormFieldErrors({});
    setFormSubmissionError(null);

    const refresh = async () => {
      try {
        const workflowsResponse = await getWorkflows(accountId,
          {sort: toSortString(['title:asc'])});
        setWorkflows(workflowsResponse.data.items);

        if (isEditMode && backlogItemTypeId) {
          const backlogItemTypeResponse = await getBacklogItemType(backlogItemTypeId);
          const backlogItemType = backlogItemTypeResponse.data;
          setTitle(backlogItemType.title ?? defaultValue);
          setDescription(backlogItemType.description ?? defaultValue);
          setWorkflowId(backlogItemType.workflow.id);
        } else {
          setTitle(defaultValue);
          setDescription(defaultValue);
          setWorkflowId(Constants.DEFAULT_VALUE_NUMBER);
        }
      } catch (error) {
        console.error("Failed to refresh backlog item type modal", error);
      } finally {
        setIsRefreshing(false);
      }
    };

    refresh();
  }, [accountId, backlogItemTypeId, isEditMode, isOpen]);

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
      let response;
      if (isEditMode && backlogItemTypeId) {
        const patchDto: BacklogItemTypePatchDto = {
          id: backlogItemTypeId,
          title: title.trim(),
          description: description.trim() || undefined
        };

        response = await updateBacklogItemType(patchDto);
        toast.success('Backlog Item Type Updated', Constants.DEFAULT_TOAST_PROPS);
      } else {
        const postDto: BacklogItemTypePostDto = {
          title: title.trim(),
          description: description.trim() || undefined,
          accountID: accountId,
          workflowID: workflowId
        };

        response = await createBacklogItemType(postDto);
        toast.success('Backlog Item Type Created', Constants.DEFAULT_TOAST_PROPS);
      }

      onSaved(response.data);
      onClose();
    } catch (err) {
      let newFormSubmissionError = ERROR_MESSAGE_DEFAULT;

      if (axios.isAxiosError(err) && err.response) {
        newFormSubmissionError = getErrorMessageForAxiosError(err, ERROR_CONTEXT.FORM_SUBMISSION);

        const newFormFieldErrors = await getProblemDetailsErrorMapFromResponse(err.response);
        if (newFormFieldErrors && Object.keys(newFormFieldErrors).length > 0) {
          setFormFieldErrors(newFormFieldErrors);
        }
      }

      setFormSubmissionError(newFormSubmissionError);
    } finally {
      setIsSubmitting(false);
    }
  };

  if (!isOpen) {
    return null;
  }

  return (
    <div className="create-modal-overlay" onMouseDown={onClose} style={{
      position: 'fixed', inset: 0, background: 'rgba(0,0,0,0.4)', display: 'flex',
      alignItems: 'center', justifyContent: 'center', zIndex: 2000
    }}>
      <div className="create-modal" onMouseDown={e => e.stopPropagation()} style={{
        background: '#fff', padding: 20, borderRadius: 6, width: 480, maxWidth: '90%'
      }}>
        <h3 style={{marginTop: 0}}>
          {isEditMode ? 'Edit Backlog Item Type' : 'Create Backlog Item Type'}
        </h3>
        <form onSubmit={handleSubmit}>
          <div className="form-group" style={{marginBottom: 12}}>
            <label htmlFor="backlog-item-type-title">Title *</label>
            <input
              id="backlog-item-type-title"
              className="form-control"
              value={title}
              onChange={e => setTitle(e.target.value)}
              disabled={isSubmitting || isRefreshing}
              required
            />
            <FormError error={formFieldErrors} id="title"/>
          </div>

          <div className="form-group" style={{marginBottom: 12}}>
            <label htmlFor="backlog-item-type-workflow">Workflow *</label>
            <select
              id="backlog-item-type-workflow"
              className="form-control"
              value={numberToString(workflowId)}
              onChange={e => setWorkflowId(stringToNumber(e.target.value))}
              disabled={isSubmitting || isRefreshing || isEditMode}
              required
            >
              <option value={Constants.DEFAULT_VALUE_STRING}></option>
              {workflows.map(workflow => (
                <option key={workflow.id} value={workflow.id}>
                  {workflow.title}
                </option>
              ))}
            </select>
            <FormError error={formFieldErrors} id="workflowid"/>
          </div>

          <div className="form-group" style={{marginBottom: 12}}>
            <label htmlFor="backlog-item-type-description">Description</label>
            <textarea
              id="backlog-item-type-description"
              className="form-control"
              value={description}
              onChange={e => setDescription(e.target.value)}
              rows={3}
              disabled={isSubmitting || isRefreshing}
            />
            <FormError error={formFieldErrors} id="description"/>
          </div>

          <div style={{display: 'flex', gap: 8, justifyContent: 'flex-end'}}>
            <FormError error={formSubmissionError}/>
            <button
              type="button"
              className="btn btn-secondary"
              onClick={onClose}
              disabled={isSubmitting || isRefreshing}
            >
              Cancel
            </button>
            <button
              type="submit"
              className="btn btn-primary"
              disabled={
                isSubmitting ||
                isRefreshing ||
                (!isEditMode && workflowId === Constants.DEFAULT_VALUE_NUMBER)
              }
            >
              {(isSubmitting || isRefreshing)
                ? <FontAwesomeIcon icon={faSpinner} size={'lg'} spin={true}/>
                : (isEditMode ? 'Save' : 'Create')
              }
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
