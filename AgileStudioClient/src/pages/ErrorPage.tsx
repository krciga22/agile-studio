import './ErrorPage.css'
import ErrorLayout from "../layouts/ErrorLayout.tsx";

type ErrorPageProps = {
  error: number
}

function ErrorPage(props: ErrorPageProps) {
  return (
    <ErrorLayout error={props.error}>
      <h1>Error {props.error}</h1>
      <p>Oops! Couldn't find exactly what you're looking for.</p>
    </ErrorLayout>
  )
}

export default ErrorPage
