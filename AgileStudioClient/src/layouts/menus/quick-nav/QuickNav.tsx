import './QuickNav.css'
import type {ReactElement} from "react";


type Project = {
  id: number,
  title: string
};


function QuickNav() {

  const projects:Project[] = [];
  for(let i=0; i<5; i++){
    const id = i + 1;
    projects.push({
      id: id,
      title: 'Project ' + id
    });
  }

  const renderProjectListItems = (projects:Project[]) => {
    const projectListItems:ReactElement[] = [];

    projects.forEach(project => {
      const href = `/projects/${project.id}`
      projectListItems.push(
        <li className="list-group-item">
          <a href={href}>{project.title}</a>
        </li>
      )
    });

    return projectListItems;
  };

  return (
    <nav className={"QuickNav flex-shrink-1 overflow-scroll me-sm-4 p-3"}>
      <p>Projects:</p>
      <ul className="list-group">
        {renderProjectListItems(projects)}
      </ul>
    </nav>
  )
}

export default QuickNav
