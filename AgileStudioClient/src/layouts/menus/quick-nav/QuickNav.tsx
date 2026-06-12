import './QuickNav.css'
import type {ReactElement} from "react";
import type {BacklogItem} from "../../../models/BacklogItem.tsx";
import ProjectMenu from "./ProjectsMenu.tsx";
import AccountsMenu from "./AccountsMenu.tsx";

function QuickNav() {

  const backlogItems:BacklogItem[] = [];
  for(let i=0; i<5; i++){
    const id = i + 1;
    backlogItems.push({
      id: id,
      title: 'Backlog Item ' + id
    });
  }

  const renderBacklogItems = (backlogItems:BacklogItem[]) => {
    const listItems:ReactElement[] = [];

    backlogItems.forEach(backlogItem => {
      const href = `/backlog-items/${backlogItem.id}`
      listItems.push(
        <li key={backlogItem.id} className="list-group-item">
          <a href={href}>{backlogItem.title}</a>
        </li>
      )
    });

    return listItems;
  };

  return (
    <nav className={"QuickNav flex-shrink-1 overflow-scroll me-sm-4 p-3"}>
      <div className={"mb-2"}>
        <AccountsMenu></AccountsMenu>
      </div>
      <div className={"mb-2"}>
        <ProjectMenu></ProjectMenu>
      </div>
      <div className={"mb-2"}>
        <p>Backlog Items:</p>
        <ul className="QuickNavMenu list-group">
          {renderBacklogItems(backlogItems)}
        </ul>
      </div>
    </nav>
  )
}

export default QuickNav
