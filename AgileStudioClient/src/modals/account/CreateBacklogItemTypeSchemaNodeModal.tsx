import React, {useEffect, useState} from 'react';
import type {BacklogItemTypeDto} from '../../api/dtos/accounts/BacklogItemTypeDtos.tsx';
import type {BacklogItemTypeSchemaNodeDto, BacklogItemTypeSchemaNodePostDto} from '../../api/dtos/accounts/BacklogItemTypeSchemaNodeDtos.tsx';
import {
  createBacklogItemTypeSchemaNode,
  getBacklogItemTypeSchema,
  getBacklogItemTypeSchemaNodes
} from '../../api/endpoints/accounts/BacklogItemTypeSchemas.tsx';
import {getBacklogItemTypes} from '../../api/endpoints/accounts/Accounts.tsx';
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
import {paginatedResultsToArray} from "../../api/api-utils.tsx";

type Props = {
  isOpen: boolean;
  backlogItemTypeSchemaID: number;
  onClose: () => void;
  onCreated: (node: BacklogItemTypeSchemaNodeDto) => void;
};

export default function CreateBacklogItemTypeSchemaNodeModal({
  isOpen,
  backlogItemTypeSchemaID,
  onClose,
  onCreated
}: Props) {
  const [isSubmitting, setIsSubmitting] = useState<boolean>(false);
  const [isRefreshing, setIsRefreshing] = useState<boolean>(false);
  const [backlogItemTypes, setBacklogItemTypes] = useState<BacklogItemTypeDto[]>([]);
  const [backlogItemTypeID, setBacklogItemTypeID] = useState<number>(Constants.DEFAULT_VALUE_NUMBER);
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
    setBacklogItemTypeID(Constants.DEFAULT_VALUE_NUMBER);

    const refresh = async () => {
      try {
        const responses = await Promise.all([
          getBacklogItemTypeSchema(backlogItemTypeSchemaID),
          paginatedResultsToArray(page =>
            getBacklogItemTypeSchemaNodes(backlogItemTypeSchemaID, page))
        ]);

        const schemaResponse = responses[0];
        const accountId = schemaResponse.data.account.id;

        const schemaNodes = responses[1];

        const backlogItemTypes = await paginatedResultsToArray(page =>
          getBacklogItemTypes(accountId, page));

        const filteredBacklogItemTypes = backlogItemTypes.filter(itemType =>
          !schemaNodes.some(node => node.backlogItemType.id === itemType.id));
        setBacklogItemTypes(filteredBacklogItemTypes);
      }
      catch(error){
        console.error("Failed to refresh backlog item type schema node modal", error);
      }
      finally {
        setIsRefreshing(false);
      }
    };

    refresh();
  }, [backlogItemTypeSchemaID, isOpen]);

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
      const postDto: BacklogItemTypeSchemaNodePostDto = {
        backlogItemTypeSchemaID,
        backlogItemTypeID
      };

      const response = await createBacklogItemTypeSchemaNode(postDto);

      toast.success('Backlog Item Type Schema Node Created', Constants.DEFAULT_TOAST_PROPS);
      onCreated(response.data);
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
        <h3 style={{marginTop: 0}}>Create Backlog Item Type Schema Node</h3>
        <form onSubmit={handleSubmit}>
          <div className="form-group" style={{marginBottom: 12}}>
            <label htmlFor="schema-node-type">Backlog Item Type *</label>
            <select
              id="schema-node-type"
              className="form-control"
              value={numberToString(backlogItemTypeID)}
              onChange={e => setBacklogItemTypeID(stringToNumber(e.target.value))}
              disabled={isSubmitting || isRefreshing}
              required
            >
              <option value={Constants.DEFAULT_VALUE_STRING}></option>
              {backlogItemTypes.map(itemType => (
                <option key={itemType.id} value={itemType.id}>
                  {itemType.title ?? `#${itemType.id}`}
                </option>
              ))}
            </select>
            <FormError error={formFieldErrors} id="backlogitemtypeid"/>
          </div>

          <div style={{display: 'flex', gap: 8, justifyContent: 'flex-end'}}>
            <FormError error={formSubmissionError}/>
            <button type="button" className="btn btn-secondary" onClick={onClose} disabled={isSubmitting || isRefreshing}>
              Cancel
            </button>
            <button
              type="submit"
              className="btn btn-primary"
              disabled={isSubmitting || isRefreshing || backlogItemTypeID === Constants.DEFAULT_VALUE_NUMBER}
            >
              {(isSubmitting || isRefreshing)
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
