import './BlankLayout.css'
import type {ReactNode} from "react";

type BlankLayoutProps = {
  children: ReactNode,
  centered?: boolean
}

function BlankLayout(props: BlankLayoutProps) {
  const { centered = true } = props;

  let className = "BlankLayout h-100";
  if (centered) {
    className += " d-flex justify-content-center align-items-center";
  }

  return (
    <div className={className}>{props.children}</div>
  )
}

export default BlankLayout
