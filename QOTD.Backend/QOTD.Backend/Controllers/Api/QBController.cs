using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QOTD.Web.Models;
using static Mysqlx.Datatypes.Scalar.Types;

namespace QOTD.Web.Controllers.Api
{
    public class QBController : ApiController
    {
        private readonly AppdbContext context;

        public QBController()
        {
            context = new AppdbContext();
        }

        // GET: api/questions/active
        [HttpGet]
        [Route("api/questions/active/{filter}/{catid}/{searchtxt}")]
        public IHttpActionResult GetActiveQuestionIds(string filter,string catid, string searchtxt)
        {
            int? catId = null;
            var today = DateTime.Today;
            var startOfToday = today;

            if (searchtxt != "null")
            {
                var activeQuestionIds = context.Questions
                                   .Where(q => q.Question.Contains(searchtxt)  ) // Assuming there's an IsActive field in the Question table
                                   .Select(q => q.QuestionId)
                                   .ToList();

                return Ok(activeQuestionIds);
            }
            else
            {
                if (!string.IsNullOrEmpty(catid) && catid != "null")
                {
                    catId = Convert.ToInt32(catid);
                }

                if (filter == "all")
                {
                    if (!catId.HasValue)
                    {
                        var activeQuestionIds = context.Questions
                                       //.Where(q => q.IsActive) // Assuming there's an IsActive field in the Question table
                                       .Select(q => q.QuestionId)
                                       .ToList();

                        return Ok(activeQuestionIds);
                    }
                    else
                    {
                        var activeQuestionIds = context.Questions
                                                           .Where(q => q.CategoryId == catId) // Assuming there's an IsActive field in the Question table
                                                           .Select(q => q.QuestionId)
                                                           .ToList();

                        return Ok(activeQuestionIds);
                    }
                }
                else if (filter == "Published")
                {
                    if (!catId.HasValue)
                    {
                        var activeQuestionIds = context.Questions
                                       .Where(q => q.IsApproved &&  ((DbFunctions.TruncateTime(q.QuestionDate)) > startOfToday)) // Assuming there's an IsActive field in the Question table
                                       .Select(q => q.QuestionId)
                                       .ToList();

                        return Ok(activeQuestionIds);
                    }
                    else
                    {
                        var activeQuestionIds = context.Questions
                                                           .Where(q =>  q.CategoryId == catId && q.IsApproved && ((DbFunctions.TruncateTime(q.QuestionDate)) > startOfToday)) // Assuming there's an IsActive field in the Question table
                                                           .Select(q => q.QuestionId)
                                                           .ToList();

                        return Ok(activeQuestionIds);
                    }

                }
                else if (filter == "unPublished")
                {
                    if (!catId.HasValue)
                    {
                        var activeQuestionIds = context.Questions
                                       .Where(q =>  !(q.IsApproved)) // Assuming there's an IsActive field in the Question table
                                       .Select(q => q.QuestionId)
                                       .ToList();

                        return Ok(activeQuestionIds);
                    }
                    else
                    {
                        var activeQuestionIds = context.Questions
                                                           .Where(q =>  q.CategoryId == catId && !(q.IsApproved)) // Assuming there's an IsActive field in the Question table
                                                           .Select(q => q.QuestionId)
                                                           .ToList();

                        return Ok(activeQuestionIds);
                    }

                }
                else if (filter == "previous")
                {
                    if (!catId.HasValue)
                    {
                        var activeQuestionIds = context.Questions
                                                           //  .Where(q => !q.IsActive) // Assuming there's an IsActive field in the Question table
                                        .Where(q =>((DbFunctions.TruncateTime(q.QuestionDate)) < startOfToday) && (q.IsApproved)) // Assuming there's an IsActive field in the Question table

                                       .Select(q => q.QuestionId)
                                       .ToList();

                        return Ok(activeQuestionIds);
                    }
                    else
                    {
                        var activeQuestionIds = context.Questions
                                                           .Where(q => ( q.CategoryId == catId ) && ((DbFunctions.TruncateTime(q.QuestionDate)) < startOfToday) && (q.IsApproved)) // Assuming there's an IsActive field in the Question table
                                                           .Select(q => q.QuestionId)
                                                           .ToList();

                        return Ok(activeQuestionIds);
                    }

                }
                return Ok("ok");
            }
        }

    }
}
