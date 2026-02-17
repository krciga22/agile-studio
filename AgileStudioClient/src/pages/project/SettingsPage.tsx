import './SettingsPage.css'
import React, {useContext, useEffect, useState} from "react";
import Utils, {debounce} from "../../Utils.tsx";
import type {ProjectDto} from "../../services/api/dtos/ProjectDtos.tsx";
import CurrentUserContext from "../../services/CurrentUser.tsx";
import {getProject} from "../../services/api/endpoints/project.tsx";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faSpinner} from "@fortawesome/free-solid-svg-icons";
import type {BacklogItemTypeSchemaDto} from "../../services/api/dtos/BacklogItemTypeSchemaDtos.tsx";
import type {BacklogItemLinkTypeSchemaDto} from "../../services/api/dtos/BacklogItemLinkTypeSchemaDtos.tsx";
import {getBacklogItemTypeSchemas} from "../../services/api/endpoints/BacklogItemTypeSchema.tsx";
import {getBacklogItemLinkTypeSchemas} from "../../services/api/endpoints/BacklogItemLinkTypeSchema.tsx";
import Constants from "../../Constants.tsx";
import type {AxiosResponse} from "axios";

type SettingsPageProps = {
  projectId: number
}

function SettingsPage(props: SettingsPageProps) {
  const defaultValue:string = "";
  const {projectId} = props;
  const [isRefreshing, setIsRefreshing] = useState<boolean|null>(null);
  const [project, setProject] = useState<ProjectDto|null>(null);
  const [backlogItemTypeSchemas, setBacklogItemTypeSchemas] = useState<BacklogItemTypeSchemaDto[]>([]);
  const [backlogItemLinkTypeSchemas, setBacklogItemLinkTypeSchemas] = useState<BacklogItemLinkTypeSchemaDto[]>([]);
  const currentUser = useContext(CurrentUserContext);
  const refreshTimeoutIdRef = React.useRef<number | null>(null);

  useEffect(() => {
    if(project){
      Utils.setDocumentTitle(`Settings - ${project.title}`);
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

  if((project === null || project.id !== projectId) && !currentUser.isLoading && currentUser.user){
    refresh();
  }

  return (
    <div className={"SettingsPage"}>
      { isRefreshing !== false && <FontAwesomeIcon icon={faSpinner} size={"lg"} spin={true}></FontAwesomeIcon> }

      {
        isRefreshing === false && project &&
          <div>
              <h1>Settings</h1>

              <div className={"py-4"} style={{maxWidth: '700px'}}>
                  <div className={"row py-2"}>
                      <div className={"col col-12 col-md-5 text-start"}>
                          <label>Project Name</label>
                      </div>
                      <div className={"col col-12 col-md-7"}>
                          <input className={"form-control"} type={"text"} defaultValue={project.title} />
                      </div>
                  </div>

                  <div className={"row py-2"}>
                      <div className={"col col-12 col-md-5 text-start"}>
                          <label>Backlog Item Type Schema</label>
                      </div>
                      <div className={"col col-12 col-md-7"}>
                          <select className={"form-control"} defaultValue={project.backlogItemTypeSchema.id}>
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
                  </div>

                  <div className={"row py-2"}>
                      <div className={"col col-12 col-md-5 text-start"}>
                          <label>Backlog Item Link Type Schema</label>
                      </div>
                      <div className={"col col-12 col-md-7"}>
                          <select className={"form-control"} defaultValue={project.backlogItemLinkTypeSchema.id}>
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
                  </div>

                  <div className={"row py-2"}>
                      <div className={"col col-12 col-md-5 text-start"}>
                          <label>Description</label>
                      </div>
                      <div className={"col col-12 col-md-7"}>
                          <textarea className={"form-control"} defaultValue={project.description}></textarea>
                      </div>
                  </div>

                  <div className={"row py-2"}>
                      <div className={"col col-12 col-md-5 text-start text-md-end"}>

                      </div>
                      <div className={"col col-12 col-md-7 d-flex justify-content-end"}>
                          <button className={"btn btn-primary"}>Save</button>
                      </div>
                  </div>
              </div>

          </div>
      }
    </div>
  )
}

export default SettingsPage
