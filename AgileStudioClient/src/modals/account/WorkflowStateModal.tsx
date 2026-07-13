import React, {useEffect, useMemo, useState} from 'react';
import type {
  WorkflowStateDto,
  WorkflowStatePatchDto,
  WorkflowStatePostDto
} from '../../api/dtos/accounts/WorkflowStateDtos.tsx';
import {createWorkflowState} from '../../api/endpoints/accounts/Workflows.tsx';
import {getWorkflowState, updateWorkflowState} from '../../api/endpoints/accounts/WorkflowStates.tsx';
import {faSpinner} from '@fortawesome/free-solid-svg-icons';
import {FontAwesomeIcon} from '@fortawesome/react-fontawesome';
import Constants from '../../Constants.tsx';
import type {ProblemDetailsErrorMap} from '../../api/dtos/ProblemDetailsDtos.tsx';
import axios from 'axios';
import {getProblemDetailsErrorMapFromResponse} from '../../api/Api.tsx';
import FormError from '../../components/form/FormError.tsx';
import {debounce} from '../../Utils.tsx';
import {ERROR_CONTEXT, ERROR_MESSAGE_DEFAULT, getErrorMessageForAxiosError} from '../../util/error.tsx';
import {toast} from 'react-toastify';

type Props = {
  isOpen: boolean;
  workflowId: number;
  workflowStateId?: number;
  onClose: () => void;
  onSaved: (workflowState: WorkflowStateDto) => void;
};

export default function WorkflowStateModal({
  isOpen,
  workflowId,
  workflowStateId,
  onClose,
  onSaved
}: Props) {
  const defaultValue = '';
  const isEditMode = useMemo(() => typeof workflowStateId === 'number', [workflowStateId]);
  const [isSubmitting, setIsSubmitting] = useState<boolean>(false);
  const [isRefreshing, setIsRefreshing] = useState<boolean>(false);
  const [title, setTitle] = useState(defaultValue);
  const [description, setDescription] = useState(defaultValue);
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
        if (isEditMode && workflowStateId) {
          const workflowStateResponse = await getWorkflowState(workflowStateId);
          const workflowState = workflowStateResponse.data;
          setTitle(workflowState.title ?? defaultValue);
          setDescription(workflowState.description ?? defaultValue);
        } else {
          setTitle(defaultValue);
          setDescription(defaultValue);
        }
      } catch (error) {
        console.error('Failed to refresh workflow state modal', error);
      } finally {
        setIsRefreshing(false);
      }
    };

    refresh();
  }, [defaultValue, isEditMode, isOpen, workflowStateId]);

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
      if (isEditMode && workflowStateId) {
        const patchDto: WorkflowStatePatchDto = {
          id: workflowStateId,
          title: title.trim(),
          description: description.trim() || undefined
        };

        response = await updateWorkflowState(patchDto);
        toast.success('Workflow State Updated', Constants.DEFAULT_TOAST_PROPS);
      } else {
        const postDto: WorkflowStatePostDto = {
          title: title.trim(),
          description: description.trim() || undefined,
          workflowId
        };

        response = await createWorkflowState(postDto);
        toast.success('Workflow State Created', Constants.DEFAULT_TOAST_PROPS);
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
          {isEditMode ? 'Edit Workflow State' : 'Create Workflow State'}
        </h3>
        <form onSubmit={handleSubmit}>
          <div className="form-group" style={{marginBottom: 12}}>
            <label htmlFor="workflow-state-title">Title *</label>
            <input
              id="workflow-state-title"
              className="form-control"
              value={title}
              onChange={e => setTitle(e.target.value)}
              disabled={isSubmitting || isRefreshing}
              required
            />
            <FormError error={formFieldErrors} id="title"/>
          </div>

          <div className="form-group" style={{marginBottom: 12}}>
            <label htmlFor="workflow-state-description">Description</label>
            <textarea
              id="workflow-state-description"
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
              disabled={isSubmitting || isRefreshing}
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
