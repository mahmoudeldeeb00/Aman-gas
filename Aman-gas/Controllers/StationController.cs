using BL.DTOS;
using BL.Helpers;
using BL.IServices;
using BL.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aman_gas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationController : ControllerBase
    {
        private readonly IStationService SS;
        public StationController(IStationService _SS)
        {
            this.SS = _SS;
        }
        [HttpPost, Route("AddStation")]
        public async Task<ActionResult<Response<string>>> AddStation(AddStationDTO Dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(await SS.AddStationAsync(Dto));
        }


    }
}
