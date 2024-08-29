import React, { useContext, useEffect, useState } from "react";

import "../Styles/QuestionBank.css";
import { GlobalStateContext } from "../Context/GlobalStateContext";
import QBblock from "./QBblock";
import axios from "axios";
import DropDown from "./DropDown";
import SearchIcon from '@mui/icons-material/Search';
import CloseIcon from '@mui/icons-material/Close';

function QuestionBank() {
  const { state, setLoading} = useContext(GlobalStateContext);
  const [selection , setSelection ]= useState(null);
   const [all , setAll ]= useState(true);
  const [published , setPublished ]= useState(false);
  const [previous , setPrevious ]= useState(false);

  const [cat , setcat ]= useState("Category");
  const [catid , setcatid ]= useState(null);
  const [filter , setFilter]= useState('all');


  const [unPublished , setunPublished]= useState(false);


  const [QIds , setQids] = useState([]);
 const [searchtxt ,setSearchtext ]= useState('');
 const [searchtxt2 ,setSearchtext2 ]= useState(null);
const [toggle , setToggle ] = useState(true);
const apiBaseUrl = process.env.REACT_APP_API_BASE_URL;


  useEffect(()=>{
  setLoading(false);
Qid();
 

  },[filter,catid,toggle,all])
  const Qid = async ()=>{
    try{
    
        const Qids = await axios.get(`${apiBaseUrl}/api/questions/active/${filter}/${catid}/${searchtxt2}`);
        setQids(Qids.data);
        // /${searchtxt}
    
    
        
    
    
      }
      catch (error){
        console.log(error);
      }
    
    }
  const deleteQuestion=async (i)=>{
      try {
        const response = await axios.delete(`${apiBaseUrl}/api/delete/${i}`);
        if (response.status === 204) {
          console.log("Question deleted successfully");
          // Optionally, update your UI here to reflect the deletion
          setQids(QIds.filter(QIds => QIds !== i));
      }
      }
      catch (error){
        console.log(error);
      }
    }
 
  const handleCategoryChange =(i)=>{
    setcat(i);
    setSearchtext2(null);
    setSearchtext("");
    if(i=='Fun'){
      setcatid(1);
    }
    else if(i=='Tech'){
      setcatid(2);

    }else if(i=='Maths'){
      setcatid(3);

    }
    if(i=='All'){
      setcatid(null);
    }
  }
  const handleAll=()=>{
    setAll(true);
    setPublished(false);
    setunPublished(false);
    setSearchtext2(null);
    setSearchtext("");
    setPrevious(false);

    setFilter('all');

  }
  const handlePublished=()=>{
    setAll(false);
    setPublished(true);
    setunPublished(false);
    setFilter('Published');
    setSearchtext2(null);
    setPrevious(false);

    setSearchtext("");

  }
  const handleunPrevious=()=>{
    setAll(false);
    setPublished(false);
    setunPublished(false);
    setPrevious(true);
    setFilter('previous');
    setSearchtext2(null);
    setSearchtext("");

  }
  const handleunPublished=()=>{
    setAll(false);
    setPublished(false);
    setunPublished(true);
    setFilter('unPublished');
    setSearchtext2(null);
    setSearchtext("");
    setPrevious(false);


  }
  const handleip =(e)=>{
    setSearchtext(e.target.value);
    if((e.target.value)==""){
    setSearchtext2(null);
    Qid();
  setAll(true);


    }else{
    setSearchtext2(e.target.value);
    Qid();
    search();
    }
    
  }
 const search =(e)=>{
 

if(searchtxt !=""){
  // console.log("sss")
  setAll(false);
    setPublished(false);
    setunPublished(false);
    setPrevious(false);

    setFilter('all');
  setcat("Category");
  setcatid(null);
  setToggle(!toggle);
  

}

 }
 const dltsearch =()=>{
  if(searchtxt!=""){
    setSearchtext2(null);
    // Qid();
    setSearchtext('')
  setAll(true);
  }
 }

  return (
    <div className="QuestionBank">
      <div className="topQB">
        <div className="QBbtns">
      <button className={all ? 'allopt' :'opt' } onClick={handleAll}>
    All
      </button>
      <button className={published ? 'publishedopt' :'opt' } onClick={handlePublished}>
        Published
      </button><button className={unPublished ? 'unPublishedopt' :'opt' } onClick={handleunPublished}>
        UnPublished
        </button>
        <button className={previous ? 'previousopt' :'opt' } onClick={handleunPrevious}>
        Previous
        </button>
      </div>
      
      <div className="QBsearch">
        <div className="QBCat">
{/* <p>Category</p> */}
<DropDown heading={cat}
              options={['All','Fun', 'Tech','Maths']}
              onChange={handleCategoryChange}/>

        </div>
        <div className="catinner">
<input type="text" className="searchip"
value={searchtxt}
onChange={handleip}
placeholder="Enter something..."
/>
<button className="searchbtn" onClick={dltsearch}>
 {(searchtxt!="") ?<CloseIcon/>:<SearchIcon/>}
</button>
        </div>
      </div>
      </div>
      <div className="bottom">
        {/* {(
      Options.map(option => (
        <div key={option.AnswerOptionId}>)))} */}
         {/* {(QIds !=  null) && console.log(QIds)} */}
        {(QIds !=  null) && (
            
          QIds.map(i =>(
              <QBblock key={i} id={i}
              onchange={deleteQuestion}
              previous={previous}/>
              
              
          )))
          
        }
      </div>
    </div>
  )
}

export default QuestionBank