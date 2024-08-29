using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QOTD.Web.Models;

namespace QOTD.Web.Controllers.Api
{
    public class UserDetailsController : ApiController
    {
        private readonly AppdbContext _context;
        public UserDetailsController()
        {
            _context = new AppdbContext();
        }

        // GET api/user/{googleId}
        [HttpGet]
        [Route("api/user/{Id}")]
        public IHttpActionResult GetUserByGoogleId(int Id)
        {
            // Fetch the user from the database using the Google User ID
            var user = _context.Users.FirstOrDefault(u => u.UserId == Id);

            if (user == null)
            {
                return NotFound(); // Returns a 404 if the user is not found
            }
            var reputationName = _context.ReputationMaster
              .Where(r => user.Points >= r.MinPoints && user.Points <= r.UptoPoints)
              .Select(r => r.Badge)
              .FirstOrDefault();

            return Ok(new
            {
                User = user, // Include all user fields
                ReputationName = reputationName // Include reputation name in the response
            });
        }
    }
}
