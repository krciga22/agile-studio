import React, {useState, useRef} from 'react';
import { faSpinner } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import FormError from '../components/form/FormError.tsx';
import {debounce} from '../Utils.tsx';
import Constants from "../Constants.tsx";
import {
  ERROR_CONTEXT,
  ERROR_MESSAGE_DEFAULT,
  getErrorMessageForAxiosError,
} from "../services/util/error.tsx";
import axios from "axios";
import Modal from '../components/modal/Modal';
import {toast} from "react-toastify";

type Props = {
  isOpen: boolean;
  title?: string;
  message?: React.ReactNode;
  confirmText?: string;
  cancelText?: string;
  onCancel?: () => void;
  onConfirm?: () => Promise<void> | void;
}

export default function ConfirmModal({
  isOpen,
  title = 'Confirm',
  message,
  confirmText = 'Confirm',
  cancelText = 'Cancel',
  onCancel,
  onConfirm
}: Props) {
  const [isWorking, setIsWorking] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const confirmTimeoutRef = useRef<number | null>(null);

  const handleConfirm = () => {
    if (isWorking) return;

    setIsWorking(true);
    setError(null);

    debounce(async () => {
      try {
        if (onConfirm) {
          await onConfirm();
        }
      }
      catch (err: unknown) {
        let errorMessage = ERROR_MESSAGE_DEFAULT;

        if (axios.isAxiosError(err) && err.response) {
          errorMessage = getErrorMessageForAxiosError(
            err, ERROR_CONTEXT.FORM_SUBMISSION);
        }

        setError(errorMessage);

        toast.error(errorMessage, Constants.DEFAULT_TOAST_PROPS);
      }
      finally {
        setIsWorking(false);
      }
    }, Constants.EXTRA_WAIT_TIME_MS, confirmTimeoutRef);
  }

  const handleCancel = () => {
    if (isWorking) return;

    setError(null);

    if (onCancel) onCancel();
  };

  if (!isOpen) return null;

  return (
    <Modal isOpen={isOpen} onClose={handleCancel} title={title}>
      {message && <div className={"mb-3"}>{message}</div>}

      <div className={"d-flex justify-content-end gap-2"}>
        <FormError error={error} />

        <button
          type="button"
          className="btn btn-secondary"
          onClick={handleCancel}
          disabled={isWorking}>{cancelText}</button>

        <button
          type="button"
          className="btn btn-danger"
          onClick={handleConfirm}
          disabled={isWorking}>
          {isWorking ? <FontAwesomeIcon icon={faSpinner} size={"lg"} spin={true}></FontAwesomeIcon> : confirmText}
        </button>
      </div>
    </Modal>
  );
}
