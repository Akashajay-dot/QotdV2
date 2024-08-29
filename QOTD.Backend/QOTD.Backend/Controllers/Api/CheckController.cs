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
    public class CheckController : ApiController
    {
        private readonly AppdbContext _context;

        public CheckController()
        {
            _context = new AppdbContext();
        }

        // GET: api/Questions/IsCorrect/{qid}
        [HttpGet]
        [Route("api/Questions/IsCorrect/{qid}")]
        public async Task<IHttpActionResult> GetIsCorrect(int qid)
        {
            // Fetch the IsCorrect value based on the QuestionId (qid)
            var isCorrect = await _context.UserResponse
                .Where(ur => ur.QuestionId == qid)
                .Select(ur => ur.IsCorrect)
                .FirstOrDefaultAsync();

           

            // Return the IsCorrect value
            return Ok(new { IsCorrect = isCorrect });
        }
    }
}
