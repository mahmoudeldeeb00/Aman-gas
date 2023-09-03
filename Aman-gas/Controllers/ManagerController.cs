using AutoMapper;
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
    // Controller Description 
    // 1 // AcceptSalesRequests // Accept and Deny Sales Man Requests 
    // 2 // PendingSalesRequests // Get Pendig Sales Request 
    // 3 // TransfereSalesMan // Transfere Sales Man From Station To Station 


    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class ManagerController : ControllerBase
    {
        private readonly IUnitOfWork UOF;
        private readonly UserManager<AppUser> UserMNG;
        private readonly IMapper Mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IManagerService MS;
        public ManagerController(IUnitOfWork _unit , UserManager<AppUser> _UserMNG, IMapper Mapper,IHttpContextAccessor httpContextAccessor, IManagerService MS)
        {
            this.UserMNG = _UserMNG;
            this.UOF = _unit;
            this.Mapper = Mapper;
            this.httpContextAccessor = httpContextAccessor; 
            this.MS = MS;
        }

        [HttpGet("AcceptSalesRequests")]
        public async Task<ActionResult<Response<string>>> AcceptSalesRequests(int RequestId , int Deny = 0 )
        {
                string UserName = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return Ok(await MS.AcceptSalesRequestsAsync(RequestId, Deny, UserName));

        }
        [HttpGet("PendingSalesRequests")]
        public async Task<ActionResult<Response<List<SalesRequestDTO>>>> PendingSalesRequests() => Ok(await MS.PendingSalesRequestsAsync());
        [HttpGet("TransfereSalesMan")]
        public async Task<ActionResult<Response<string>>> TransfereSalesMan(int StationId, int SalesManId) => Ok( await MS.TransfereSalesManAsync(StationId,SalesManId));
        [HttpGet("GetFuelSetting")]
        public async Task<ActionResult<Response<FuelSettingDTO>>> GetFuelSetting(int FuelId)
        {
            return Ok(await MS.GetFuelSettingAsync(FuelId));
        } 
        [HttpPost("SetFuelSetting")]
        public async Task<ActionResult<Response<string>>> SetFuelSetting([FromBody]FuelSettingDTO DTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(await MS.SetFuelSettingAsync(DTO));
        }
      
    }
}
