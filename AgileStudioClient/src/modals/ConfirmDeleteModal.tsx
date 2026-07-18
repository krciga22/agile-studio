import ConfirmModal from "./ConfirmModal.tsx";
import Constants from "../Constants.tsx";
import type {AxiosResponse} from "axios";

export type ConfirmDeleteModalProps = {
  resourceType: string,
  resourceTitle?: string,
  resourceID?: any,
  deleteEndpoint?: ((resourceID:any) => Promise<AxiosResponse>),
  onDelete?: () => Promise<void> | void,
  onDeleteSuccess?: () => Promise<void> | void,
  onDeleteError?: (error:Error) => Promise<void> | void,
  onCancel?: () => void
}

export default function ConfirmDeleteModal({
  resourceType,
  resourceTitle,
  resourceID,
  deleteEndpoint,
  onDelete,
  onDeleteSuccess,
  onDeleteError,
  onCancel
}: ConfirmDeleteModalProps) {

  const doDelete = async () => {
    if(deleteEndpoint && onDelete){
      throw new Error('Both deleteEndpoint and onDelete are defined. Only use one or the other.');
    }

    if(deleteEndpoint && resourceID == undefined){
      throw new Error('deleteEndpoint requires resourceID to be defined.');
    }

    try{
      if(deleteEndpoint && resourceID){
        await deleteEndpoint(resourceID);
      }
      else if(onDelete){
        await onDelete();
      }

      if(onDeleteSuccess){
        onDeleteSuccess();
      }
    }
    catch(error){
      if(error instanceof Error && onDeleteError){
        onDeleteError(error);
      }
    }
  };

  return (
    <ConfirmModal
      isOpen={resourceID !== undefined && resourceID !== null}
      title={`Delete ${resourceType}`}
      message={
        <>
          <p>Are you sure you want to delete the following {resourceType}?</p>
          <p><strong>{resourceTitle ?? Constants.DEFAULT_VALUE_STRING}</strong></p>
        </>
      }
      confirmText={"Delete"}
      onCancel={onCancel}
      onConfirm={doDelete}
    />
  );
}
