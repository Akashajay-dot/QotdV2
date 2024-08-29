using Google;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http; 
using QOTD.Web.Models;

namespace QOTD.Web.Controllers.Api
{
    [RoutePrefix("api/question")]
    public class QuestionController : ApiController
    {
        private AppdbContext db = new AppdbContext();

        [HttpGet]
        [Route("get-daily-question/{userId}")]
        public IHttpActionResult GetDailyQuestion(int userId)
        {
            var today = DateTime.Today;
            var yesterday = today.AddDays(-1);

           MarkPreviousDayQuestionAsInactive(yesterday);
            
            var dailyQuestion = db.Questions
                .Where(q => DbFunctions.TruncateTime(q.QuestionDate) == today && q.IsActive && q.IsApproved)
                .Select(q => new
                {   
                    Question = q,
                    AnswerOptions = db.AnswerOptions.Where(a => a.QuestionId == q.QuestionId).ToList(),
                    AnswerKeys = db.AnswerKeys.Where(k => k.QuestionId == q.QuestionId).ToList(),
                   


                })
                .FirstOrDefault();

            if (dailyQuestion == null)
            {
                return NotFound(); 
            }
            var userResponse = db.UserResponse
                .FirstOrDefault(ur => ur.UserId == userId && ur.QuestionId == dailyQuestion.Question.QuestionId);
            if (userResponse != null)
            {
                return NotFound();

            }
            else
            {
            return Ok(dailyQuestion);

            }

        }

        private void MarkPreviousDayQuestionAsInactive(DateTime date)
        {
            var previousDayQuestion = db.Questions
                .Where(q => DbFunctions.TruncateTime(q.QuestionDate) == date && q.IsActive)
                .FirstOrDefault();

            if (previousDayQuestion != null)
            {
                previousDayQuestion.IsActive = false;
                db.Entry(previousDayQuestion).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
