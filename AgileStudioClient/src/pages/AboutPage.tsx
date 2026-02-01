import './AboutPage.css'
import {useEffect} from "react";
import Utils from "../Utils.tsx";

function AboutPage() {

  useEffect(() => {
    Utils.setDocumentTitle("About");
  }, []);

  return (
    <div className={"AboutPage"}>
      <h1>About</h1>
    </div>
  )
}

export default AboutPage
