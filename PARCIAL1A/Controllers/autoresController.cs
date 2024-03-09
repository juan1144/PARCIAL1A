using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class autoresController : ControllerBase
    {
        private readonly parcial1aContext _parcial1aContexto;
        public autoresController(parcial1aContext parcial1AContexto)
        {
            _parcial1aContexto = parcial1AContexto;
        }




    }
}
