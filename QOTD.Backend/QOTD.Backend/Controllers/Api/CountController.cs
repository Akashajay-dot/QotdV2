using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QOTD.Web.Models;

namespace QOTD.Web.Controllers.Api
{
    public class CountController : ApiController
    {

        private AppdbContext db = new AppdbContext();

        [HttpGet]
        [Route("api/Count/{userId}")]
        public IHttpActionResult GetQuestioncount(int userId)
        {
            var answeredQuestionIds = db.UserResponse
               .Where(ur => ur.UserId == userId)
               .Select(ur => ur.QuestionId)
               .ToList();

            // var unansweredQuestions = _context.Questions
            //   .Where(q => !answeredQuestionIds.Contains(q.QuestionId) && q.IsActive == true && q.IsApproved == true)
            //   .ToList();
            var today = DateTime.Today;
            var startOfToday = today; // Start of today, i.e., 00:00:00

            // Select questions with dates up to and including today
            var ApprovedQuestionIds = db.Questions
                .Where(q => DbFunctions.TruncateTime(q.QuestionDate) <= startOfToday)
                .Select(q => q.QuestionId)
                .ToList();
            

            return base.Ok(new
            {
                ApprovedQuestionIds,
                answeredQuestionIds
            });
        }

    }
}
