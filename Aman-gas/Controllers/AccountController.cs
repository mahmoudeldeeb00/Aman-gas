using BL.DTOS;
using BL.Helpers;
using BL.IServices;
using BL.TwilioSMSService;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
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
        public AccountController(IAccountRepo _acc, ISMSService sms)
        {
            this.acc = _acc;
            this._sms = sms;
        }
        [HttpPost("register")]
        public async Task<ActionResult<Response<AuthenticationModel>>> RegisterAsync(RegisterModel model)
        {
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
        [HttpPost("SendSMS")]
        public async Task<ActionResult<Response<string>>> SendSMS(SMSDTO model)
        {
            var result = await _sms.SendSMSAsync(model.Number, model.Body);

            if (!result.ErrorMessage.IsNullOrEmpty())
                return Ok(new Response<MessageResource>() { State = 3, Data = result });
            return Ok(new Response<string>() { State = 3, Data = "sms send successfully" });

        }

        [HttpGet("RecentlyTask")]
        public async Task<ActionResult<Response<string>>> RecentlyBackGroundTask()
        {
            BackgroundJob.Enqueue(() =>  Print("One Time "));
            return Ok(new Response<string>() { State = 1, Data = "ExecutedSussefully" });
        }

        [HttpGet("DelayTask")]
        public async Task<ActionResult<Response<string>>> delayBackGroungTask()
        {
            BackgroundJob.Schedule(() => Print(" Delay  "), TimeSpan.FromMinutes(1));
            return Ok(new Response<string>() { State = 1, Data = "ExecutedSussefully" });
        }

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



    }
}
