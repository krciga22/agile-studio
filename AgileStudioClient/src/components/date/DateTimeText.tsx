import {formatDate} from "date-fns";
import Constants from "../../Constants.tsx";

type DateTimeTextProps = {
  date:string
}

const DateTimeText = (props:DateTimeTextProps) => {
  const date = formatDate(props.date, Constants.DATE_FORMAT);
  const dateTime = formatDate(props.date, Constants.DATE_TIME_FORMAT);
  return <span title={dateTime}>{date}</span>
};

export default DateTimeText;