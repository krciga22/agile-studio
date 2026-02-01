import './InitPage.css'
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faSpinner} from "@fortawesome/free-solid-svg-icons";

function InitPage() {
  return (
    <div className={"InitPage d-flex"}>
      <h3 className={"m-0"}>
        <FontAwesomeIcon icon={faSpinner} size={"lg"} spin={true} className={"me-2"}></FontAwesomeIcon>
        Initializing
      </h3>
    </div>
  )
}

export default InitPage
