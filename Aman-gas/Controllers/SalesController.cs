using BL.Helpers;
using BL.UOW;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aman_gas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly IUnitOfWork UOW;
        public SalesController(IUnitOfWork _unit)
        {
            this.UOW = _unit;
        }
        [HttpPost("AddNewSalesRequest")]
        public async Task<ActionResult<Response<string>>> AddNewSalesRequest(SalesRequest model )
        {
            try
            {
                if (!ModelState.IsValid)
                    return Ok(new Response<string>() { State = 10, Data = null, Message = ModelState.ToString() });
                model.Password = Encrypt_Decrypt.Encrypt(model.Password);
                await UOW.SalesRequests.AddAsync(model);
                if (UOW.Complete() == 1)
                    return Ok(new Response<string>() { State = 1, Data = null, Message = "Request have been Saved Succefully ! \n wait the Manager to Approve It " });
                 return Ok(new Response<string>() { State = 500, Data = null, Message = "Request doesnot Saved ! \n Please Try Again ! " });


               
            }catch (Exception ex)
            {

            }


            return Ok();
        }
    }
}
