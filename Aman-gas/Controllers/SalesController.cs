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
       
    }
}
