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
        public IActionResult Get(User model)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine($"Авторизация {model.Email}, {model.Password}");
                UsersDatabase UDB = new UsersDatabase();
                if (!UDB.Authorization(model.Email, model.Password)) return Unauthorized();

                // deleting a token with a new login 
                TokenDatabase TDB = new TokenDatabase();
                TDB.DeleteToken(model.Email);
                
                // generate new token
                TokenClass tokenCL = new TokenClass();
                // data for generate token
                string token = tokenCL.GenerateToken();
                while (!TDB.AddToken(model.Email, token))
                {
                    token = tokenCL.GenerateToken();
                }

                string responseJSON = "{\n" + $"""
                "message": "User authorized", 
                "token": "{token}"
               """ + "\n}";
                return Ok(responseJSON);
            }

            return Unauthorized();
        }
    }
}