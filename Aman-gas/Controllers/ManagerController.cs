using BL.Helpers;
using BL.UOW;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Aman_gas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IUnitOfWork UOF;
        private readonly UserManager<AppUser> UserMNG;
        public ManagerController(IUnitOfWork _unit , UserManager<AppUser> _UserMNG)
        {
            this.UserMNG = _UserMNG;
            this.UOF = _unit;
        }

        [HttpGet("AcceptSalesRequests")]
        public async Task<ActionResult<Response<string>>> AcceptSalesRequests(int RequestId)
        {
            try
            {
                SalesRequest request = await UOF.SalesRequests.FindAsync(s => s.Id == RequestId);
                if (request is null)
                    return Ok(new Response<string>() { State = 2, Data = null, Message = "No Request Found with This ID " });
                if (request.Status == 1)
                    return Ok(new Response<string>() { State = 3, Data = null, Message = "This Request Have Been Approved from While " });
                var CurrentUserName = HttpContext.User.Identity.Name;
                var CurrentUser = UserMNG.Users.FirstOrDefault(s => s.UserName == CurrentUserName);
                SalesMan Sale = new SalesMan() { 
                Name= request.Name , 
               // DateOfBirth = DateTime.Now,
                JoinDate = DateTime.Now,
                Password = request.Password ,
                StationId = request.StatioId , 
                Status = 1 
                };
                return Ok(new Response<string>() { });

            }
            catch (Exception ex)
            {
                return Ok(new Response<string>() { State = 500, Data = null, Message = ex.Message });

            }

        }
    }
}
