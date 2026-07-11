import React, {useState} from 'react';
import type {WorkflowDto, WorkflowPostDto} from '../../api/dtos/accounts/WorkflowDtos.tsx';
import {createWorkflow} from '../../api/endpoints/accounts/Accounts.tsx';
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
  accountID: number;
  onClose: () => void;
  onCreated: (workflow: WorkflowDto) => void;
};

export default function CreateWorkflowModal({isOpen, accountID, onClose, onCreated}: Props) {
  const defaultValue = '';
  const [isSubmitting, setIsSubmitting] = useState<boolean>(false);
  const [title, setTitle] = useState(defaultValue);
  const [description, setDescription] = useState(defaultValue);
  const [formFieldErrors, setFormFieldErrors] = useState<ProblemDetailsErrorMap>({});
  const [formSubmissionError, setFormSubmissionError] = useState<string | null>(null);

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
      const postDto: WorkflowPostDto = {
        title: title.trim(),
        description: description.trim() || undefined,
        accountId: accountID
      };

      const response = await createWorkflow(postDto);

      toast.success('Workflow Created', Constants.DEFAULT_TOAST_PROPS);

      onCreated(response.data);

      setTitle(defaultValue);
      setDescription(defaultValue);

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
        <h3 style={{marginTop: 0}}>Create Workflow</h3>
        <form onSubmit={handleSubmit}>
          <div className="form-group" style={{marginBottom: 12}}>
            <label htmlFor="workflow-title">Title *</label>
            <input
              id="workflow-title"
              className="form-control"
              value={title}
              onChange={e => setTitle(e.target.value)}
              disabled={isSubmitting}
              required
            />
            <FormError error={formFieldErrors} id="title"/>
          </div>

          <div className="form-group" style={{marginBottom: 12}}>
            <label htmlFor="workflow-description">Description</label>
            <textarea
              id="workflow-description"
              className="form-control"
              value={description}
              onChange={e => setDescription(e.target.value)}
              rows={3}
              disabled={isSubmitting}
            />
            <FormError error={formFieldErrors} id="description"/>
          </div>

          <div style={{display: 'flex', gap: 8, justifyContent: 'flex-end'}}>
            <FormError error={formSubmissionError}/>
            <button type="button" className="btn btn-secondary" onClick={onClose} disabled={isSubmitting}>
              Cancel
            </button>
            <button type="submit" className="btn btn-primary" disabled={isSubmitting}>
              {isSubmitting
                ? <FontAwesomeIcon icon={faSpinner} size={'lg'} spin={true}/>
                : 'Create'
              }
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
