import type {CurrentPageContextValue} from "./services/CurrentPage.tsx";

export const getHomePagePath = () => `/`;
export const getAboutPagePath = () => `/about`;

export const getAccountsPagePath = () => `/accounts`;
export const getAccountPagePath = (accountId: number) => `${getAccountsPagePath()}/${accountId}`;
export const getAccountSettingsPagePath = (accountId: number) => `${getAccountPagePath(accountId)}/settings`;
export const getAccountHomePagePath = (accountId: number) => getAccountSettingsPagePath(accountId);

export const getProjectsPagePath = () => `/projects`;
export const getProjectPagePath = (projectId: number) => `${getProjectsPagePath()}/${projectId}`;
export const getProjectBacklogPagePath = (projectId: number) => `${getProjectPagePath(projectId)}/backlog`;
export const getProjectSprintsPagePath = (projectId: number) => `${getProjectPagePath(projectId)}/sprints`;
export const getProjectReleasesPagePath = (projectId: number) => `${getProjectPagePath(projectId)}/releases`;
export const getProjectSettingsPagePath = (projectId: number) => `${getProjectPagePath(projectId)}/settings`;
export const getProjectHomePagePath = (projectId: number) => getProjectBacklogPagePath(projectId);

export const isCurrentPagePath = (currentPage: CurrentPageContextValue, path: string) => {
  return currentPage.pathname === path;
}

export const isCurrentPageBasePath = (currentPage: CurrentPageContextValue, path: string) => {
  return currentPage.pathname ?
      currentPage.pathname.startsWith(path) : false;
}