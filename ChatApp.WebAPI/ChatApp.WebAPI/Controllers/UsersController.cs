using ChatApp.Managers.Extensions;
using ChatApp.Managers.Interfaces;
using ChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersManager usersManager;

        public UsersController(IUsersManager usersManager)
        {
            this.usersManager = usersManager;
        }

        [AllowAnonymous]
        [HttpPost("Insert")]
        public IActionResult InserUser([FromBody] UserModel userModel)
        {
            try
            {
                var response = usersManager.InsertUser(userModel);
                if (userModel.ID > 0)
                {
                    var user = usersManager.GetUserById(userModel.ID);
                }
                return Ok(response);
            }
            catch (Exception exp)
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            try
            {
                var userModel = usersManager.Login(loginModel, HttpContext);

                if (userModel == null || !string.IsNullOrEmpty(userModel.ValidationToken))
                {
                    return BadRequest(new { message = "Login or password is incorrect" });
                }
                return Ok(userModel.WithoutPassword());
            }
            catch (Exception exp)
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public IActionResult RefreshToken([FromBody] string refreshToken)
        {
            try
            {
                var userModel = usersManager.RefreshToken(refreshToken, HttpContext);
                if (userModel == null)
                {
                    // TODO Log Error Refresh Token not found.
                    return Unauthorized();
                }
                return Ok(userModel);
            }
            catch (Exception exp)
            {
                return BadRequest();
            }
        }

        [HttpGet("getMyFriends/{userId}")]
        public IActionResult GetMyFriends(long userId)
        {
            try
            {
                return Ok(usersManager.GetMyFriends(userId));
            }
            catch (Exception exp)
            {
                return BadRequest();
            }
        }
    }
}
