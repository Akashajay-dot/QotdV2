using Google;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QOTD.Web.Models;

namespace QOTD.Web.Controllers.Api
{
    public class DatesetController : ApiController
    {
        private readonly AppdbContext _context;

        public DatesetController()
        {
            _context = new AppdbContext();
        }
        [HttpGet]
        [Route("api/questions/dates")]
        public IHttpActionResult GetQuestionDates()
        {
            try
            {
                var questionDates = _context.Questions
                    .Select(q => q.QuestionDate)
                    .Distinct()
                    .OrderBy(date => date)
                    .ToList();

                return Ok(questionDates);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
