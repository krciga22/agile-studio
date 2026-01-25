import './InitPage.css'
import BlankLayout from "../layouts/BlankLayout.tsx";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faSpinner} from "@fortawesome/free-solid-svg-icons";

function InitPage() {
  return (
    <BlankLayout centered={true}>
      <div className={"InitPage d-flex"}>
        <h3 className={"m-0"}>
          <FontAwesomeIcon icon={faSpinner} size={"lg"} spin={true} className={"me-2"}></FontAwesomeIcon>
          Initializing
        </h3>
      </div>
    </BlankLayout>
  )
}

export default InitPage
