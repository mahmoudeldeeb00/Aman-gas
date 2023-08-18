using BL.Helpers;
using BL.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aman_gas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationController : ControllerBase
    {
        private readonly IUnitOfWork _unit;
        public StationController(IUnitOfWork unit)
        {
            this._unit = unit;
        }
        [HttpPost, Route("AddStation")]
        public async Task<ActionResult<Response<string>>> AddStation(){
            return Ok(new Response<string>() { });
            }


    }
}
