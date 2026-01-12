import './AboutPage.css'
import MainLayout from "../layouts/MainLayout.tsx";
import {useEffect} from "react";
import Utils from "../Utils.tsx";

function AboutPage() {

  useEffect(() => {
    Utils.setDocumentTitle("About");
  }, []);

  return (
    <MainLayout>
      <h1>About</h1>
    </MainLayout>
  )
}

export default AboutPage
