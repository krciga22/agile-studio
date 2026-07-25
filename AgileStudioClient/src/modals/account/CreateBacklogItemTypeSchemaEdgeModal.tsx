import React, {useEffect, useState} from 'react';
import type {BacklogItemTypeDto} from '../../api/dtos/accounts/BacklogItemTypeDtos.tsx';
import type {BacklogItemTypeSchemaEdgeDto, BacklogItemTypeSchemaEdgePostDto} from '../../api/dtos/accounts/BacklogItemTypeSchemaEdgeDtos.tsx';
import {
  createBacklogItemTypeSchemaEdge,
  getBacklogItemTypeSchema,
  getBacklogItemTypeSchemaEdges
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

type Mode = 'from' | 'to';

type Props = {
  isOpen: boolean;
  mode: Mode;
  backlogItemTypeSchemaID: number;
  targetBacklogItemTypeID?: number | undefined;
  onClose: () => void;
  onCreated: (edge: BacklogItemTypeSchemaEdgeDto) => void;
};

export default function CreateBacklogItemTypeSchemaEdgeModal({
  isOpen,
  mode,
  backlogItemTypeSchemaID,
  targetBacklogItemTypeID,
  onClose,
  onCreated
}: Props) {
  const [isSubmitting, setIsSubmitting] = useState<boolean>(false);
  const [isRefreshing, setIsRefreshing] = useState<boolean>(false);
  const [backlogItemTypes, setBacklogItemTypes] = useState<BacklogItemTypeDto[]>([]);
  const [selectedBacklogItemTypeID, setSelectedBacklogItemTypeID] = useState<number>(Constants.DEFAULT_VALUE_NUMBER);
  const [isStartSelected, setIsStartSelected] = useState<boolean>(false);
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
    setSelectedBacklogItemTypeID(Constants.DEFAULT_VALUE_NUMBER);
    setIsStartSelected(false);

    const refresh = async () => {
      try {
        const responses = await Promise.all([
          getBacklogItemTypeSchema(backlogItemTypeSchemaID),
          paginatedResultsToArray(page =>
            getBacklogItemTypeSchemaEdges(backlogItemTypeSchemaID, {page}))
        ]);

        const schemaResponse = responses[0];
        const accountId = schemaResponse.data.account.id;

        const backlogItemTypeSchemaEdges = responses[1];

        const backlogItemTypes = await paginatedResultsToArray(page =>
          getBacklogItemTypes(accountId, page));

        const filteredBacklogItemTypes = backlogItemTypes.filter(backlogItemType =>
          !backlogItemTypeSchemaEdges.some(edge => edge.toType.id == targetBacklogItemTypeID && edge?.fromType?.id === backlogItemType.id));
        setBacklogItemTypes(filteredBacklogItemTypes);
      }
      catch(error){
        console.error("Failed to refresh backlog item type schema edge modal", error);
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
      if(targetBacklogItemTypeID === undefined){
        throw new Error('Target backlog item type must be defined');
      }

      let postDto: BacklogItemTypeSchemaEdgePostDto;

      if(mode === 'from'){
        postDto = {
          backlogItemTypeSchemaID,
          fromTypeID: isStartSelected ? null : (selectedBacklogItemTypeID === Constants.DEFAULT_VALUE_NUMBER ? null : selectedBacklogItemTypeID),
          toTypeID: targetBacklogItemTypeID
        };
      }
      else {
        postDto = {
          backlogItemTypeSchemaID,
          fromTypeID: targetBacklogItemTypeID,
          toTypeID: selectedBacklogItemTypeID
        };
      }

      const response = await createBacklogItemTypeSchemaEdge(postDto);

      toast.success('Backlog Item Type Schema Edge Created', Constants.DEFAULT_TOAST_PROPS);
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
        <h3 style={{marginTop: 0}}>{mode === 'from' ? 'Add From Type' : 'Add To Type'}</h3>
        <form onSubmit={handleSubmit}>
          <div className="form-group" style={{marginBottom: 12}}>
            <label htmlFor="schema-edge-type">Backlog Item Type *</label>
            <select
              id="schema-edge-type"
              className="form-control"
              value={isStartSelected ? '__start__' : numberToString(selectedBacklogItemTypeID)}
              onChange={e => {
                const v = e.target.value;
                if(v === '__start__'){
                  setIsStartSelected(true);
                  setSelectedBacklogItemTypeID(Constants.DEFAULT_VALUE_NUMBER);
                }
                else if(v === Constants.DEFAULT_VALUE_STRING){
                  setIsStartSelected(false);
                  setSelectedBacklogItemTypeID(Constants.DEFAULT_VALUE_NUMBER);
                }
                else {
                  setIsStartSelected(false);
                  setSelectedBacklogItemTypeID(stringToNumber(v));
                }
              }}
              disabled={isSubmitting || isRefreshing}
              required
            >
              <option value={Constants.DEFAULT_VALUE_STRING}></option>
              {mode === 'from' && (
                <option value={'__start__'}>[start]</option>
              )}
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
              disabled={isSubmitting || isRefreshing || (mode === 'from' ? (!isStartSelected && selectedBacklogItemTypeID === Constants.DEFAULT_VALUE_NUMBER) : selectedBacklogItemTypeID === Constants.DEFAULT_VALUE_NUMBER)}
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
