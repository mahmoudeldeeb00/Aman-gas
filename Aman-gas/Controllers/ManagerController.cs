using AutoMapper;
using BL.DTOS;
using BL.Helpers;
using BL.UOW;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Aman_gas.Controllers
{
    // Controller Description 
    // 1 // AcceptSalesRequests // Accept and Deny Sales Man Requests 
    // 2 // PendingSalesRequests // Get Pendig Sales Request 


    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class ManagerController : ControllerBase
    {
        private readonly IUnitOfWork UOF;
        private readonly UserManager<AppUser> UserMNG;
        private readonly IMapper Mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        public ManagerController(IUnitOfWork _unit , UserManager<AppUser> _UserMNG, IMapper Mapper,IHttpContextAccessor httpContextAccessor)
        {
            this.UserMNG = _UserMNG;
            this.UOF = _unit;
            this.Mapper = Mapper;
            this.httpContextAccessor = httpContextAccessor; 
        }

        [HttpGet("AcceptSalesRequests")]
        public async Task<ActionResult<Response<string>>> AcceptSalesRequests(int RequestId , int Deny = 0 )
        {
            try
            {
                SalesRequest request = await UOF.SalesRequests.FindAsync(s => s.Id == RequestId);
                if (request is null)
                    return Ok(new Response<string>() { State = 2, Data = null, Message = "No Request Found with This ID " });
                if (request.Status == 1)
                    return Ok(new Response<string>() { State = 3, Data = null, Message = "This Request Have Been Approved from While " });
                if (request.Status == 2)
                    return Ok(new Response<string>() { State = 3, Data = null, Message = "Your Request Have Been Denied !! " });
                string UserName = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                
                if (Deny == 0) /// Aprove 
                {

                    SalesMan Sale = new SalesMan() { 
                    Name= request.Name , 
                    JoinDate = DateTime.Now,
                    DateOfBirth = request.DateOfBirth,
                    Password = request.Password ,
                    StationId = request.StationId , 
                    Status = 1 
                    };

                    await UOF.SalesMen.AddAsync(Sale);
                    request.Status = 1;
                    request.MangerApproved = UserName;
                    UOF.SalesRequests.Update(request);

                    UOF.Complete();
                    return Ok(new Response<string>() {State = 1 , Message = "Sales Request Approved Succefully !" });
                }
                /// Denying 
                    request.Status = 2;// DENIED
                    UOF.SalesRequests.Update(request);
                    UOF.Complete();
                    return Ok(new Response<string>() {State = 1 , Message = "Sales Request Denied Succefully !" });
            }
            catch (Exception ex)
            {
                if(ex.Message.Contains("An error occurred while saving the entity changes"))
                {
                return Ok(new Response<string>() { State = 10, Data = null, ErrorMessage = "Error Loss Of Data Entered NUll Or Foreign Keys" });
                }
                return Ok(new Response<string>() { State = 500, Data = null, ErrorMessage = ex.Message });

            }

        }
        [HttpGet("PendingSalesRequests")]
        public async Task<ActionResult<Response<List<SalesRequestDTO>>>> PendingSalesRequests()
        {
            try
            {

                string[] Includes = { "Station" };
                var requests = await UOF.SalesRequests.FindAllAsync(f => f.Status == 0, Includes);
                if(requests is not null)
                {
                    List<SalesRequestDTO> models = Mapper.Map<List<SalesRequestDTO>>(requests.ToList());
                    models = models.Select(s => { s.StationName = requests.FirstOrDefault(f => f.Id == s.Id).Station.Name;return s; }).ToList();
                    return Ok(new Response<List<SalesRequestDTO>> { State = 1, Data = models, Message = "Pending Sales Men Requests " });
                }
                return Ok(new Response<List<SalesRequestDTO>> { State = 2, Data = new List<SalesRequestDTO>(), Message = "No Pending Sales Men Requests Found  !" });
            }catch(Exception ex)
            {
                return Ok(new Response<List<SalesRequestDTO>> { State = 500, Data = new List<SalesRequestDTO>(), Message = "Error !" , ErrorMessage = ex.Message });

            }
           
        }
    }
}
