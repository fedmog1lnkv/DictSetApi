using Api.Classes;
using Api.Models;
using Database;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Set = Database.Set;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetController : ControllerBase
    {
        // GET: api/Set
        // Getting all the user's sets by his token
        [HttpGet]
        public IActionResult GetAllSets()
        {
            var headers = Request.Headers;
            headers.TryGetValue("Token", out var token);
            TokenClass tokenCL = new TokenClass();
            if (!tokenCL.ValidateToken(token))
            {
                return Unauthorized();
            }

            TokenDatabase TDB = new TokenDatabase();
            string email = TDB.GetEmailByToken(token);
            int userId = UsersDatabase.GetUserIdByEmail(email);

            string resultingSet = SetDatabase.GetAllSets(userId);
            if (resultingSet == "[]")
            {
                return BadRequest("You don't have any sets");
            }

            return Ok(resultingSet);
        }

        // GET: api/Set/5
        [HttpGet("{setId}", Name = "GetSetById")]
        public IActionResult GetSet(int setId)
        {
            var headers = Request.Headers;
            headers.TryGetValue("Token", out var token);
            TokenClass tokenCL = new TokenClass();
            if (!tokenCL.ValidateToken(token))
            {
                return Unauthorized();
            }

            TokenDatabase TDB = new TokenDatabase();
            string email = TDB.GetEmailByToken(token);
            int userId = UsersDatabase.GetUserIdByEmail(email);

            Set resultingSet = SetDatabase.GetSet(userId, setId);
            if (resultingSet.Name == null)
            {
                return BadRequest("You don't have set with this id");
            }

            return Ok(resultingSet);
        }

        // POST: api/Set
        [HttpPost]
        public IActionResult CreateSet(Set model)
        {
            var headers = Request.Headers;
            headers.TryGetValue("Token", out var token);
            TokenClass tokenCL = new TokenClass();
            if (!tokenCL.ValidateToken(token))
            {
                return Unauthorized();
            }

            TokenDatabase TDB = new TokenDatabase();
            string email = TDB.GetEmailByToken(token);
            model.UserId = UsersDatabase.GetUserIdByEmail(email);

            SetDatabase SDB = new SetDatabase();
            if (SDB.AddSet(model.UserId, model.Name, model.Description))
            {
                return Ok();
            }

            // If a set with the same name has already been created for such a user
            return BadRequest();
        }
    }
}