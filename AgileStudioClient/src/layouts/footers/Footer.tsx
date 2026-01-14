import './Footer.css'
import Constants from "../../Constants.tsx";

function Footer() {
  return (
    <footer className={"p-2"}>
      {Constants.PRODUCT_NAME} v{Constants.PRODUCT_VERSION}
    </footer>
  )
}

export default Footer
