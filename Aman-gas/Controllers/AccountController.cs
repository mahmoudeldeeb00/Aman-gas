using BL.DTOS;
using BL.Helpers;
using BL.IServices;
using BL.TwilioSMSService;
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
using Twilio.Rest.Api.V2010.Account;

namespace Aman_gas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepo acc;
        private readonly ISMSService _sms;
        private readonly UserManager<AppUser> _userManager;
       private readonly DbContainer db;

        public AccountController(IAccountRepo _acc, ISMSService sms,UserManager<AppUser> userManager,DbContainer db)
        {
            this.acc = _acc;
            this._sms = sms;
            _userManager = userManager;
            this.db = db;
        }
        [HttpGet("AddUserRole")]
        public async Task<IActionResult> AddRole(string Role)
        {
           
            return Ok(await acc.AddNewRoleAsync(Role));
        }
        [HttpPost("register")]
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
            return Ok(new Response<AuthenticationModel>() { State = 1, Data = result, Message = "succefully registered " });

        }
        [HttpPost("Token")]
        public async Task<ActionResult<Response<AuthenticationModel>>> GetToken(getTokenModel model)
        {
            Response<AuthenticationModel> result = new Response<AuthenticationModel>();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            AuthenticationModel Ath = await acc.TokenAsync(model);
            result.State = Ath.IsAuthenticated ? 1 : 2;
            result.Data = Ath;
            return Ok(result);



        }
        [ApiExplorerSettings(IgnoreApi =true)]
        [HttpPost("SendSMS")]
        public async Task<ActionResult<Response<string>>> SendSMS(SMSDTO model)
        {
            var result = await _sms.SendSMSAsync(model.Number, model.Body);

            if (!result.ErrorMessage.IsNullOrEmpty())
                return Ok(new Response<MessageResource>() { State = 3, Data = result });
            return Ok(new Response<string>() { State = 3, Data = "sms send successfully" });

        }

        [HttpGet("RecentlyTask")]
        [ApiExplorerSettings(IgnoreApi = true)]

        public async Task<ActionResult<Response<string>>> RecentlyBackGroundTask()
        {
            BackgroundJob.Enqueue(() =>  Print("One Time "));
            return Ok(new Response<string>() { State = 1, Data = "ExecutedSussefully" });
        }
        [ApiExplorerSettings(IgnoreApi = true)]

        [HttpGet("DelayTask")]
        public async Task<ActionResult<Response<string>>> delayBackGroungTask()
        {
            BackgroundJob.Schedule(() => Print(" Delay  "), TimeSpan.FromMinutes(1));
            return Ok(new Response<string>() { State = 1, Data = "ExecutedSussefully" });
        }
        [ApiExplorerSettings(IgnoreApi = true)]

        [HttpGet("RepeatTask")]
        [Obsolete]
        //  [Obsolete]
        public  ActionResult<Response<string>> RepeatBackGroundTask()
        {
            RecurringJob.AddOrUpdate(() => Print("Repeat "), Cron.Minutely);

            RecurringJob.AddOrUpdate(() => PrintMonthly("Repeat Monthly "), Cron.Monthly(9,20,41));



            return Ok(new Response<string>() { State = 1, Data = "ExecutedSussefully" });
        }
        [ApiExplorerSettings(IgnoreApi =true)]
        public  void Print(string? message)
        {
        Console.WriteLine($"{message} Task is excuted in {DateTime.Now}");

        } 
        [ApiExplorerSettings(IgnoreApi =true)]
        public  void PrintMonthly(string? message)
        {
        Console.WriteLine($"{message} Task is excuted in {DateTime.Now}");
        }

        [HttpGet,Route("getFromView")]
        public IActionResult get()
        {
           // DbContainer db = new DbContainer();
            var y = db.CarTypes.ToList();
            var x = db.TestViews.ToList();
            return Ok(x);
        }

    }
}
