import React, {useEffect, useState} from 'react';
import { createProject } from '../services/api/endpoints/project.tsx';
import type { ProjectDto } from '../services/api/dtos/ProjectDtos.tsx';
import type {BacklogItemTypeSchemaDto} from "../services/api/dtos/BacklogItemTypeSchemaDtos.tsx";
import {getBacklogItemTypeSchemas} from "../services/api/endpoints/BacklogItemTypeSchema.tsx";
import {faSpinner} from "@fortawesome/free-solid-svg-icons";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import type {BacklogItemLinkTypeSchemaDto} from "../services/api/dtos/BacklogItemLinkTypeSchemaDtos.tsx";
import {getBacklogItemLinkTypeSchemas} from "../services/api/endpoints/BacklogItemLinkTypeSchema.tsx";
import Constants from "../Constants.tsx";

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
  const [backlogItemTypeSchemaId, setBacklogItemTypeSchemaId] = useState<string>(defaultValue);
  const [backlogItemLinkTypeSchemaId, setBacklogItemLinkTypeSchemaId] = useState<string>(defaultValue);
  const [backlogItemTypeSchemas, setBacklogItemTypeSchemas] = useState<BacklogItemTypeSchemaDto[]>([]);
  const [backlogItemLinkTypeSchemas, setBacklogItemLinkTypeSchemas] = useState<BacklogItemLinkTypeSchemaDto[]>([]);

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

    // todo do these in parallel
    const backlogItemTypeSchemasResponse = await getBacklogItemTypeSchemas();
    const backlogItemLinkTypeSchemasResponse = await getBacklogItemLinkTypeSchemas();

    setBacklogItemTypeSchemas(backlogItemTypeSchemasResponse.data);
    setBacklogItemLinkTypeSchemas(backlogItemLinkTypeSchemasResponse.data);

    setTimeout(() => {
      setIsRefreshing(false);
    }, Constants.EXTRA_WAIT_TIME_MS);
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (isSubmitting) return;

    setIsSubmitting(true);
    try {
      const response = await createProject({
        title: title.trim(),
        description: description.trim(),
        backlogItemTypeSchemaId: parseInt(backlogItemTypeSchemaId),
        backlogItemLinkTypeSchemaId: parseInt(backlogItemLinkTypeSchemaId)
      });

      onCreated(response.data);

      setTitle('');
      setDescription('');
      setBacklogItemTypeSchemaId(defaultValue);
      setBacklogItemLinkTypeSchemaId(defaultValue);

      onClose();
    }
    catch (err) {
      console.error('Create project failed', err);
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
        console.error('Error refreshing backlog item type schemas:', error);
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
              required={true}
            />
          </div>

          <div className="form-group" style={{ marginBottom: 12 }}>
            <label htmlFor="project-backlog-item-type-schema">Backlog Item Type Schema *</label>
            <select
              id="project-backlog-item-type-schema"
              className="form-control"
              value={backlogItemTypeSchemaId}
              onChange={e => setBacklogItemTypeSchemaId(e.target.value)}
              disabled={isWorking}
              required={true}
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
          </div>

          <div className="form-group" style={{ marginBottom: 12 }}>
            <label htmlFor="project-backlog-item-link-type-schema">Backlog Item Link Type Schema *</label>
            <select
              id="project-backlog-item-link-type-schema"
              className="form-control"
              value={backlogItemLinkTypeSchemaId}
              onChange={e => setBacklogItemLinkTypeSchemaId(e.target.value)}
              disabled={isWorking}
              required={true}
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
          </div>

          <div style={{ display: 'flex', gap: 8, justifyContent: 'flex-end' }}>
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
