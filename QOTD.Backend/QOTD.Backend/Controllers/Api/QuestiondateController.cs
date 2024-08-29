using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QOTD.Web.Models;

namespace QOTD.Web.Controllers.Api
{
    public class QuestiondateController : ApiController
    {
        private readonly AppdbContext context;

        public QuestiondateController()
        {
            context = new AppdbContext();
        }
        [HttpPut]
        [Route("api/questions/update")]
        public IHttpActionResult UpdateQuestionDate([FromBody] QuestionUpdateRequest request)
        {
            var question = context.Questions.SingleOrDefault(q => q.QuestionId == request.QuestionId);
            if (question == null)
            {
                return NotFound(); // Return 404 if the question is not found
            }

            // Update the date
            if (request.Date == null)
            {
                question.QuestionDate = null;
                question.IsApproved = false;

            }
            else
            {
                question.QuestionDate = request.Date;
                question.IsApproved = true;
            }
            context.SaveChanges(); // Save changes to the database

            return Ok(question); // Return the updated question
        }

    }
    public class QuestionUpdateRequest
    {
        public int QuestionId { get; set; }
        public DateTime? Date { get; set; }
    }
}
