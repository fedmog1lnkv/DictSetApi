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
    public IActionResult Register(User model)
    {
        if (ModelState.IsValid)
        {
            UsersDatabase UDB = new UsersDatabase();
            bool reg = UDB.Register(model.Username, model.Email, model.Password);
            if (!reg) return Conflict("A user with this E-mail exists");
            return Ok();
        }

        return BadRequest();
    }
    
    /*[HttpPost]
    public IActionResult Login(User model)
    {
        var user = _context.Users.SingleOrDefault(u => u.Login == model.Login);

        if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
        {
            return Unauthorized();
        }

        var tokenString = GenerateToken(user);

        return Ok(new { token = tokenString });
    }*/
}