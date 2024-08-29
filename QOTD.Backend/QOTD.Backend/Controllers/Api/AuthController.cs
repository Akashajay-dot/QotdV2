using Google.Apis.Auth;

using System;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Google.Apis.Auth.OAuth2;
using Google;
using QOTD.Web.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Microsoft.Exchange.WebServices.Data;



namespace QOTD.Web.Controllers.Api
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private readonly AppdbContext _context;

        public AuthController(AppdbContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("validate-token")]
        public async Task<IHttpActionResult> ValidateToken([FromBody] TokenDto tokenDto)
        {

            if (string.IsNullOrWhiteSpace(tokenDto.Credential))
            {
                
                return BadRequest(new { message = "Credential must not be empty" });
            }   
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(tokenDto.Credential);
                var user = await _context.Users.FirstOrDefaultAsync(u => u.GoogleId == payload.Subject);

                if (user == null)
                {
                    user = new User
                    {
                        GoogleId = payload.Subject,
                        Name = payload.Name,
                        Pic = payload.Picture,
                        CreatedON = DateTime.UtcNow,
                        UpdatedON = DateTime.UtcNow,
                        

                    };
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    user.UpdatedON = DateTime.UtcNow;
                    await _context.SaveChangesAsync();
                   
                   
                }
                var userRank = _context.Users
                   .Where(u => u.Points > _context.Users
                   .Where(us => us.UserId == user.UserId).Select(us => us.Points).FirstOrDefault())
                   .Count() + 1;
                var totalUsers = _context.Users.Count();

                return base.Ok(new
                {
                   Isvalid = true,
                   Payload = payload,
                   UserId = user.UserId,
                   isAdmin = user.IsAdmin,
                   category = _context.Categories,
                    userRank= userRank,
                    totalUsers= totalUsers,

                });
            }
            catch (InvalidJwtException e)
            {
                var response = Request.CreateResponse(HttpStatusCode.Unauthorized, new
                {
                   
                    message = "Invalid token",
                    error = e.Message
                });

                return ResponseMessage(response);
            }
            catch (DbUpdateException ex)
            {
                var detailedErrorMessage = ex.InnerException?.InnerException?.Message ?? ex.Message;

                var response = Request.CreateResponse(HttpStatusCode.InternalServerError, new
                {
                    message = "An unexpected error occurred while updating the database.",
                    error = detailedErrorMessage
                });

                return ResponseMessage(response);
            }
            catch (Exception ex)
            {
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError, new
                {
                    message = "An unexpected error occurred.",
                    error = ex.Message  ,
                    err= tokenDto.Credential
                });

                return ResponseMessage(response);
            }
        }

        private IHttpActionResult BadRequest(object value)
        {
            throw new NotImplementedException();
        }
    }
    public class TokenDto
    {
        public string Credential { get; set; }
    }

}
