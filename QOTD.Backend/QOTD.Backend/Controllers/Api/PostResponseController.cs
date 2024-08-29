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
    public class PostResponseController : ApiController
    {
        private AppdbContext db = new AppdbContext();

        [HttpPost]
        [Route("api/PostResponse")]
        public async Task<IHttpActionResult> PostResponseAsync(ResponseDto responseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await db.Users.FirstOrDefaultAsync(u => u.UserId == responseDto.UserId);
            if (user == null)
            {
                return BadRequest("User not found");
            }
            if (responseDto.isCorrect)
            {
                user.Points += responseDto.Points;
            }
          
           foreach(var i in responseDto.AnswerOptionId)
            {
                var userResponse = new UserResponse
                {
                    QuestionId = responseDto.QuestionId,
                    UserId = responseDto.UserId,
                    AnswerOptionId = i,
                    IsCorrect = responseDto.isCorrect,
                };
                db.UserResponse.Add(userResponse);

            }
           





            db.SaveChanges();
            return Ok();
        }
    }
    public class ResponseDto
    {
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public int[] AnswerOptionId { get; set; }
        public bool isCorrect { get; set; }
        public int Points { get; set; }




    }
}
