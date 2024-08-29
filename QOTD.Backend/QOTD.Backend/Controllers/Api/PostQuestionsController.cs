using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QOTD.Web.Models;

namespace QOTD.Web.Controllers.Api
{
    public class PostQuestionsController : ApiController
    {
        private AppdbContext db = new AppdbContext();

        [HttpPost]
        [Route("api/PostQuestions")]
        public IHttpActionResult PostQuestions(QuestionDto questionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = db.Categories.SingleOrDefault(c => c.CategoryId == questionDto.CategoryId);
            if (category == null)
            {
                return BadRequest("Invalid CategoryId");
            }
            var author = db.Users.SingleOrDefault(u => u.UserId == questionDto.AuthorId);
            if (author == null)
            {
                return BadRequest("Invalid AuthorId");
            }   

            var question = new Questions
            {
                Question = questionDto.Question,
                CategoryId = questionDto.CategoryId,
                AuthorId = questionDto.AuthorId,
               
                Point = questionDto.Point,
                HasMultipleAnswers = questionDto.HasMultipleAnswers,
                CreatedBy = questionDto.CreatedBy,
                UpdatedBy = questionDto.UpdatedBy,
                IsApproved = questionDto.IsApproved,
                SnapShot = questionDto.SnapShot,
                LastUpdatedOn = questionDto.LastUpdatedOn,
                LastUpdatedBy = questionDto.LastUpdatedBy,
                IsActive= true,
            };

            db.Questions.Add(question);
            db.SaveChanges();

            foreach (var option in questionDto.Answers)
            {
                var answerOption = new AnswerOption
                {
                    QuestionId = question.QuestionId,
                    Option = option.Text
                };

                db.AnswerOptions.Add(answerOption);
                db.SaveChanges();

                if (option.IsCorrect)
                {
                    var answerKey = new AnswerKey
                    {
                        QuestionId = question.QuestionId,
                        AnswerOptionId = answerOption.AnswerOptionId
                    };

                    db.AnswerKeys.Add(answerKey);
                    db.SaveChanges();
                }
            }

            return Ok(question);
        }

    }
    public class QuestionDto
    {
        public string Question { get; set; }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
       
        public int Point { get; set; }
        public bool HasMultipleAnswers { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsApproved { get; set; }
        public string SnapShot { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public List<AnswerDto2> Answers { get; set; }
    }

    public class AnswerDto
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}
