import './ErrorPage.css'

type ErrorPageProps = {
  error: number
}

function ErrorPage(props: ErrorPageProps) {
  return (
    <div className={"ErrorPage"}>
      <h1>Error {props.error}</h1>
      <p>Oops! Couldn't find exactly what you're looking for.</p>
    </div>
  )
}

export default ErrorPage
