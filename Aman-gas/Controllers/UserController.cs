using BL.DTOS;
using BL.Helpers;
using BL.IServices;
using Data.Entities;
using Data.Views;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace Aman_gas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class UserController : ControllerBase
    {
        private readonly IUserService US;
        private readonly IHttpContextAccessor httpContextAccessor;
        
        public UserController(IUserService US, IHttpContextAccessor httpContextAccessor)
        {
            this.US = US;
            this.httpContextAccessor = httpContextAccessor;
           
        }
        [HttpGet("GetUserData")]
        public async Task<ActionResult <Response<UserDataDTO>>> GetUserDataAsync()
        {
            string UserName = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return Ok(await US.GetUserDataAsync(UserName));
        }
    }
}
