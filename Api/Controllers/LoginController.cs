using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Classes;
using Api.Models;
using Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        // POST: api/Login
        [HttpPost]
        public IActionResult Get(UserDto model)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine($"Авторизация {model.Email}, {model.Password}");
                UsersDatabase UDB = new UsersDatabase();
                if (!UDB.Authorization(model.Email, model.Password))
                    return Unauthorized(new Response(false, null, "The user is not logged in"));
                
                // deleting a token with a new login 
                TokenDatabase TDB = new TokenDatabase();
                string userToken = TDB.GetTokenByEmail(model.Email);
                if (userToken != "")
                {
                    var resp = new
                    {
                        message = "User authorized",
                        token = userToken
                    };
                    return Ok(new Response(true, resp, null));
                }
                else
                {
                    TDB.DeleteToken(model.Email);

                    // generate new token
                    TokenClass tokenCL = new TokenClass();
                    // data for generate token
                    string token = tokenCL.GenerateToken();
                    while (!TDB.AddToken(model.Email, token))
                    {
                        token = tokenCL.GenerateToken();
                    }

                    var resp = new
                    {
                        message = "User authorized",
                        token = token
                    };
                    return Ok(new Response(true, resp, null));
                }
            }

            return Unauthorized(new Response(false, null, "The user is not logged in"));
        }
    }
}