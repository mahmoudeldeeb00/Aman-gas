using BL.DTOS;
using BL.Helpers;
using BL.IServices;
using BL.UOW;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Aman_gas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="User")]
    public class FuelingController : ControllerBase
    {
        private readonly IFuelingService FS;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<AppUser> USM;
        public FuelingController(IFuelingService FS , IHttpContextAccessor httpContextAccessor , UserManager<AppUser> USM)
        {
            this.FS = FS;
            this.httpContextAccessor = httpContextAccessor;
            this.USM = USM;
        }

        [HttpPost("AddNewFueling")]
        public async Task<ActionResult<Response<string>>> AddNewFueling([FromBody] FuelingDTO Model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            string UserName = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string UserId = USM.FindByNameAsync(UserName).Result.Id;
            return Ok(await FS.AddFueling(Model, UserId));
        }
    }
}
