
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using QOTD.Web.Models;

namespace QOTD.Web.Controllers.Api
{

   
    public class DeleteController : ApiController
    {
        private readonly AppdbContext context;

        public DeleteController()
        {
            context = new AppdbContext();
        }
        [HttpDelete]
        [Route("api/delete/{id}")]
        public IHttpActionResult DeleteQuestion(int id)
        {
            // Find the question by its ID
            var question = context.Questions.SingleOrDefault(q => q.QuestionId == id);
            if (question == null)
            {
                return NotFound(); // Return 404 if the question doesn't exist
            }
            var userResponses = context.UserResponse.Where(ur => ur.QuestionId == id).ToList();
            context.UserResponse.RemoveRange(userResponses);

            // Remove the question from the database
            context.Questions.Remove(question);
            context.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent); // Return 204 No Content after successful deletion
        }
    }
}
