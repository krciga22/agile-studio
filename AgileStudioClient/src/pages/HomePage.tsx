import './HomePage.css'
import {useEffect} from "react";
import Utils from "../Utils.tsx";

function HomePage() {

  useEffect(() => {
    Utils.setDocumentTitle("Home");
  }, []);

  return (
    <div className={"HomePage"}>
      <h1>Home</h1>
    </div>
  )
}

export default HomePage
