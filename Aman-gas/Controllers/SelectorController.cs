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
                response.Message = "No Regions found To this Range!";
            }
            return Ok(response);
        }
        [HttpGet, Route("GetCarTypes")]
        public async Task<ActionResult<Response<List<SelectDTO>>>> GetCarTypes()
        {
            
            var response = new Response<List<SelectDTO>>();
            var result = await _unit.CarTypes.FindAllAsync(s =>  s.Status == 1);
            List<SelectDTO> cartypes = result.Select(s => new SelectDTO { Id = s.Id, Name = s.Name, ARName = s.ARName }).ToList();

            if (result.Count() > 0)
            {
                response.State = 1;
                response.Data = cartypes;
                response.Message = "Get All Car Types  ";
            }
            else
            {
                response.State = 2;
                response.Data = null;
                response.Message = "No  Car Types found !";
            }
            return Ok(response);
        }

        [HttpGet, Route("GetUnitTypes")]
        public async Task<ActionResult<Response<List<SelectDTO>>>> GetUnitTypes()
        {

            var response = new Response<List<SelectDTO>>();
            var result = await _unit.UnitTypes.FindAllAsync(s => s.Status == 1);
            List<SelectDTO> unitTypes = result.Select(s => new SelectDTO { Id = s.Id, Name = s.Name, ARName = s.ARName }).ToList();

            if (result.Count() > 0)
            {
                response.State = 1;
                response.Data = unitTypes;
                response.Message = "Get All Unit Types  ";
            }
            else
            {
                response.State = 2;
                response.Data = null;
                response.Message = "No  Unit Types found !";
            }
            return Ok(response);
        }

        [HttpGet("GetStations")]
        public async Task<ActionResult<Response<List<SelectDTO>>>> GetStations()
        {
            Response<List<SelectDTO>> response = new();
            var result = await _unit.Stations.GetAllAsync();
            List<SelectDTO> Stations = result.Select(s => new SelectDTO { Id = s.Id, Name = s.Name, ARName = s.ARName }).ToList();
            if (Stations.Count() > 0)
            {
                response.State = 1;
                response.Data = Stations;
                response.Message = "All Stations ";
            }
            else
            {
                response.State = 2;
                response.Data = null;
                response.Message = "NO Station Found yet !! ";
            }
            return Ok(response);
        }
        [HttpGet("GetFuelTypes")]
        public async Task<ActionResult<Response<List<SelectDTO>>>> GetFuelTypes()
        {

            var response = new Response<List<SelectDTO>>();
            var result = await _unit.FuelTypes.FindAllAsync(s => s.Status == 1);
            List<SelectDTO> unitTypes = result.Select(s => new SelectDTO { Id = s.Id, Name = s.Name, ARName = s.ARName }).ToList();

            if (result.Count() > 0)
            {
                response.State = 1;
                response.Data = unitTypes;
                response.Message = "Get All Fuel Types  ";
            }
            else
            {
                response.State = 2;
                response.Data = null;
                response.Message = "No  Fuel Types found !";
            }
            return Ok(response);
        }

    }
}
