using AutoMapper;
using BL.DTOS;
using BL.Helpers;
using BL.IServices;
using BL.TwilioSMSService;
using BL.UOW;
using Data;
using Data.Entities;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Twilio.Rest.Api.V2010.Account;

namespace Aman_gas.Controllers
{
    // Controller Description 
    // 1 // AddRole
    // 2 // RegisterAsync
    // 3 // GetToken
    // 4 // AddNewSalesRequest -  Add Sales Man Request

    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepo acc;
        private readonly ISMSService _sms;
        private readonly UserManager<AppUser> _userManager;
       private readonly DbContainer db;
       private readonly IUnitOfWork UOW;
       private readonly IMapper Mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AccountController(IAccountRepo _acc, ISMSService sms,UserManager<AppUser> userManager,DbContainer db , IUnitOfWork UOW,IMapper Mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.acc = _acc;
            this._sms = sms;
            _userManager = userManager;
            this.db = db;
            this.UOW = UOW;
            this.Mapper = Mapper;
            this.httpContextAccessor = httpContextAccessor;
        }
        [HttpGet("AddUserRole")]
        public async Task<IActionResult> AddRole(string Role)
        {
           
            return Ok(await acc.AddNewRoleAsync(Role));
        }
        [HttpPost("Register")]
        public async Task<ActionResult<Response<AuthenticationModel>>> RegisterAsync(RegisterModel model)
        {

            if (model.UserName == null || model.UserName=="")
            {
                model.UserName = model.FirstName + model.LastName;
                AppUser isexist = await _userManager.FindByNameAsync(model.UserName);

                while (isexist is null && (model.UserName == null || model.UserName == ""))
                {
                    Random x = new Random();
                    model.UserName = model.UserName + x.Next(0,1000);
                    model.Email = model.UserName + "@amangas.com";
                };
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            

            var result = await acc.RegisterAsync(model);
            
            if (!result.IsAuthenticated)
                return Ok(new Response<string>() { State = 2, Message = result.Message });
            var car = await UOW.Cars.FindAsync(s=>s.User==model.UserName);
            if (car is not null)
            {
                model.CarNumbers = car.CarNumbers;
                model.CarChars =new char[3] {car.FirstChar, car.SecondChar, car.ThirdChar};
            }
            return Ok(new Response<AuthenticationModel>() { State = 1, Data = result, Message = "succefully registered " });

        }
        [HttpPost("Token")]
        public async Task<ActionResult<Response<AuthenticationModel>>> GetToken(getTokenModel model)
        {
            if (model.UserName is null)
                model.UserName = "x";
            Response<AuthenticationModel> result = new Response<AuthenticationModel>();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            AuthenticationModel Ath = await acc.TokenAsync(model);
            result.State = Ath.IsAuthenticated ? 1 : 2;
            var car = await UOW.Cars.FindAsync(s => s.User == model.UserName);
            result.Data = Ath;
            if (car is not null)
            {
                result.Data.CarNumbers = car.CarNumbers;
                result.Data.GetChars = new char[3] { car.FirstChar, car.SecondChar, car.ThirdChar };
            }
            return Ok(result);
        }




        [HttpPost("AddNewSalesRequest")]
        public async Task<ActionResult<Response<string>>> AddNewSalesRequest(AddSalesRequestDTO model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                if (!ModelState.IsValid)
                    return Ok(new Response<string>() { State = 10, Data = null, Message = ModelState.ToString() });

                var IsExist =await  UOW.SalesRequests.FindAsync(f => f.Name == model.Name);
                if(IsExist is not null)
                    return Ok(new Response<string>() { State = 2, Data = null, Message = "This Name Is Exist Please Change Name and Try Again ! " });
                IsExist = await UOW.SalesRequests.FindAsync(f => f.NationalId == model.NationalId);
                if (IsExist is not null)
                    return Ok(new Response<string>() { State = 2, Data = null, Message = "This Nathional Id Is Alreardy Exist ! " });



                model.Password = Encrypt_Decrypt.Encrypt(model.Password);
                SalesRequest Entity = Mapper.Map<SalesRequest>(model);
                if (Entity.DateOfBirth == null)
                    Entity.DateOfBirth = DateTime.Parse("1900-01-01");
                Entity.Status = 0;
                Entity.RequestDate = AmanGasTime.Now();
               
              
                Entity.MangerApproved = "";
                var success = UOW.Complete();
              SalesRequest SR =   await UOW.SalesRequests.AddAsync(Entity);
                if (UOW.Complete() == 1)
                    return Ok(new Response<string>() { State = 1, Data = SR.Id.ToString(), Message = "Request have been Saved Succefully ! \n wait the Manager to Approve It " });
                return Ok(new Response<string>() { State = 500, Data = null, Message = "Request doesnot Saved ! \n Please Try Again ! " });



            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("An error occurred while saving the entity changes"))
                    return new Response<string>() { State = 10, Data = null, ErrorMessage = "Error Loss Of Data Entered NUll Or Foreign Keys" };
                return Ok(new Response<string> { State = 500, Message = "Error !", ErrorMessage = ex.Message });
            }
        }






        ///  Codes IF WANTS 

        //[ApiExplorerSettings(IgnoreApi =true)]
        //[HttpPost("SendSMS")]
        //public async Task<ActionResult<Response<string>>> SendSMS(SMSDTO model)
        //{
        //    var result = await _sms.SendSMSAsync(model.Number, model.Body);

        //    if (!result.ErrorMessage.IsNullOrEmpty())
        //        return Ok(new Response<MessageResource>() { State = 3, Data = result });
        //    return Ok(new Response<string>() { State = 3, Data = "sms send successfully" });

        //}

        //[HttpGet("RecentlyTask")]
        //[ApiExplorerSettings(IgnoreApi = true)]

        //public async Task<ActionResult<Response<string>>> RecentlyBackGroundTask()
        //{
        //    BackgroundJob.Enqueue(() =>  Print("One Time "));
        //    return Ok(new Response<string>() { State = 1, Data = "ExecutedSussefully" });
        //}
        //[ApiExplorerSettings(IgnoreApi = true)]

        //[HttpGet("DelayTask")]
        //public async Task<ActionResult<Response<string>>> delayBackGroungTask()
        //{
        //    BackgroundJob.Schedule(() => Print(" Delay  "), TimeSpan.FromMinutes(1));
        //    return Ok(new Response<string>() { State = 1, Data = "ExecutedSussefully" });
        //}
        //[ApiExplorerSettings(IgnoreApi = true)]

        //[HttpGet("RepeatTask")]
        //[Obsolete]
        ////  [Obsolete]
        //public  ActionResult<Response<string>> RepeatBackGroundTask()
        //{
        //    RecurringJob.AddOrUpdate(() => Print("Repeat "), Cron.Minutely);

        //    RecurringJob.AddOrUpdate(() => PrintMonthly("Repeat Monthly "), Cron.Monthly(9,20,41));

        //    return Ok(new Response<string>() { State = 1, Data = "ExecutedSussefully" });
        //}
        //[ApiExplorerSettings(IgnoreApi =true)]
        //public  void Print(string? message)
        //{
        //Console.WriteLine($"{message} Task is excuted in {DateTime.Now}");

        //} 
        //[ApiExplorerSettings(IgnoreApi =true)]
        //public  void PrintMonthly(string? message)
        //{
        //Console.WriteLine($"{message} Task is excuted in {DateTime.Now}");
        //}

        //[HttpGet,Route("getFromView")]
        //public IActionResult get()
        //{
        //   // DbContainer db = new DbContainer();
        //    var y = db.CarTypes.ToList();
        //    var x = db.TestViews.ToList();
        //    return Ok(x);
        //}

    }
}
