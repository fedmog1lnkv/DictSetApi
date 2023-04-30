using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Classes;
using Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    // Класс проверяет можно ли пользователю сидеть на странице
    [Route("api/[controller]")]
    [ApiController]
    public class ValidateTokenController : ControllerBase
    {
        // GET: api/ValidateToken/{token}
        [HttpGet("{token}")]
        public IActionResult Get(string token)
        {
            TokenClass tokenCL = new TokenClass();
            if (tokenCL.ValidateToken(token)) return Accepted(new Response(true, null, null));
            else return Unauthorized(new Response(false, null, "The user is not logged in"));
        }
    }
}