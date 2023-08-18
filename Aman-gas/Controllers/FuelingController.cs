using BL.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aman_gas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuelingController : ControllerBase
    {
        private readonly IUnitOfWork Unit;
        public FuelingController(IUnitOfWork unit)
        {
            this.Unit = unit;
        }
    }
}
