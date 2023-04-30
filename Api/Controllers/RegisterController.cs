using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Classes;
using Api.Models;
using Database;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;


namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegisterController : ControllerBase
{
    [HttpPost]
    public IActionResult Register(UserDto model)
    {
        if (ModelState.IsValid)
        {
            UsersDatabase UDB = new UsersDatabase();
            bool reg = UDB.Register(model.Username, model.Email, model.Password);
            if (!reg) return Conflict(new Response(false, null, "A user with this E-mail exists"));
            return Ok(new Response(true, new { message = "The user is registered" }, null));
        }

        return BadRequest(new Response(false, null, "Bad JSON"));
    }
}