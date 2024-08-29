using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QOTD.Web.Models;

namespace QOTD.Web.Controllers.Api
{
    public class FetchQuestionController : ApiController
    {
        private readonly AppdbContext _context;

        public FetchQuestionController()
        {
            _context = new AppdbContext();
        }

        [HttpGet]
        [Route("api/FetchQuestion/{Qid}")]
        public IHttpActionResult GetUnansweredQuestions(int Qid)
        {
            var question = _context.Questions
                .Where(q => q.QuestionId == Qid)
                .Select(q => new
                {
                    Question = q,
                    AnswerOptions = _context.AnswerOptions.Where(a => a.QuestionId == q.QuestionId).ToList(),
                    AnswerKeys = _context.AnswerKeys.Where(k => k.QuestionId == q.QuestionId).ToList(),

                })
                .ToList();
            if (question == null)
            {
                return NotFound();
            }



            return Ok(question);
        }
    }
}
