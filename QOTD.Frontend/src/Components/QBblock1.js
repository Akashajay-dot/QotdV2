import React, { useContext, useEffect, useState } from 'react'
import "../Styles/QBblock.css"
import { useNavigate } from 'react-router-dom';
import { GlobalStateContext } from '../Context/GlobalStateContext';
import axios from 'axios';
import circle from "../Assets/circle.png";
import tick from "../Assets/tick.png";
import edit from "../Assets/edit.png";
import { DemoContainer } from '@mui/x-date-pickers/internals/demo';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import dayjs from 'dayjs';
import moment from 'moment';
// import CancelIcon from '@mui/icons-material/Cancel';
import utc from 'dayjs/plugin/utc';
import CheckCircleIcon from '@mui/icons-material/CheckCircle';
import CancelIcon from '@mui/icons-material/Cancel';
dayjs.extend(utc);

function QBblock1({id,unAnswered}) {
  const { state  ,setLoading,setQId,setDates} = useContext(GlobalStateContext);
  const navigate =useNavigate();
  const [Question, setQuestion] = useState('');
  const [QuestionDate, setQuestionDate] = useState(null);
  const [created, setCreated] = useState('');
  const [value, setValue] = React.useState(null);
  const [IsApproved, setIsApproved] = useState('');
  const [ isActive ,setIsactive ] = useState();
  const [toggle ,setToggle] = useState(false);
  const [isToday ,setIsToday] = useState();
  const [isCorrect ,setIscorrect]=useState();
  const minDate = dayjs();
  // .add(1, 'day')
  // const [dates, setDates] = useState([]);
   const today = dayjs();
const apiBaseUrl = process.env.REACT_APP_API_BASE_URL;


   

const approve=()=>{
//   setQId(id);
 
navigate(`/prevQuestionspage/${id}`);
}
useEffect(() => {
 

  fetchDates();
}, []);
const fetchDates = async () => {
  try {
    const response = await axios.get(`${apiBaseUrl}/api/questions/dates`);
    setDates(response.data.map(date => dayjs(date).format('YYYY-MM-DD')));
  } catch (error) {
    console.error('Error fetching question dates:', error);
  }
};
useEffect(()=>{
  const checkcorrect =async ()=>{

  
  try{
    const response = await axios.get(`${apiBaseUrl}/api/Questions/IsCorrect/${id}`);
    setIscorrect(response.data.IsCorrect)
    console.log(response);

  }
  catch{
    console.log("error")
  }
}
checkcorrect();
})
useEffect(() => {
   
  const fetchQuestion = async () => {
    try {
      
      const response = await axios.get(`${apiBaseUrl}/api/FetchQuestion/${id}`);
      if (response.status === 200) {
        // console.log(response);
        setQuestion(response.data[0].Question.Question);
        setIsactive(response.data[0].Question.IsActive);
        setIsApproved(response.data[0].Question.IsApproved);
        setCreated((response.data[0].Question.CreatedBy))
        if((response.data[0].Question.QuestionDate)!=null){

        const dateTimeString = (response.data[0].Question.QuestionDate)
    
const dateOnly = moment(dateTimeString).format('YYYY-MM-DD');
setQuestionDate(dateOnly);
const date=response.data[0].Question.QuestionDate;
// console.log(dayjs(date));
 setIsToday( (dayjs(date).isSame(today, 'day')));
//  console.log( ""+(dayjs(date).isSame(minDate, 'day')));
 


        }
        // setValue(QuestionDate);
      } else {
        console.log('No question available for today.');

      }
    } catch (error) {

      console.log('An error occurred while fetching the question.');
      console.error('Error fetching daily question:', error);
    }
    

    
  };

fetchQuestion();
// setIsToday( (dayjs(QuestionDate).isSame(today, 'day')));

   

},[id]);
// const deleteQuestion=async ()=>{
//   try {
//     const response = await axios.delete(`http://localhost:57101/api/delete/${id}`);
//     if (response.status === 204) {
//       console.log("Question deleted successfully");
//       // Optionally, update your UI here to reflect the deletion

//   }
//   }
//   catch (error){
//     console.log(error);
//   }
// }




return (
    <div >
      <div className="QBblock">
      <button className='QBblock1' onClick={approve} disabled={!unAnswered} >
        <div className="QBleft">
      {(!unAnswered && isCorrect) && <img src={tick} className='approveIndicator'/>}
      {(!unAnswered && !isCorrect) && <CancelIcon sx={{ color: 'red' }} />}
            
<div className="QbQuestion">
<p className='QuestionQB'>{Question}</p>
<p className="QBdate">Created By:{created}</p>
</div></div>
</button>

<div className="QBright">

  <p className='dateInactive'>{QuestionDate}</p>


      </div>
      </div>
    </div>
  )
}

export default QBblock1