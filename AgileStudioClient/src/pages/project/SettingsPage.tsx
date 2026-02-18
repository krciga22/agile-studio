import './SettingsPage.css'
import React, {useContext, useEffect, useState} from "react";
import Utils, {debounce, numberToString, stringToNumber} from "../../Utils.tsx";
import type {ProjectDto} from "../../services/api/dtos/ProjectDtos.tsx";
import CurrentUserContext from "../../services/CurrentUser.tsx";
import {getProject, updateProject} from "../../services/api/endpoints/project.tsx";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faSpinner} from "@fortawesome/free-solid-svg-icons";
import type {BacklogItemTypeSchemaDto} from "../../services/api/dtos/BacklogItemTypeSchemaDtos.tsx";
import type {BacklogItemLinkTypeSchemaDto} from "../../services/api/dtos/BacklogItemLinkTypeSchemaDtos.tsx";
import {getBacklogItemTypeSchemas} from "../../services/api/endpoints/BacklogItemTypeSchema.tsx";
import {getBacklogItemLinkTypeSchemas} from "../../services/api/endpoints/BacklogItemLinkTypeSchema.tsx";
import Constants from "../../Constants.tsx";
import axios, {type AxiosResponse} from "axios";
import {ERROR_CONTEXT, ERROR_MESSAGE_DEFAULT, getErrorMessageForAxiosError} from "../../services/util/error.tsx";
import {getProblemDetailsErrorMapFromResponse} from "../../services/api/Api.tsx";
import type {ProblemDetailsErrorMap} from "../../services/api/dtos/ProblemDetailsDtos.tsx";
import FormError from "../../components/form/FormError.tsx";

type SettingsPageProps = {
  projectId: number
}

function SettingsPage(props: SettingsPageProps) {
  const {projectId} = props;
  const [isWorking, setIsWorking] = useState(false);
  const [isRefreshing, setIsRefreshing] = useState<boolean>(false);
  const [isSubmitting, setIsSubmitting] = useState<boolean>(false);
  const [project, setProject] = useState<ProjectDto|null>(null);
  const [backlogItemTypeSchemas, setBacklogItemTypeSchemas] = useState<BacklogItemTypeSchemaDto[]>([]);
  const [backlogItemLinkTypeSchemas, setBacklogItemLinkTypeSchemas] = useState<BacklogItemLinkTypeSchemaDto[]>([]);
  const [title, setTitle] = useState(Constants.DEFAULT_VALUE_STRING);
  const [description, setDescription] = useState(Constants.DEFAULT_VALUE_STRING);
  const [backlogItemTypeSchemaId, setBacklogItemTypeSchemaId] = useState<number>(Constants.DEFAULT_VALUE_NUMBER);
  const [backlogItemLinkTypeSchemaId, setBacklogItemLinkTypeSchemaId] = useState<number>(Constants.DEFAULT_VALUE_NUMBER);
  const currentUser = useContext(CurrentUserContext);
  const refreshTimeoutIdRef = React.useRef<number | null>(null);
  const submitTimeoutIdRef = React.useRef<number | null>(null);
  const [formFieldErrors, setFormFieldErrors] = useState<ProblemDetailsErrorMap>({});
  const [formSubmissionError, setFormSubmissionError] = useState<string|null>(null);
  const [enableRequiredFieldValidation] = useState<boolean>(true);

  useEffect(() => {
    setIsWorking(isRefreshing || isSubmitting);
  }, [isRefreshing, isSubmitting]);

  useEffect(() => {
    if(project){
      Utils.setDocumentTitle(`Settings - ${project.title}`);
      setTitle(project.title ?? Constants.DEFAULT_VALUE_STRING);
      setDescription(project.description ?? Constants.DEFAULT_VALUE_STRING);
      setBacklogItemTypeSchemaId(project.backlogItemTypeSchema.id);
      setBacklogItemLinkTypeSchemaId(project.backlogItemLinkTypeSchema.id);
    }
  }, [project]);

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
        getProject(projectId),
        getBacklogItemTypeSchemas(),
        getBacklogItemLinkTypeSchemas()
      ];

      const responses = await Promise.all(promises);

      const projectResponse =
        responses[0] as AxiosResponse<ProjectDto>;

      const backlogItemTypeSchemasResponse =
        responses[1] as AxiosResponse<BacklogItemTypeSchemaDto[]>;

      const backlogItemLinkTypeSchemasResponse =
        responses[2] as AxiosResponse<BacklogItemLinkTypeSchemaDto[]>;

      setProject(projectResponse.data);
      setBacklogItemTypeSchemas(backlogItemTypeSchemasResponse.data);
      setBacklogItemLinkTypeSchemas(backlogItemLinkTypeSchemasResponse.data);
    }
    catch(e){
      console.error("Failed to refresh project settings", e);
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
    try {
      await updateProject(projectId, {
        id: projectId,
        title: title.trim(),
        description: description.trim()
        // todo include backlogItemTypeSchemaId
        // todo include backlogItemLinkTypeSchemaId
      });
    }
    catch (err) {
      console.log(err);
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
      setIsSubmitting(false);
    }
  };

  if((project === null || project.id !== projectId) && !currentUser.isLoading && currentUser.user){
    refresh();
  }

  return (
    <div className={"SettingsPage"}>
      { isRefreshing && <FontAwesomeIcon icon={faSpinner} size={"lg"} spin={true}></FontAwesomeIcon> }

      {
        !isRefreshing && project &&
          <div>
              <h1>Settings</h1>

              <form className={"py-4"} style={{maxWidth: '700px'}} onSubmit={handleSubmit}>
                  <div className={"row py-2"}>
                      <div className={"col col-12 col-md-5 text-start"}>
                          <label>Project Name *</label>
                      </div>
                      <div className={"col col-12 col-md-7"}>
                        <input className={"form-control"}
                          type={"text"}
                          value={title}
                          onChange={e => setTitle(e.target.value)}
                          disabled={isWorking}
                          required={enableRequiredFieldValidation}
                        />
                        <FormError error={formFieldErrors} id="title" />
                      </div>
                  </div>

                  <div className={"row py-2"}>
                      <div className={"col col-12 col-md-5 text-start"}>
                          <label>Backlog Item Type Schema *</label>
                      </div>
                      <div className={"col col-12 col-md-7"}>
                          <select
                            className={"form-control"}
                            value={numberToString(backlogItemTypeSchemaId)}
                            onChange={e => {
                              setBacklogItemTypeSchemaId(
                                stringToNumber(e.target.value))
                            }}
                            disabled={true}
                            required={enableRequiredFieldValidation}
                          >
                              <option value={Constants.DEFAULT_VALUE_STRING}></option>

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
                  </div>

                  <div className={"row py-2"}>
                      <div className={"col col-12 col-md-5 text-start"}>
                          <label>Backlog Item Link Type Schema *</label>
                      </div>
                      <div className={"col col-12 col-md-7"}>
                          <select
                              className={"form-control"}
                              value={numberToString(backlogItemLinkTypeSchemaId)}
                              onChange={e => {
                                setBacklogItemLinkTypeSchemaId(
                                  stringToNumber(e.target.value))
                              }}
                              disabled={true}
                              required={enableRequiredFieldValidation}
                          >
                              <option value={Constants.DEFAULT_VALUE_STRING}></option>

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
                  </div>

                  <div className={"row py-2"}>
                      <div className={"col col-12 col-md-5 text-start"}>
                          <label>Description</label>
                      </div>
                      <div className={"col col-12 col-md-7"}>
                          <textarea
                              className={"form-control"}
                              value={description}
                              onChange={e => setDescription(e.target.value)}
                              disabled={isWorking}
                          ></textarea>
                          <FormError error={formFieldErrors} id="description" />
                      </div>
                  </div>

                  <div className={"row py-2"}>
                      <div className={"col col-12 col-md-5 text-start text-md-end"}></div>
                      <div className={"col col-12 col-md-7 d-flex justify-content-end gap-8"}>
                        <FormError error={formSubmissionError} />
                        <button type="submit" className={"btn btn-primary"} disabled={isWorking}>
                          {
                            isWorking ?
                              <FontAwesomeIcon icon={faSpinner} size={"lg"} spin={true}></FontAwesomeIcon> :
                              'Save'
                          }
                        </button>
                      </div>
                  </div>
              </form>

          </div>
      }
    </div>
  )
}

export default SettingsPage
