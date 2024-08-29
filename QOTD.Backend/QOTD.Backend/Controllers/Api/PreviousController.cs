using Google;
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
    public class PrevousController : ApiController
    {

        private readonly AppdbContext _context;

        public PrevousController()
        {
            _context = new AppdbContext();
        }

        [HttpGet]
        [Route("api/Prevous/{userId}")]
        public IHttpActionResult GetUnansweredQuestions(int userId)
        {
            var answeredQuestionIds = _context.UserResponse
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.QuestionId)
                .ToList();

            var today = DateTime.Today;
            var startOfToday = today; // Start of today, i.e., 00:00:00

            // Select questions with dates up to and including today
            var activeApprovedQuestionIds = _context.Questions
                .Where(q => DbFunctions.TruncateTime(q.QuestionDate) <= startOfToday)
                .Select(q => q.QuestionId)
                .ToList();

            var unansweredQuestionIds = activeApprovedQuestionIds
               .Except(answeredQuestionIds)
               .ToList();

            return Ok(unansweredQuestionIds);
        }
    }
}
