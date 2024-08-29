using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QOTD.Web.Models;

namespace QOTD.Web.Controllers.Api
{
    public class UserController : ApiController
    {
        private readonly AppdbContext _context;

        public UserController()
        {
            _context = new AppdbContext();
        }

        // GET: api/users/orderedbyPoints
        [HttpGet]
        [Route("api/users/orderedbyPoints")]
        public IHttpActionResult GetUsersOrderedByPoints()
        {
            var userIds = _context.Users
                .OrderByDescending(u => u.Points) // Order users by points in ascending order
                .Select(u => u.UserId)  // Select only the user IDs
                .ToList();

            return Ok(userIds); // Return the list of user IDs
        }
    }
}
