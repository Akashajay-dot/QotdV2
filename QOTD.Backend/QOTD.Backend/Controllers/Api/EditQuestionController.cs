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
    public class EditQuestionController : ApiController
    {

        private AppdbContext db = new AppdbContext();

        [HttpPut]
        [Route("api/EditQuestion/{QId}")]
        public IHttpActionResult EditQuestion(int QId, QuestionDto2 questionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           var optionsToRemove = db.AnswerOptions.Where(ao => ao.QuestionId == QId);
            db.AnswerOptions.RemoveRange(optionsToRemove);
            db.SaveChanges();



            var questionInDb = db.Questions.SingleOrDefault(q => q.QuestionId == QId);
            if (questionInDb == null)
            {
                return NotFound();
            }

            questionInDb.Question = questionDto.Question;
            questionInDb.CategoryId = questionDto.CategoryId;
            questionInDb.AuthorId = questionDto.AuthorId;

            questionInDb.Point = questionDto.Point;
            questionInDb.HasMultipleAnswers = questionDto.HasMultipleAnswers;
            questionInDb.UpdatedBy = questionDto.UpdatedBy;
            questionInDb.LastUpdatedOn = questionDto.LastUpdatedOn;
            questionInDb.LastUpdatedBy = questionDto.LastUpdatedBy;
            questionInDb.IsActive = true;
            db.SaveChanges();

            foreach (var option in questionDto.Answers)
            {
                var answerOption = new AnswerOption
                {
                    QuestionId = QId,
                    Option = option.Text
                };

                db.AnswerOptions.Add(answerOption);
                db.SaveChanges();

                if (option.IsCorrect)
                {
                    var answerKey = new AnswerKey
                    {
                        QuestionId = QId,
                        AnswerOptionId = answerOption.AnswerOptionId
                    };

                    db.AnswerKeys.Add(answerKey);
                    db.SaveChanges();
                }
            }

            return Ok("ok");
        }
    }
    public class QuestionDto2
    {
        public string Question { get; set; }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }

        public int Point { get; set; }
        public bool HasMultipleAnswers { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public List<AnswerDto2> Answers { get; set; }
    }

    public class AnswerDto2
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}
