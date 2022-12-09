using Microsoft.AspNetCore.Mvc;
using ShoeStore.Data;
using ShoeStore.dto.User.Request;
using ShoeStore.Models;

namespace ShoeStore.Controllers;
[ApiController]
[Route("BackUser")]
public class UserController : Controller
{
    private readonly ApplicationDbContext _db;
    public UserController(ApplicationDbContext db)
    {
        _db = db;
    }
    [HttpPost("Registration")]
    public async Task<IActionResult> RegistrationUser([FromBody] RegistrationUserRequest info)
    {
        string message = "";
        try
        {
            var zxc = new User
            {
                Name = info.Name,
                SecondName = info.SecondName,
                Phone = info.Phone,
                Email = info.Email,
                Password = info.Password
            };
            await _db.Users.AddAsync(zxc);
            await _db.SaveChangesAsync();
            message = "good";
            return new JsonResult(message);
        }
        catch (Exception ex)
        {
            message = "Bad";
            return new JsonResult(message);
        }
    }
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetUsers()
    {
        try
        {
           var result =  _db.Users.ToList();
           return new JsonResult(result);
        }
        catch (Exception ex)
        {
            string message = "Bed";
            return new JsonResult(message);
        }
    }
}