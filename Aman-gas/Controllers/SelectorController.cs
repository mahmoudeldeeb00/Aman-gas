using BL.Helpers;
using BL.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Unicode;

namespace Aman_gas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SelectorController : ControllerBase
    {
        private readonly IUnitOfWork _unit;
        public SelectorController(IUnitOfWork unit)
        {
            this._unit = unit;
        }

        [HttpGet,Route("GetRanges")]
        public async Task<ActionResult<Response<List<SelectDTO>>>> GetRanges(){
            var response = new Response<List<SelectDTO>>();
            var result = await _unit.Ranges.FindAllAsync(s => s.Status == 1);
            List<SelectDTO> ranges = result.Select(s => new SelectDTO { Id = s.Id, Name = s.Name, ARName = s.ARName }).ToList();
                
            if(result.Count() > 0)
            {
                response.State = 1;
                response.Data = ranges;
                response.Message = "Get All Ranges ";
            }
            else
            {
                response.State = 2;
                response.Data = null;
                response.Message = "no Ranges found !";
            }
            return Ok(response);
        }
        [HttpGet,Route("GetRegionsToRange")]
        public async Task<ActionResult<Response<List<SelectDTO>>>> GetRegions(int RangeId){
            var response = new Response<List<SelectDTO>>();
            var result = await _unit.Regions.FindAllAsync(s => s.RangeId == RangeId &&  s.Status == 1 );
            List<SelectDTO> ranges = result.Select(s => new SelectDTO { Id = s.Id, Name = s.Name, ARName = s.ARName }).ToList();
                
            if(result.Count() > 0)
            {
                response.State = 1;
                response.Data = ranges;
                response.Message = "Get All Regions to range  ";
            }
            else
            {
                response.State = 2;
                response.Data = null;
                response.Message = "no Regions found To this Range!";
            }
            return Ok(response);
        }
        
    }
}
