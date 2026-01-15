import './QuickNav.css'
import type {ReactElement} from "react";
import type {Project} from "../../../models/Project.tsx";
import type {BacklogItem} from "../../../models/BacklogItem.tsx";

function QuickNav() {

  const projects:Project[] = [];
  for(let i=0; i<5; i++){
    const id = i + 1;
    projects.push({
      id: id,
      title: 'Project ' + id
    });
  }

  const backlogItems:BacklogItem[] = [];
  for(let i=0; i<5; i++){
    const id = i + 1;
    backlogItems.push({
      id: id,
      title: 'Backlog Item ' + id
    });
  }

  const renderProjectListItems = (projects:Project[]) => {
    const listItems:ReactElement[] = [];

    projects.forEach(project => {
      const href = `/projects/${project.id}`
      listItems.push(
        <li className="list-group-item">
          <a href={href}>{project.title}</a>
        </li>
      )
    });

    return listItems;
  };

  const renderBacklogItems = (backlogItems:BacklogItem[]) => {
    const listItems:ReactElement[] = [];

    backlogItems.forEach(backlogItem => {
      const href = `/backlog-items/${backlogItem.id}`
      listItems.push(
        <li className="list-group-item">
          <a href={href}>{backlogItem.title}</a>
        </li>
      )
    });

    return listItems;
  };

  return (
    <nav className={"QuickNav flex-shrink-1 overflow-scroll me-sm-4 p-3"}>
      <div className={"mb-2"}>
        <p>Projects:</p>
        <ul className="list-group">
          {renderProjectListItems(projects)}
        </ul>
      </div>
      <div className={"mb-2"}>
        <p>Backlog Items:</p>
        <ul className="list-group">
          {renderBacklogItems(backlogItems)}
        </ul>
      </div>
    </nav>
  )
}

export default QuickNav
