import './HomePage.css'
import MainLayout from "../layouts/MainLayout.tsx";
import {useEffect} from "react";
import Utils from "../Utils.tsx";

function HomePage() {

  useEffect(() => {
    Utils.setDocumentTitle("Home");
  }, []);

  return (
    <MainLayout>
      <h1>Home</h1>
    </MainLayout>
  )
}

export default HomePage
