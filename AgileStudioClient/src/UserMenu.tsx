import './UserMenu.css'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faUser } from '@fortawesome/free-solid-svg-icons'

function UserMenu() {
  return (
    <div className={"UserMenu"}>
      <FontAwesomeIcon icon={faUser} size={"lg"}></FontAwesomeIcon>
      <span>Guest</span>
    </div>
  )
}

export default UserMenu
