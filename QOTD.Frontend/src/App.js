import React, { useContext, useEffect } from 'react';
import './App.css';
import { BrowserRouter as Router,Routes,Route, Navigate} from 'react-router-dom';
import Header from './Components/Header';
import LoginPage from './Components/LoginPage';
import LandingPage from './Components/LandingPage';
import PostQuestions from './Components/PostQuestions';
import { toast, ToastContainer } from 'react-toastify';
import { GlobalStateContext } from './Context/GlobalStateContext';
import axios from 'axios';
import Loading from './Components/Loading';
import PreviousQuestion from './Components/PreviousQuestion';
import Approve from './Components/Approve';
import NotFoundComponent from './Components/NotFoundComponent';
import QuestionBank from './Components/QuestionBank';
import Prev from './Components/Prev';
import DesktopOnly from './Components/DesktopOnly';
import LeaderBoard from './Components/LeaderBoard';
import backgroundImage from './Assets/background.jpg';
import anotherBackgroundImage from './Assets/new1.jpg';
function App() {

  const { state, setLogedin ,setId,setCategory,setUserName, setLoading,setAdmin,setPic,setTotaluser,setRank,setEmail} = useContext(GlobalStateContext);
  const apiBaseUrl = process.env.REACT_APP_API_BASE_URL;
 
  useEffect(() => {
    const credential = localStorage.getItem('token');
    const notify = () => toast.error("Error login");

    const validateToken = async () => {
      if (credential ) {
        try {
          const res = await axios.post(`${apiBaseUrl}/api/auth/validate-token`, {
            credential: credential
          });

          if (res.data.Isvalid) {
            setId(res.data.UserId);
            setUserName(res.data.Payload.name);

            setLogedin(true);
            setPic(res.data.Payload.picture);
            setTotaluser(res.data.totalUsers)
            // setPic(res.data.Payload.picture);
            setEmail(res.data.Payload.email)
                          setRank(res.data.userRank)

            setCategory(res.data.category);
            setLoading(true);
            if(res.data.isAdmin){
              setAdmin(true);
              // console.log("i am admin")
             }

          } else {
           
          }
        } catch (error) {
          console.error('An error occurred:', error);
          
        }
      }
      else{
        setLogedin(false);
        

      }
    };

    validateToken();
  }, []);
  const backgroundStyle = {
    backgroundImage:`url(${state.isLogedin ? backgroundImage : anotherBackgroundImage})`,
  };
  
  return (
    <div className="App" style={backgroundStyle}>
       <DesktopOnly>
              <Router >
                {
                  state.isLogedin && <Header/>
                }
        {state.loading &&<Loading/> }
        <Routes>
          
          <Route path='/' element={state.isLogedin ? <LandingPage />: <LoginPage/> }/> 
        
       <Route path='/postQuestions' element={state.isLogedin ? <PostQuestions />: <LoginPage/>} />
          <Route path='/leaderBoard' element={state.isLogedin ?<LeaderBoard />: <LoginPage/>} />
          <Route path='/prevQuestions' element={state.isLogedin ?<Prev />: <LoginPage/>} />
          <Route path='/prevQuestionspage/:message' element={state.isLogedin ?<PreviousQuestion />: <LoginPage/>} />

           <Route path='/approveQuestions' element={state.isLogedin && state.isAdmin ? <Approve />: <LoginPage/>} />
           <Route path='/questions' element={state.isLogedin && state.isAdmin ? <QuestionBank />: <LoginPage/>} />

          <Route path="*" element={<NotFoundComponent />} />
         
        </Routes>
     </Router>
     </DesktopOnly>
     <ToastContainer />
   
    </div>
  );
}

export default App;
