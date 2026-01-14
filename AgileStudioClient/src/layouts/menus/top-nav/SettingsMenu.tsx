import './SettingsMenu.css'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faCog } from '@fortawesome/free-solid-svg-icons'

function SettingsMenu() {
  return (
    <div className={"SettingsMenu"}>
      <FontAwesomeIcon icon={faCog} size={"lg"}></FontAwesomeIcon>
    </div>
  )
}

export default SettingsMenu
